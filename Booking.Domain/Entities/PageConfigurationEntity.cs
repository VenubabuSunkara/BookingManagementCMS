using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{

    public class PageConfiguration
    {
        // Private setters to enforce encapsulation
        public PageConfigurationId Id { get; private set; }
        public PageName Name { get; private set; }
        public PageContent Content { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public string UpdatedBy { get; private set; }
        public bool IsActive { get; private set; }
        public string Placeholder { get; private set; }

        // Private constructor to prevent direct instantiation
        private PageConfiguration() { }

        // Factory method for creating a new PageConfiguration
        public static PageConfiguration Create(
            PageName name,
            PageContent content,
            string createdBy,
            DateTime createdOn,
            bool isActive,
            string placeholder)
        {
            return new PageConfiguration
            {
                Id = new PageConfigurationId(1), // Replace '1' with appropriate Id generation logic
                Name = name,
                Content = content,
                CreatedOn = createdOn,
                UpdatedOn = createdOn,
                CreatedBy = createdBy,
                UpdatedBy = createdBy,
                IsActive = isActive,
                Placeholder = placeholder,
            };
        }

        // Methods to update properties
        public void UpdateContent(PageContent newContent, string updatedBy)
        {
            Content = newContent;
            UpdatedOn = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

        public void ToggleActive(string updatedBy)
        {
            IsActive = !IsActive;
            UpdatedOn = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

        // Other domain methods as needed...
    }

    // Value Objects
    public record PageConfigurationId
    {
        public int Value { get; }

        public PageConfigurationId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be a positive integer", nameof(id));
            Value = id;
        }

        // Parameterless constructor for ORM
        private PageConfigurationId() { }
    }

    public record PageName
    {
        public string Value { get; }

        public PageName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Page name cannot be empty", nameof(name));
            Value = name;
        }
    }

    public record PageContent
    {
        public string Value { get; }

        public PageContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Page content cannot be empty", nameof(content));
            Value = content;
        }
    }
}
