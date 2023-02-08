using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UtilityBot.Utilities
{
    public static class TextToTask
    {
        static Regex case1 = new Regex(@"1$");
        static Regex case11 = new Regex(@"11$");
        static Regex case234 = new Regex(@"[2-4]$");
        static Regex summRgx = new Regex(@"\s+");
        public static string CallRightTask(string text, string task)
        {int count;
            switch (task)
            {
                case "count":
                    count = CountChars(text);
                    if (case11.IsMatch(count.ToString()))
                        return $"В вашем сообщении {count} символов.";
                    else if (case1.IsMatch(count.ToString()))
                            return $"В вашем сообщении {count} символ.";
                    else if (case234.IsMatch(count.ToString()))
                        return $"В вашем сообщении {count} символа.";
                    else
                        return $"В вашем сообщении {count} символов.";
                case "add":
                    count = CountSumm(text);
                    return $"Сумма введенных вами чисел равна {count}.";
                    default: throw new NotSupportedException("Выберите задачу в главном меню или введя команду /start.");
            }
        }

        public static int CountChars(string text)
        {    
            return text.Length;
        }
        public static int CountSumm(string text)
        {
            
            int summ = 0;
            string[] numbers = summRgx.Split(text);
            foreach (string chunk in numbers)
            {
                if (Int32.TryParse(chunk, out int number))
                    summ += number; 
                else throw new ArgumentException("Пожалуйста введите числа, разделенные пробелом.");
            }
            return summ;
        }
    }
}
