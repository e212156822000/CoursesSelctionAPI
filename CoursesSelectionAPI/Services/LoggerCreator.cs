namespace CoursesSelectionAPI.Init
{
    public static class LoggerCreator
    {
        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            AddLogging(builder.Logging);

            return builder;
        }

        public static ILogger CreateLogger<T>()
        {
            return LoggerFactory
                .Create(AddLogging)
                .CreateLogger<T>();
        }

        public static void AddLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder
                .ClearProviders()
                .AddSimpleConsole(options =>
                {
                    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
                    options.IncludeScopes = true;
                })
                .AddDebug();
        }
    }
}
