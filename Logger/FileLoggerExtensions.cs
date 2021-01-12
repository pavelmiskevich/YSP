using Microsoft.Extensions.Logging;

namespace Logger
{
    public static class FileLoggerExtensions
    {
        /// <summary>
        /// Раширяет объект ILoggerFactory метод расширения AddFile, который будет добавлять провайдер логгирования в файл
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static ILoggerFactory AddFile(this ILoggerFactory factory,
                                        string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }
    }
}
