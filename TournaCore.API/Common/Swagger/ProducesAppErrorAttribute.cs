namespace TournaCore.API.Common.Swagger {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ProducesAppErrorAttribute(string errorName) : Attribute {
        public string ErrorName { get; } = errorName;
    }
}
