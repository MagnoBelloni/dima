namespace Dima.Core
{
    public static class Configuration
    {
        public const int DefaultPageSize = 15;
        public const int DefaultPageNumber = 1;
        public const int DefaultStatusCode = 200;

        public static string ConnectionString { get; set; } = string.Empty;
        public static string BackendUrl { get; set; } = string.Empty;
        public static string FrontendUrl { get; set; } = string.Empty;
    }
}
