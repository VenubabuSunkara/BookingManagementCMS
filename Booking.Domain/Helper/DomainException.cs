using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Helper
{
    public abstract class DomainException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        protected DomainException(string message, IDictionary<string, string[]> errors = null)
            : base(message)
        {
            Errors = errors?.AsReadOnly() ?? new Dictionary<string, string[]>().AsReadOnly();
        }
    }

    public class InvalidEntityStateException : DomainException
    {
        public InvalidEntityStateException(string message) : base(message) { }
    }

    public class BusinessRuleValidationException : DomainException
    {
        public BusinessRuleValidationException(string message) : base(message) { }
    }
}
