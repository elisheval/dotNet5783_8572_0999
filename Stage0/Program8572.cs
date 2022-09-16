//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome8572();
            Welcome0999();
            Console.ReadKey();
        }
         static partial void Welcome0999();

        private static void Welcome8572()
        {
            Console.Write("enter your name ");
            string userName = Console.ReadLine();
            Console.WriteLine(userName + " ,welcome to my first console application");
        }
    }
}
