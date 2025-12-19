using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client;
using SQLSchool.Data;

namespace SQLSchool
{
    internal class Menu
    {

        public static void MainMenu() //Huvudmeny för val av Student eller Personal
        {
            using var context = new SQLSchoolDbContext();

            Console.WriteLine($"Gör något av följande val:\n" +
                "1. Studenter\n" +
                "2. Personal");
            switch
                (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Student.StudentMenu();
                    break;
                case "2":
                    Console.Clear();
                    Staff.StaffMenu();
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    MainMenu();
                    break;
            }            
        }
        
        public static void ReturnToMainMenu() //Metod för att återgå till huvudmenyn
        {
            Console.WriteLine("Tryck på valfri knapp för att återgå till Huvudmenyn.");
            Console.ReadKey();
            MainMenu();
        }
    }
}
