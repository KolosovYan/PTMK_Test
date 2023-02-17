using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MyTestApp
{
    public static class Program
    {
        public static void Main()
        {
            using var db = new ApplicationContext();
            string[] words;
            CommandChecker();

            void CommandChecker()
            {
                Console.WriteLine("Введите команду: ");
                string? actual = Console.ReadLine();

                words = actual.Split(new char[] { ' ' });
                if (words.Length != 0)
                {
                    if (words[0] == "myApp")
                    {
                        switch (words[1])
                        {
                            case "1": db.CreateDB(); CommandChecker(); break;

                            case "2": if (words.Length >= 5)
                                {
                                    AddnSavePerson(CreatePerson()); CommandChecker(); break;
                                }
                                else
                                {
                                    Console.WriteLine("Не хватает аргументов, пожалуйста попробуйте снова"); CommandChecker(); break;
                                }

                            case "3" : SortAndSee(); CommandChecker(); break;

                            case "4" : Autofill(); CommandChecker(); break;

                            case "5" : GetMaleWithF(); CommandChecker(); break;

                            default: Console.WriteLine("Неизвестный номер команды, пожалуйста используйте номер 1-5"); CommandChecker(); break;
                        }
                    }

                    else
                    {
                        Console.WriteLine("Пожалуйста используйте myApp 1-5");
                        CommandChecker();
                    }
                }
            }


            Person CreatePerson()
            {
                Person ps = new Person(words[2], words[3], words[4]);
                return ps;
            }

            void AddnSavePerson(Person person)
            {
                db.Add(person);
                db.SaveChanges();
            }

            void SortAndSee()
            {
                var Persons = db.Persons.ToList().DistinctBy(c => c.FullName + c.DateOfBirth).OrderBy(p => p.FullName);
                foreach (Person p in Persons)
                {
                    Console.WriteLine($"{p.FullName} - {p.DateOfBirth} - {p.Gender} - {p.Age}");
                }
            }

            void Autofill()
            {
                string GetRandomChar()
                {
                    Random random = new Random();
                    char tmp = (char) random.Next('A', 'Z' + 1);
                    return tmp.ToString();
                }

                string GetRandomGender()
                {
                    Random random = new Random();
                    string[] Genders = { "Male", "Female" };
                    return Genders[random.Next(0, 2)];
                }

                for (int i = 0; i < 100; i++)
                {
                    Person go = new Person("Male", "F");
                    db.Add(go);
                }

                for (int i = 100; i < 1000000; i++)
                {
                    db.Add((new Person(GetRandomGender(), GetRandomChar())));
                }
                db.SaveChanges();
                Console.WriteLine("Database filled");
            }

            void GetMaleWithF()
            {
                var Persons = db.Persons.Where(p => p.FullName.Contains("F") && p.Gender.Equals("Male"));
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                foreach (Person p in Persons)
                {
                    Console.WriteLine($"{p.Id}.{p.FullName} - {p.DateOfBirth} - {p.Gender}");
                }
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds + " ms");
            }

            Console.ReadLine();
        }
    }
}