using System;
using System.Collections.Generic;
using System.IO;
using FreqncyAnalys.CL.Models;

namespace FreqncyAnalys.CL.Controllers
{
    /// <summary>
    /// Контроллер работы с файлами.
    /// </summary>
    public class FileController
    {
        public FileToAnalys File { get; }
        /// <summary>
        /// Создание и загрузка файла.
        /// </summary>
        /// <param name="path"> Путь. </param>
        public FileController(string path)
        {
            File = new FileToAnalys(path);
            
        }
        public void Load()
        {
            if (string.IsNullOrWhiteSpace(File.Path)) {
                throw new ArgumentNullException("Путь не может быть Null");
            }
            try {
                using (var sr = new StreamReader(File.Path)) {
                    File.Text = sr.ReadToEnd();
                }
            }
            catch {
                //throw new Exception("Неудается прочесть выбранный файл");
            }
        }
        /// <summary>
        /// Нормализирование текста.
        /// </summary>
        /// <param name="file"> Объект. </param>
        public void Normalize(FileToAnalys file)
        {
            while (file.Text.Contains(Environment.NewLine)) { file.Text = file.Text.Replace(Environment.NewLine, " "); }
            while (file.Text.Contains("  ")) { file.Text = file.Text.Replace("  ", " "); }
        }
        /// <summary>
        /// Разделение текста на отдельные слова.
        /// </summary>
        /// <param name="file"> Объект. </param>
        public void Split(FileToAnalys file)
        {
            //TODO: производить проверку слов менее 3х символов здесь и не записывать такие слова в словарь
            file.Words = file.Text.Split(' ');
        }
        /// <summary>
        /// Анализ текста.
        /// </summary>
        /// <param name="file"> Объект. </param>
        public void Analys(FileToAnalys file)
        {
            //Выводим массив слов
            for (int h = 0; h < file.Words.Length; h++) {
                //Запоолняем словарь
                if (file.Words[h].Length > 2) {
                    for (int i = 0; i < file.Words[h].Length - 2; i++) {
                        if (!file.Triplets.ContainsKey(file.Words[h].Substring(i, 3))) {
                            file.Triplets.Add(file.Words[h].Substring(i, 3), 1);
                        } else {
                            file.Triplets[file.Words[h].Substring(i, 3)] += 1;
                        }
                    }
                }
            }
            file.AnalysWasCompleted = true;
        }
        /// <summary>
        /// Поиск 10ти максимальных значений.
        /// </summary>
        /// <param name="file"> Объект. </param>
        public void FindMaxTen(FileToAnalys file)
        {
            foreach (KeyValuePair<string, int> obj in file.Triplets) {
                // Т.к. ищем 10 значений
                for (int i = 0; i < 10; i++) {
                    if (file.MaxValues[i] < obj.Value) {
                        file.MaxValues[i] = obj.Value;
                        file.MaxKeys[i] = obj.Key;
                        break;
                    }
                }
            }
        }
    }
}
