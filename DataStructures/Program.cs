using System;
using System.Collections.Generic;

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
        public static int PrintNewArray()
        {
            Array num = Array.CreateInstance(typeof(int), 6);
            num.SetValue(2, 0);
            num.SetValue(1, 1);
            num.SetValue(2, 2);
            num.SetValue(3, 3);
            num.SetValue(4, 4);
            num.SetValue(5, 5);
            int res = num.GetLength(0);
            return num.Length;
           
        }
        static void Main(string[] args)
        {
            var result = PrintNewArray();
            
           
           {
                Console.WriteLine(result);
                Console.WriteLine();
            }
        }
    }
}
