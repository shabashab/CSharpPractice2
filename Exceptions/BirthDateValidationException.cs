using System;

namespace CSharpPractice2.Exceptions
{
    public class BirthDateValidationException : Exception
    {
        public BirthDateValidationException(string message) : base(message)
        {
        }
    }
}
