using SQLSchool.Data;
using SQLSchool.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLSchool
{
    internal class Student
    {
        public static void StudentMenu() //Studentmeny för val
        {
            Console.WriteLine($"1. Se alla studenter\n" +
                "2. Se studenter i en klass\n" +
                "3. Lägg till en student");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    ViewAllStudents();
                    break;
                case "2":
                    Console.Clear();
                    ViewStudentsInClass();
                    break;
                case "3":
                    Console.Clear();
                    AddStudent();
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    StudentMenu();
                    break;
            }
        }

        public static void ViewAllStudents() //Visa alla studenter
        {
            using var context = new SQLSchoolDbContext();

            var students = context.Elevers;
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.ElevId}, Namn: {student.Namn}");
            }            
            Menu.ReturnToMainMenu();
        }

        public static void ViewStudentsInClass() //Visa studenter i en specifik klass
        {
            using var context = new SQLSchoolDbContext();

            Console.WriteLine("Ange klass:");
            string klass = Console.ReadLine();
            var studentsInClass = context.Elevers
                .Where(e => e.Klass.Namn == klass);
            foreach (var student in studentsInClass)
            {
                Console.WriteLine($"ID: {student.ElevId}, Namn: {student.Namn}");
            }
            Menu.ReturnToMainMenu();
        }

        public static void AddStudent() //Lägg till en student
        {
            using var context = new SQLSchoolDbContext();

            Console.Write("Ange elevens namn: ");
            string namn = Console.ReadLine();

            Console.Write("Ange personnummer (ÅÅMMDDXXXX): ");
            string personnummer = Console.ReadLine();

            // Visa klasser
            Console.WriteLine("\nTillgängliga klasser:");
            var classes = context.Klassers.ToList();
            foreach (var c in classes)
            {
                Console.WriteLine($"{c.KlassId}: {c.Namn}");
            }

            Console.Write("\nAnge KlassID: ");
            int klassId = int.Parse(Console.ReadLine());

            var student = new Elever
            {
                Namn = namn,
                Personnummer = personnummer,
                KlassId = klassId
            };

            context.Elevers.Add(student);
            context.SaveChanges();

            Console.WriteLine($"\nStudenten {namn} har lagts till i klass {klassId}.");
            Menu.ReturnToMainMenu();
        }


    }
}
