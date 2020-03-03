using System;
using System.Collections.Generic;

namespace FreqncyAnalys.CL.Models
{
    /// <summary>
    /// Файл.
    /// </summary>
    public class FileToAnalys
    {
        /// <summary>
        /// Путь к файлу.
        /// </summary>
        public string Path { get; }
        /// <summary>
        /// Содержание файла.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Встречающиеся в тексте слова.
        /// </summary>
        public string[] Words { get; set; }
        /// <summary>
        /// Ключ для максимальных значений вхождений триплетов.
        /// </summary>
        public string[] MaxKeys { get; set; }
        /// <summary>
        /// Максимальные знаения вхождений триплетов.
        /// </summary>
        public int[] MaxValues { get; set; }
        /// <summary>
        /// Маркер готовности файла к обработке.
        /// </summary>
        public bool FileReady { get; set; }
        /// <summary>
        /// Флаг о полном завершении анализа.
        /// </summary>
        public bool AnalysWasCompleted { get; set; }
        /// <summary>
        /// Триплет + колиство вхождений.
        /// </summary>
        public Dictionary<string, int> Triplets { get; set; }
        /// <summary>
        /// Создать новый файл.
        /// </summary>
        /// <param name="path"> Путь к файлу. </param>
        public FileToAnalys(string path)
        {
            Triplets = new Dictionary<string, int>();
            MaxValues = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            MaxKeys = new string[10];
            FileReady = false;
            AnalysWasCompleted = false;

            if (string.IsNullOrWhiteSpace(path)) {
                throw new ArgumentNullException($"Путь {nameof(path)} не может быть пустым");
            }
            Path = path;
        }
        public override string ToString()
        {
            return Path;
        }
    }
}
