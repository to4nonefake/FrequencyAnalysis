using System;
using System.Diagnostics;
using System.Threading;
using FreqncyAnalys.CL.Controllers;

namespace FrequencyAnalys.CMD
{
    class Program
    {
        static Stopwatch timer = new Stopwatch();
        static void Main(string[] args)
        {
            Console.Write("Введите путь: ");
            var path = Console.ReadLine();
            Console.WriteLine("Подготовка файла...\nДля отмены нажмите любую клавишу.");

            // Подготовка файла
            FileController fc = new FileController(path);
            Thread filePreapareThread = new Thread(new ParameterizedThreadStart(Prepare));
            filePreapareThread.Start(fc);

            Console.ReadKey();
            if (!fc.File.FileReady) {
                filePreapareThread.Abort();
                Console.WriteLine("Была произведена отмена операции. Файл не был загружен.");
                Console.Write("Для выхода из программы нажмите любую клавишу.");
                Console.ReadKey();

            } else {
                Console.WriteLine("Анализ текста...\nДля отмены нажмите любую клавишу.");

                //Анализ файла
                timer.Start();
                Thread fileAnalysThread = new Thread(new ParameterizedThreadStart(Analys));
                fileAnalysThread.Start(fc);
                Console.ReadKey();
                if (!fc.File.AnalysWasCompleted) {
                    fileAnalysThread.Abort();
                    timer.Stop();
                    Console.WriteLine("Была произведена отмена операции. Текущий результат:");
                    Print(fc);
                    Console.ReadKey();
                }
            }
        }
        /// <summary>
        /// Подготовка файла к анализу.
        /// </summary>
        /// <param name="fc"> Объект контроллера. </param>
        public static void Prepare(object fc)
        {
            var _fc = (FileController)fc;
            _fc.Load();
            _fc.Normalize(_fc.File);
            _fc.Split(_fc.File);
            _fc.File.FileReady = true;
            Console.WriteLine("Файл подготовлен к анализу. Для начала анализа нажмите любую клавишу.");
        }
        /// <summary>
        /// Анализ файла.
        /// </summary>
        /// <param name="fc"> Объект контроллера. </param>
        public static void Analys(object fc)
        {
            var _fc = (FileController)fc;
            _fc.Analys(_fc.File);
            timer.Stop();
            Print(_fc);
        }
        /// <summary>
        /// Вывод результатов.
        /// </summary>
        /// <param name="fc"> Объект контроллера. </param>
        public static void Print(FileController fc)
        {
            fc.FindMaxTen(fc.File);
            for (int i = 0; i < 10; i++) {
                Console.WriteLine($"\"{fc.File.MaxKeys[i]}\" встречается {fc.File.MaxValues[i]} раз");
            }
            TimeSpan timeTaken = timer.Elapsed;
            Console.WriteLine($"Врем выполнения: {timeTaken.ToString(@"m\:ss\.fff")}");
            Console.Write("Для выхода из программы нажмите любую клавишу.");
        }
    }
}
