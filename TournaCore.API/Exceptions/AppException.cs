using TournaCore.API.Common;

namespace TournaCore.API.Exceptions {
    public class AppException(AppError error) : Exception(error.ErrorMessage) {
        public AppError Error { get; } = error;
    }
}
