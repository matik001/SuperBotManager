using SuperBotManagerBackend.DTOs;

namespace SuperBotManagerBackend.Services
{
    public class ServiceException : Exception
    {
        public ErrorDTO Error { get; set; }
        public ServiceException(ErrorDTO error)
        {
            Error = error;
        }
    }
    public static class ServiceUtils
    {
        public static ServiceException BadRequest(string message = "Bad Request")
        {
            return new ServiceException(new ErrorDTO
            {
                StatusCode = 400,
                Message = message
            });
        }

    }
}
