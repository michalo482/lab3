namespace lab3library
{
    public interface ILogger : IDisposable
    {
        public void Log(params string[] messages);
    }
}