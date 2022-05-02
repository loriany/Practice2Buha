using System;

namespace Practice2Buha.Exceptions
{
    class EmailException : Exception
    {
        public string Value { get; }
        public EmailException(string message, string value) : base(message)
        {
            Value = value;
        }
    }
}
