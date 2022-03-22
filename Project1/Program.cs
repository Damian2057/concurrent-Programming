using System;

namespace FirstProgram
{
    public class Program
    {
        public static string toTestMethod()
        {
            return "Test";
        }

        public static double add(double x, double y)
        {
            return x + y;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Dodaje dwie wartosci!");

            Console.WriteLine("Podaj pierwsza liczbe:");
            double x = Double.Parse(Console.ReadLine());

            Console.WriteLine("Podaj druga liczbe:");
            double y = Double.Parse(Console.ReadLine());

            Console.WriteLine("Wynik to:"+add(x,y));
        }
    }
}