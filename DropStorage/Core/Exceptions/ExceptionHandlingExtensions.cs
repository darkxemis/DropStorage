namespace DropStorage.Core.Exceptions
{
    public static class ExceptionHandlingExtensions
    {
        public static void UseErrorControllerAsExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions()
            {
                AllowStatusCode404Response = true,
                ExceptionHandlingPath = "/error-local-development"
            });
        }
    }
}
