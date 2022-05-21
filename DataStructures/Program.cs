using System;

namespace DataStructures
{
    public class Program
    {
        public static int[] PrintArray()
        {
            int[] numbers = new int[4 ];
            numbers[0] = 10;
            numbers[1] = 20;
            numbers[2] = 30;
            numbers[3] = 40;
            return numbers;
        }
        static void Main(string[] args)
        {
            var result = PrintArray();
            foreach(int i in result)
            {
                Console.WriteLine(i);
            }
        }
    }
}
