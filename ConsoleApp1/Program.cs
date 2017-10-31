using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = 5;
            A(out b);
            Console.WriteLine("Hello World!"+b);
            Console.ReadLine();
        }
        static void A(out int b)
        {
            b = 10;
        }
    }
}
