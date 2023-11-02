using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
//Створіть клас «Кредитна картка». Клас повинен містити: 
//■ Номер картки;
//■ ПІБ власника; 
//■ Термін дії карти; 
//■ PIN; 
//■ Кредитний ліміт; 
//■ Сума грошей. 
//Створіть потрібний набір способів класу. 
//Реалізуйте події для наступних ситуацій: 
//■ Поповнення рахунку; 
//■ Витрата коштів з рахунку; 
//■ Старт використання кредитних коштів; 
//■ Досягнення ліміту заданої суми грошей; 
//■ Зміна PIN.
//Записуйте об’єкти “Кредитної картки” в файл

namespace Task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreditCard acount = new CreditCard("123456789", "Vitaliy Pupkin", "01/11/2024", 987654321, 5000, 1000 );
            acount.RegisterHandler(new CreditCard.AccountStateHandler(Red_Message));

            foreach (CreditCard.AccountStateHandler item in acount.del.GetInvocationList())
            {
                Console.WriteLine("{0}", item.Method.Name);
            }

            
            Console.WriteLine(acount.ToString());
            acount.Put(1000);
            Console.WriteLine(acount.ToString());
            acount.Withdraw(500);
            Console.WriteLine(acount.ToString());
            acount.Withdraw(2000);
            Console.WriteLine(acount.ToString());
            acount.Withdraw(5000);
            Console.WriteLine(acount.ToString());
            acount.Put(2000);
            Console.WriteLine(acount.ToString());
            acount.Put(4000);
            Console.WriteLine(acount.ToString());
            acount.SumLimit();
            acount.ChangePin();
            Console.WriteLine(acount.ToString());

        }
        private static void Blue_Message(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Red_Message(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
