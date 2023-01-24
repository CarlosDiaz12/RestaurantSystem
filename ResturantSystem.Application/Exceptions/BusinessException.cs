namespace ResturantSystem.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() { }
        public BusinessException(string message) : base(message) { }
    }
}
