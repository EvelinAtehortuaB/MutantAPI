namespace Application.Exceptions
{
    public class ValidateException: Exception
    {
        public ValidateException(string messages): base(message: messages)
        {

        }
    }
}
