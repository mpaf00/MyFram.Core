namespace MyFram.Core.Exception
{
    using System;

    public static class ExceptionHelper
    {
        public static void DealingException(Exception exception)
        {
            throw exception;
        }

        public static void RaiseException(string message)
        {
            throw new Exception(message);
        }
    }
}