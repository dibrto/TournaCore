namespace TournaCore.API.Exceptions {
    public class AppException(int statusCode, int errorCode, string errorMessage) : Exception(errorMessage) {                
        public int StatusCode { get; } = statusCode;
        public int ErrorCode { get; } = errorCode;
    }
}
