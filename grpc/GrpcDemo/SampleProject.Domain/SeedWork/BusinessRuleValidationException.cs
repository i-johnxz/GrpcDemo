using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Domain.SeedWork
{
    public class BusinessRuleValidationException : Exception
    {
        public string Details { get; set; }

        public BusinessRuleValidationException(string message, string details) : base(message)
        {
            Details = details;
        }

        public BusinessRuleValidationException(string message) : base(message)
        {

        }
    }
}
