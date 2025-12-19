using System;
using System.Collections.Generic;
using System.Text;
using SQLSchool.Data;

namespace SQLSchool
{
    internal class Staff
    {
        public static void StaffMenu()
        {
            Console.WriteLine($"1. All Personal\n" +
                "2. Välj en peronal kategori\n" +
                "3. Återgå till Huvudmenyn");
            switch (Console.ReadLine())
                {
                case "1":
                    Console.Clear();
                    ViewAllStaff();
                    break;
                case "2":
                    Console.Clear();
                    ViewStaffByCategory();
                    break;
                case "3":
                    Console.Clear();
                    Menu.ReturnToMainMenu();
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    StaffMenu();
                    break;
            }
        }

        public static void ViewAllStaff()
        {
            using var context = new SQLSchoolDbContext();

            var staffMembers = context.Personals;
            foreach (var staff in staffMembers)
            {
                Console.WriteLine($"ID: {staff.PersonalId}, Namn: {staff.Namn}");
            }
            Menu.ReturnToMainMenu();
        }

        public static void ViewStaffByCategory()
        {
            using var context = new SQLSchoolDbContext();

            Console.WriteLine("Ange kategori:\n" +
                "Lärare/Administrator");
            string kategori = Console.ReadLine();
            var staffInCategory = context.Personals
                .Where(p => p.Befattning.Befattning1 == kategori);
            foreach (var staff in staffInCategory)
            {
                Console.WriteLine($"ID: {staff.PersonalId}, Namn: {staff.Namn}");
            }
            Menu.ReturnToMainMenu();
        }
    }
}
