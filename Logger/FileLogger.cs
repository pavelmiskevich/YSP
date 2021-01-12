using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Logger
{
    /// <summary>
    /// Логгер
    /// </summary>
    public class FileLogger : ILogger
    {
        private string filePath;        
        private static object _lock = new object();
        public FileLogger(string path)
        {
            filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        /// <summary>
        /// Производится запись в текстовый файл
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                var dateTime2 = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff");
                string dirPath = filePath.Substring(0, filePath.LastIndexOf("/"));
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);

                lock (_lock)
                {                    
                    File.AppendAllTextAsync(filePath, $"{dateTime2} : " + formatter(state, exception) + Environment.NewLine); //.AppendAllText
                }
            }
        }
    }
}
