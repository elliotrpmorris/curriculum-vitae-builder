// <copyright file="Extensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace Chest.Core.Command
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Chest.Core.Command.Internal;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A factory function that can dynamically resolve an object instance
    /// from its type.
    /// </summary>
    /// <param name="type">The type to resolve.</param>
    /// <returns>The resolved object instance.</returns>
    internal delegate object ServiceLocator(Type type);

    /// <summary>
    /// Dependency Registration Extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Creates and registers a command pipeline with the given container
        /// for all commands in the given assemblies.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="commandHandlerAssembly">The assembly to scan for commands.</param>
        /// <param name="setupAction">A delegate to configure the options for the command pipeline.</param>
        /// <returns>The service instance.</returns>
        public static IServiceCollection AddCommandBus(
           this IServiceCollection services,
           Assembly commandHandlerAssembly,
           Action<CommandBusOptions>? setupAction = null)
        {
            return services.AddCommandBus(
                new List<Assembly>
                {
                    commandHandlerAssembly,
                },
                setupAction);
        }

        /// <summary>
        /// Creates and registers a command pipeline with the given container
        /// for all commands in the given assemblies.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="commandHandlerAssemblies">The assemblies to scan for commands.</param>
        /// <param name="setupAction">A delegate to configure the options for the command pipeline.</param>
        /// <returns>The service instance.</returns>
        public static IServiceCollection AddCommandBus(
           this IServiceCollection services,
           List<Assembly> commandHandlerAssemblies,
           Action<CommandBusOptions>? setupAction = null)
        {
            var busOptions = new CommandBusOptions();
            setupAction?.Invoke(busOptions);
            services.AddSingleton(busOptions);

            // register a service locator to be used by the command dispatcher
            services.AddSingleton<ServiceLocator>(services =>
            {
                return type => services.GetService(type);
            });

            // register the command bus and dispatcher
            services.AddSingleton<ICommandBus, CommandBus>();
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

            // get the type information for all commands in the given assemblies
            // this will return all of the command handlers, all of the command
            // validators and a list of handler details for the registry
            var types = commandHandlerAssemblies
                .GetHandlerDetails();

            // register the command handlers, auth providers and validator so
            // they can be resolved dynamically
            types.HandlerTypes.ToList().ForEach(x => services.AddTransient(x));
            types.ValidatorTypes.ToList().ForEach(x => services.AddTransient(x));
            types.AuthProviderTypes.ToList().ForEach(x => services.AddTransient(x));

            // create and register the command handler registry
            services.AddSingleton<ICommandHandlerRegistry>(
                new CommandHandlerRegistry(types.TypeDetails));

            return services;
        }

        /// <summary>
        /// Gets the dynamic context from the command metadata as the given type.
        /// If either the metadata or context is null, or is of the wrong type,
        /// the method will return <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The context type.</typeparam>
        /// <param name="metadata">The command metadata.</param>
        /// <returns>The custom context as the given type.</returns>
        public static T? GetContext<T>(this CommandMetadata metadata)
            where T : class
        {
            if (metadata?.Context == null)
            {
                return null;
            }

            if (metadata.Context is T context)
            {
                return context;
            }

            return null;
        }
    }
}
