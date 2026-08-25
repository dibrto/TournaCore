namespace TournaCore.API.Common;

public static class ErrorCodes {
    // General 1000-1099
    public const int ValidationError = 1001;

    // Auth 1100-1199
    public const int InvalidCredentials = 1101;
    public const int EmailAlreadyExists = 1102;
}