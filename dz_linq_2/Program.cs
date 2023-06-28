using System;

namespace dz_linq_2
{
    class Phone
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public double Price { get; set; }
        public DateOnly Date { get; set; }

        public Phone(string name, string company,  double price, DateOnly date)
        {
            Name=name;
            Company=company;
            Price=price;
            Date=date;
        }

        public void ShowPhone()
        {
            Console.WriteLine($"{Name} {Company} {Price} {Date.Year}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Phone[] phones =
            {
                new Phone("IPhone8", "Apple", 15000, new DateOnly(2019,01,01)),
                new Phone("IPhone10", "Apple", 22000, new DateOnly(2021,08,01)),
                new Phone("IPhone12", "Apple", 30000, new DateOnly(2021,01,01)),
                new Phone("SamsungA1", "Samsung", 12000, new DateOnly(2016,01,01)),
                new Phone("SamsungA2", "Samsung", 15000, new DateOnly(2017,01,01)),
                new Phone("SamsungA3", "Samsung", 22000, new DateOnly(2019,01,01)),
                new Phone("SamsungA4", "Samsung", 30000, new DateOnly(2020,01,01)),
                new Phone("R1", "Redmi", 11000, new DateOnly(2018,01,01)),
                new Phone("R2", "Redmi", 15000, new DateOnly(2019,01,01)),
                new Phone("R2", "Redmi", 15500, new DateOnly(2019,01,01))
            };

            //  Посчитайте количество телефонов
            var q1 = from item in phones
                     select item;
            Console.WriteLine("\nКол-во телефонов: " + q1.Count());

            // Посчитайте количество телефонов с ценой больше 100
            var q2 = from item in phones
                     where item.Price>20000
                     group item by item.Name into g
                     select new { name = g.Key, count = g.Count() };
            Console.WriteLine("\nТелефоны с ценой больше 20000: " + q2.Count() + "\nНазвания: ");
            foreach (var gr in q2)
                Console.WriteLine(gr.name + " " + gr.count);

            // Посчитайте количество телефонов с ценой в диапазоне
            // от 15000 до 30000
            var q3 = phones.Where(x => x.Price>=15000 && x.Price<=30000).Count();
            Console.WriteLine("\nКол-во телефонов с ценой в диапазоне от 15000 до 30000: " + q3);

            // Посчитайте количество телефонов конкретного производителя
            Console.WriteLine("\nКол-во телефонов конкретного производителя");
            var q4 = from i in phones
                     group i by i.Company into g
                     select new { name = g.Key, count = g.Count() };
            foreach (var gr in q4)
                Console.WriteLine(gr.name + " " + gr.count);

            // Найдите телефон с минимальной ценой
            var q5 = phones.Min(x => x.Price);
            var min = phones.First(x => x.Price == q5);
            Console.WriteLine("\nМинимальная цена: " 
                + min.Company + " " + min.Name + " " + min.Price);

            // Найдите телефон с макс ценой
            var q6 = phones.Max(x => x.Price);
            var max = phones.Where(x => x.Price == q6);
            Console.WriteLine("\nМаксимальная цена: ");
            foreach (var i in max)
                i.ShowPhone();

            // Отобразите информацию о самом старом телефоне
            var q7 = phones.Min(x => x.Date);
            var q8 = phones.First(x => x.Date == q7);
            Console.WriteLine("\nСамый старый телефон: ");
            q8.ShowPhone();

            // Отобразите информацию о самом свежем телефоне
            var q9 = phones.Max(x => x.Date);
            var q10 = phones.First(x => x.Date == q9);
            Console.WriteLine("\nСамый новый телефон: ");
            q10.ShowPhone();

            // Найдите среднюю цену телефона
            double q11 = phones.Average(x => x.Price);
            Console.WriteLine("\nСредняя цена телефонов: " + q11.ToString("F2"));

            // Отобразите пять самых дорогих телефонов:
            Console.WriteLine("\n5 самых дорогих телефонов:");
            var q12 = phones.OrderByDescending(x => x.Price).Take(5);
            foreach (var i in q12)
                i.ShowPhone();

            // Отобразите пять самых дешевых телефонов:
            Console.WriteLine("\n5 самых дешевых телефонов:");
            var q13 = phones.OrderBy(x => x.Price).Take(5);
            foreach (var i in q13)
                i.ShowPhone();

            // Отобразите три самых старых телефона
            Console.WriteLine("\n3 самых старых телефона:");
            var q14 = phones.OrderBy(x => x.Date).Take(3);
            foreach (var i in q14)
                i.ShowPhone();

            // Отобразите три самых новых телефона
            Console.WriteLine("\n3 самых новых телефона:");
            var q15 = phones.OrderByDescending(x => x.Date).Take(3);
            foreach (var i in q15)
                i.ShowPhone();

            // Отобразите статистику по количеству телефонов каждого
            // производителя.Например: Sony – 3, Samsung – 4, Apple – 5 и т. д
            Console.WriteLine("\nСтатистика по количеству телефонов каждого производителя:");
            var q16 = from i in phones
                      group i by i.Company into g
                      select new { name = g.Key, count = g.Count() };
            foreach (var i in q16)
                Console.WriteLine(i.name + " " +  i.count);

            // Отобразите статистику по количеству моделей телефонов
            Console.WriteLine("\nСтатистика по количеству моделей телефонов:");
            var q17 = phones.GroupBy(x => x.Name).
                Select(g => new { name = g.Key, count = g.Count() });
            foreach (var i in q17)
                Console.WriteLine(i.name + " " +  i.count);

            // Отобразите статистику телефонов по годам
            Console.WriteLine("\nСтатистика телефонов по годам:");
            var q18 = phones.GroupBy(x => x.Date.Year).Select(g => new {n=g.Key, c=g.Count() });
            foreach (var i in q18)
                Console.WriteLine(i.n + " " +  i.c);
        }
    }
}