using System;

namespace Targil0
{
    class Program
    {
        static void Main(string[] args)
        {
            Welcome8034();
            Welcome3107();
            Console.ReadKey();
        }

        static partial void Welcome3107();
        private static void Welcome8034()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}