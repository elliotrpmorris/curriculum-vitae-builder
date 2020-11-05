// <copyright file="ContactDetail.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.Cv.CvSections.Contact
{
    using System;

    /// <summary>
    /// Contact detail.
    /// </summary>
    public class ContactDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactDetail"/> class.
        /// </summary>
        /// <param name="name">The contact detail name.</param>
        /// <param name="value">The contact detail value.</param>
        public ContactDetail(
            string name,
            string value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets the name of the contact detail.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the contact detail.
        /// </summary>
        public string Value { get; }
    }
}
