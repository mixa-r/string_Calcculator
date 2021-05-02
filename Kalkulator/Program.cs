using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kalkulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            
            while (true)
            {
                Console.WriteLine("Калькулятор строковых выражений. " +
                    "\nАрефмитические действия: \n" +
                    " + Сложение\n" +
                    " - Вычетание\n" +
                    " * Умножение\n" +
                    " / Деление\n" +
                    " ^ Возведение в степень\n" +
                    "Пример записи 5+5+(5^2)/(3,25-0,25)" +
                    "\nВведите выражение: ");
                calc.Search(Console.ReadLine());
                Console.WriteLine("Для завершения работы калькулятора нажмите \"Q\", для продолжнения нажмите Enter");
                if (Console.ReadLine() == 'q'.ToString().ToLower())
                    break;
            }
        }
    }
}
