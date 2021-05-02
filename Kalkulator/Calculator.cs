using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator
{
    class Calculator
    {   
        List<string> list = new List<string>();

        public void Search(string primer)
        {
            bool open = false;
            bool close = false;
            int open_pos = -1;
            int close_pos = -1;

            // Проходим по примеру и проверяем на наличие скобок
            for (int i = 0; i < primer.Length; i++)
            {
                if(primer[i] == '(')
                {
                    open = true;
                    open_pos = i;
                }
                if (primer[i] == ')')
                {
                    if(open)
                    {
                        close = true;
                        close_pos = i;
                        // Извлекаем выражение из скобок и передаем в метод производя математическую операцию
                        // и записываем результат в переменную
                        string result = Calc(primer.Substring(open_pos + 1, close_pos - open_pos-1));

                        // Заменяем выражение со скобками на полученный результат
                        primer = primer.Remove(open_pos, close_pos - open_pos+1);
                        primer = primer.Insert(open_pos, result);
                        i = 0;
                        open=close = false;
                        open_pos = close_pos = -1;
                    }
                    else
                        Console.WriteLine("Отсутствует открывающая скобка");
                }
            }

            if (open && !close)
            {
                Console.WriteLine("В примере не закрыта скобка");
            }
            else
            {
                // Проверка наличия скобок в примере далее
                for (int i = 0; i < primer.Length; i++)
                {
                    // Если в примере есть скобка, то запутить метод поиска скобок заново
                    if (primer[i] == '(')
                    {
                        Search(primer);
                    }
                }
                Console.WriteLine(Calc(primer));
            }
        }

        // Метод преобразования примера в простое выражение
        private string Calc(string primer)
        {
            list.Clear();
            string tmp = "";
            for (int i = 0; i < primer.Length; i++)
            {
                
                if (primer[i] == '+' || primer[i] == '-' || primer[i] == '*'|| primer[i] == '/' || primer[i] == '^')
                {
                    list.Add(tmp);
                    list.Add(primer[i].ToString());
                    tmp = "";
                }
                else
                {
                    tmp += primer[i];
                }
            }
            list.Add(tmp);

            // Проверяем и выполняем приоритентные операции
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].ToString() == "*" || list[i].ToString() == "/" || list[i].ToString() == "^")
                {
                    if(i == 0 || i == list.Count-1)
                    {
                        return "Error";
                    }
                    ProSkobk(i);
                    i = 0;
                }
            }

            // Проверяем и выполняем на второстепенные операции
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ToString() == "+" || list[i].ToString() == "-")
                {
                    if (i == 0 || i == list.Count - 1)
                    {
                        return "Error";
                    }
                    ProSkobk(i);
                    i = 0;
                }
            }
            return list[0].ToString();
        }

        /// <summary>
        /// Метод математических вычислений
        /// </summary>
        /// <param name="num1">Первое число</param>
        /// <param name="mfunc">Математическое действие</param>
        /// <param name="num2">Второе число</param>
        /// <returns></returns>
        private string MathCalc(string number1, string mfunc, string number2)
        {
            try
            {
                double num1 = Convert.ToDouble(number1);
                double num2 = Convert.ToDouble(number2);

                switch (mfunc)
                {
                    case "/":
                        string res = num2 != 0 ? (num1 / num2).ToString() : "Деление на ноль не возможно";
                        return res;
                    case "*":
                        return (num1 * num2).ToString();
                    case "^":
                        return (Math.Pow(num1, num2)).ToString();
                    case "+":
                        return (num1 + num2).ToString();
                    case "-":
                        return (num1 - num2).ToString();
                }

            }
            catch (Exception)
            {
                return "Error";
            }
            return "Error";
        }

        // Метод замены выражения в скобках на полученный результат
        public string ProSkobk(int i)
        {
            string result = MathCalc(list[i - 1].ToString(), list[i].ToString(), list[i + 1].ToString());
            if (result == "Error") return "Error";
            list.RemoveAt(i - 1);
            list.RemoveAt(i - 1);
            list.RemoveAt(i - 1);
            list.Insert(i - 1, result);
            return "";
        }
    }
}
