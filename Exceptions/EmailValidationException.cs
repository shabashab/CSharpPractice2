using System;

namespace CSharpPractice2.Exceptions
{
    public class EmailValidationException : Exception
    {
        public EmailValidationException(string message) : base(message)
        {

        }
    }
}
