using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    internal class CreditCard
    {

        string Number { get; set; }
        string Name { get; set; }
        DateTime OverdueDate { get; set; }
        int Pin { get; set; }
        double CreditLimit { get; set; }
        double Credit { get; set; }
        double Sum { get; set; }

        public CreditCard() { }
        public CreditCard(string number, string name, string overdueDate, int pin, double creditLimit, double sum)
        {
            Number = number;
            Name = name;
            OverdueDate = DateTime.Parse(overdueDate);
            Pin = pin;
            CreditLimit = creditLimit;
            Credit = creditLimit;
            Sum = sum;
        }
        //Реалізуйте події для наступних ситуацій:



        //■ Досягнення ліміту заданої суми грошей;
        //■ Зміна PIN.
        public double CurrentSum
        {
            get { return Sum; }
        }
        //■ Поповнення рахунку;
        public void Put(double sum)
        {
            if (sum <= (CreditLimit - Credit))
            {
                Credit += sum;
                del?.Invoke("Погашение кредита на сумму " + sum.ToString() + " гривен. Всего на счете " + Sum.ToString() + " гривен. Доступный кредит " + Credit.ToString() + " гривен");
            }
            else
            {
                sum -= (CreditLimit - Credit);
                Credit = CreditLimit;
                Sum += sum;
                del?.Invoke("Счет пополнен на сумму " + sum.ToString() + " гривен. Всего на счете " + Sum.ToString() + " гривен. Доступный кредит " + Credit.ToString() + " гривен");
            }
        }
        //■ Витрата коштів з рахунку;
        public void Withdraw(double sum)
        {
            if (sum <= Sum)
            {
                Sum -= sum;
                del?.Invoke("Сумма " + sum.ToString() + " гривен снята со счета. Всего на счете " + Sum.ToString() + " гривен");
            }
            else
            {
                del?.Invoke("Недостаточно денег на счете. Хотите использовать кредитные средства (Yes or No)?");
                if (Console.ReadLine().ToLower() == "yes")
                    UseCreditMoney(sum);
                else
                    del?.Invoke("Отмена операции снятия суммы " + sum.ToString() + " гривен. Недостаточно денег на счете.");
            }
        }
        //■ Старт використання кредитних коштів;
        public void UseCreditMoney(double credit)
        {
            if (Credit >= (credit - Sum))
            {
                Credit -= (credit - Sum);
                Sum = 0;
                del?.Invoke("Снятие суммы " + (credit - Sum).ToString() + " гривен c кредитного счета. Всего на счете " + Sum.ToString() + " гривен. Доступный кредит " + Credit.ToString() + " гривен");
            }
            else
                del?.Invoke("Отмена операции снятия суммы " + credit.ToString() + " гривен. Недостаточно денег на счете и кредите.");
        }
        //■ Досягнення ліміту заданої суми грошей;
        public void SumLimit()
        {
            del?.Invoke("Введите размер лимита: ");
            double sumlimit = Double.Parse(Console.ReadLine());
            if (Sum <= sumlimit)
            {
                del?.Invoke("Достигнут установленный лимит остатка на счете " + sumlimit.ToString() + " гривен. Всего на счете " + Sum.ToString() + " гривен");
            }
        }
        //■ Зміна PIN.
        public void ChangePin()
        {
            del?.Invoke("Смена ПИН кода. Введите новый ПИН: ");
            int newpin = Int32.Parse(Console.ReadLine());
            Pin = newpin;
            del?.Invoke("ПИН код изменен.");
        }


        public override string ToString()
        {
            return $"Card number: {Number}, Holder name: {Name}, Overdue date: {OverdueDate}, PIN: {Pin}, Credit Limit: {CreditLimit}, Sum: {Sum}";
        }

        // Объявляем делегат
        public delegate void AccountStateHandler(string message);
        // Создаем переменную делегата
        public AccountStateHandler del;

        // Регистрируем делегат
        public void RegisterHandler(AccountStateHandler _del)
        {
            del += _del; // добавляем делегат
        }

        // Отмена регистрации делегата
        public void UnregisterHandler(AccountStateHandler _del)
        {
            del -= _del; // удаляем делегат
        }
    }
}
