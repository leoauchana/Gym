﻿namespace Web_Application.Exceptions;

public class NullException : ApplicationException
{
    public NullException(string message) : base(message) { }
}
