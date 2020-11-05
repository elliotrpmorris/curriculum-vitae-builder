// <copyright file="QueryController.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query
{
    using System.Linq;
    using System.Threading.Tasks;

    using GraphQL;
    using GraphQL.DataLoader;
    using GraphQL.Types;

    using Microsoft.AspNetCore.Mvc;

    [Route("query")]
    [Produces("application/json")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Query(
          [FromServices] IDocumentExecuter executor,
          [FromServices] ISchema schema,
          [FromServices] DataLoaderDocumentListener dataLoaderListener,
          [FromBody] GraphQLRequest request)
        {
            if (request?.Query == null)
            {
                return this.BadRequest("Invalid Query.");
            }

            var result = await executor.ExecuteAsync(options =>
            {
                options.Schema = schema;
                options.Query = request.Query;
                options.Inputs = request.Variables?.ToInputs();

                options.Listeners.Add(dataLoaderListener);
            });

            if (result.Errors?.Any() == true)
            {
                foreach (var error in result.Errors)
                {
                    // TODO: Could track this via something like Azure Application Insights.
                }

                return this.BadRequest(result.Errors);
            }

            return this.Ok(result);
        }
    }
}
