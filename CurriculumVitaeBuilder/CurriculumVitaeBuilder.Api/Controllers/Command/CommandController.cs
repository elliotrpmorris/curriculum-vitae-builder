// <copyright file="CommandController.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Command
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;
    using Chest.Core.Logging;

    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    [Route("command")]
    [Produces("application/json")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(
           [FromServices] ICommandBus commandBus,
           [FromBody] CommandRequest request)
        {
            if (request == null
                || string.IsNullOrWhiteSpace(request.Command)
                || request.Body == null)
            {
                return this.BadRequest("Invalid Command.");
            }

            Exception? commandException = null;

            try
            {
                var correlationId = Guid.NewGuid().ToString();

                var validateOnly =
                    this.Request.Headers.ContainsKey(CommandHeaders.ValidateOnly)
                    && bool.TryParse(this.Request.Headers[CommandHeaders.ValidateOnly].ToString(), out bool result);

                var metadata = new CommandMetadata(
                    request.Command,
                    DateTime.UtcNow,
                    correlationId);

                await commandBus.Send(
                    request.Body,
                    request.Command,
                    metadata,
                    validateOnly);

                var response = new
                {
                    command = request.Command,
                    correlationId,
                    executed = !validateOnly,
                };

                return this.Ok(response);
            }
            catch (CommandHandlerNotFoundException ex)
            {
                commandException = ex;

                Logger.LogError(ex, ex.Message);
                return this.BadRequest(
                    new
                    {
                        message = $"Unknown command: '{request.Command}'",
                    });
            }
            catch (InvalidCommandException ex)
            {
                commandException = ex;

                Logger.LogError(ex, ex.Message);
                return this.BadRequest(
                    new
                    {
                        message = $"{request.Command} command is invalid",
                        errors = ex.ValidationErrors,
                    });
            }
            catch (JsonSerializationException ex)
            {
                commandException = ex;

                return this.BadRequest(
                    new
                    {
                        message = $"{request.Command} command is invalid",
                        errors = new List<string> { $"Could not process {ex.Path}. Please check value (and parent) is of correct type." },
                    });
            }
            catch (UnauthorizedAccessException ex)
            {
                commandException = ex;

                Logger.LogError(ex, ex.Message);
                return this.StatusCode(403);
            }
        }
    }
}
