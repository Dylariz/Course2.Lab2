using System;
using System.Collections.Generic;

namespace Number8
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var random = new Random();
            var list = new List<int>();
            
            for (int i = 0; i < 30; i++)
            {
                list.Add(random.Next(0, 100));
            }

            var tree = new BinaryTree(list);
            
            
            Console.Write("Отсортированные значения дерева: ");
            foreach (var t in tree.GetTreeInOrder())
            {
                Console.Write(t + " ");
            }
            
            Console.WriteLine($"\nМаксимальная глубина: {tree.GetTreeHeight()}");
            Console.WriteLine($"Максимальная ширина: {tree.GetTreeWidth()}");
            
            Console.WriteLine("Дерево, целиком:");
            tree.Print();
            Console.ReadLine();
        }
    }
}