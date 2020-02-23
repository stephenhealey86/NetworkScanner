namespace NetworkScanner
{
    public interface ILoggingService
    {
        void Log(LogLevels logLevel, string message);
    }
}