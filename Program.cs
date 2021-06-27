using System;
using static System.Console;

namespace HelloApp
{
    class Program
    {
        delegate void NewString();
        static void Main(string[] args)
        {
                WriteLine("Операции должны происходить в асинхронном режиме? [y/n]");
                var cache = ReadLine();
                var yesno = cache.ToLower() == "y" ? true : false;
                WriteLine("Введите количесвто строк");
                cache = "";
                var s = ReadLine();
                int i;
                int.TryParse(s, out i);
                WriteLine("Попытка генерации строк с использованием асинхронности:");
                var time = DateTime.Now;
                WriteLine("Начало генерации - " + time);
                var iters = 0;
                NewString ns = () => {
                    for (int d = 0; d < 160; d++)
                    {
                        cache = cache + (char)new Random().Next(10, 126);
                    }
                    cache = cache + "\n";
                    iters++;
                };
                for (int c = 1; c < i; c++)
                {
                    if (yesno)
                        ns.BeginInvoke((IAsyncResult ar) =>
                        {
                            WriteLine("Конец итерации генерации " + iters + " наступил на - " + DateTime.Now + " - начало - " + time+" Заняло: "+ DateTime.Now.Subtract(time));
                        }, null);
                    else
                    {
                        ns();
                        WriteLine("Конец итерации генерации " + iters + " наступил на - " + DateTime.Now + " - начало - " + time + " Заняло: " + DateTime.Now.Subtract(time));
                    }
                }
                var text = yesno ? "\n\nКонец генерации ещё не наступил" : "\n\nНаступил конец генерации";
                WriteLine(text);
                ReadLine();
            //}
        }
    }
}
