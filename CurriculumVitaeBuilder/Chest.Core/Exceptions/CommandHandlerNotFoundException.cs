﻿// <copyright file="CommandHandlerNotFoundException.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace Chest.Core.Exceptions
{
    using System;

    /// <summary>
    /// An exception that is thrown when a command handler's details cannot be resolved
    /// from the <see cref="Internal.ICommandHandlerRegistry"/>.
    /// </summary>
    [Serializable]
    public class CommandHandlerNotFoundException : Exception
    {
    }
}