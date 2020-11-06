// <copyright file="ICvReader.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// CV reader.
    /// </summary>
    public interface ICvReader
    {
        public Task<Cv?> GetCvByUserAsync(Guid userId);
    }
}
