using System;
using System.Windows;


namespace Practice2Buha.Exceptions
{
    class DateException : Exception
    {
        public DateTime Value { get; }
        public DateException(string message, DateTime value) : base(message)
        {
            Value = value;
        }
    }
}
