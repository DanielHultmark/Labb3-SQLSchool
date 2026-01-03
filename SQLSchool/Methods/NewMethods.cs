using Microsoft.EntityFrameworkCore;
using SQLSchool.Data;
using SQLSchool.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLSchool.Methods
{
    internal class NewMethods
    {
        //Metod för att sätta betyg på en elev i en kurs
        public static void SetGrade()
        {
            using var context = new SQLSchoolDbContext();            

            // Lista elever
            foreach (var e in context.Elevers)
                Console.WriteLine($"ElevID: {e.ElevId}, Namn: {e.Namn}");

            Console.Write("Ange ElevID: ");
            int elevId = int.Parse(Console.ReadLine());

            // Lista kurser
            foreach (var k in context.Kursers)
                Console.WriteLine($"KursID: {k.KursId}, Namn: {k.Namn}");

            Console.Write("Ange KursID: ");
            int kursId = int.Parse(Console.ReadLine());

            // Lista lärare
            foreach (var l in context.Personals)
                Console.WriteLine($"LärareID: {l.PersonalId}, Namn: {l.Namn}");

            Console.Write("Ange LärareID: ");
            int larareId = int.Parse(Console.ReadLine());

            Console.Write("Ange betyg (A–E): ");
            string betyg = Console.ReadLine().ToUpper();

            // Kontrollera att allt finns
            var elev = context.Elevers.FirstOrDefault(e => e.ElevId == elevId);
            var kurs = context.Kursers.FirstOrDefault(k => k.KursId == kursId);
            var larare = context.Personals.FirstOrDefault(l => l.PersonalId == larareId);

            if (elev == null || kurs == null || larare == null)
            {
                Console.WriteLine("Felaktigt ID angivet.");
                return;
            }

            // Nu börjar transaktionen
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var nyttBetyg = new Betyg
                {
                    ElevId = elevId,
                    KursId = kursId,
                    LärareId = larareId,
                    Betyg1 = betyg,
                    Datum = DateOnly.FromDateTime(DateTime.Now)
                };

                context.Betygs.Add(nyttBetyg);
                context.SaveChanges();
                transaction.Commit();

                Console.WriteLine("Betyg sparat!");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Fel: " + ex.Message);
            }

            Console.WriteLine($"Betyg {betyg} satt på elev {elev.Namn} i kurs {kurs.Namn}.");  
            Menu.ReturnToMainMenu();
        }

        //Metod för att visa alla aktiva kurser
        public static void ShowActiveCourses()
        {
            using var context = new SQLSchoolDbContext();

            var today = DateOnly.FromDateTime(DateTime.Now);

            var activeCourses = context.Kursers
                .Where(k => k.Slutdatum != null && k.Slutdatum > today)
                .ToList();

            Console.WriteLine("Aktiva kurser:");
            foreach (var kurs in activeCourses)
            {
                Console.WriteLine($"Namn: {kurs.Namn}, Slutar: {kurs.Slutdatum}");
            }
            Menu.ReturnToMainMenu();
        }
        //Räkna antal personal i varje avdelning
        public static void DepartmentStaffCount() 
        {
            using var context = new SQLSchoolDbContext();
            var departmentCounts = context.Personals
                .GroupBy(p => p.BefattningId)
                .Select(g => new
                {
                    AvdId = g.Key,
                    Count = g.Count()
                });
            foreach (var dept in departmentCounts)
            {
                Console.WriteLine($"Avdelning ID: {dept.AvdId}, Antal personal: {dept.Count}");
            }
            Menu.ReturnToMainMenu();
        }
        //Metod för att visa elever samt betyg i varje klass
        public static void ShowAllStudentsInfo()
        {
            using var context = new SQLSchoolDbContext();

            var students = context.Elevers
                .Include(e => e.Klass)
                .Include(e => e.Betygs)
                    .ThenInclude(b => b.Kurs)
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"\nStudent: {student.Namn}");

                // Klass
                string klassNamn = student.Klass != null ? student.Klass.Namn : "Ingen klass";
                Console.WriteLine($"Klass: {klassNamn}");

                // Kurser och betyg
                if (student.Betygs.Any())
                {
                    Console.WriteLine("Kurser och betyg:");
                    foreach (var betyg in student.Betygs)
                    {
                        Console.WriteLine($"  - {betyg.Kurs.Namn}: {betyg.Betyg1}");
                    }
                }
                else
                {
                    Console.WriteLine("Inga betyg registrerade.");
                }
            }
            Menu.ReturnToMainMenu();
        }


    }
}
