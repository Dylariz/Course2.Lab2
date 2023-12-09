using System;
using System.Collections.Generic;

namespace Number11
{
    /// <summary>
    /// 11. Этажи
    /// Реализовать метод, выводящий количество вершин на каждом из уровней дерева..
    /// Повышенная сложность  Реализовать метод, возвращающий все узлы указанного уровня в виде списка.
    /// </summary>
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
            foreach (var t in tree.GetTreeValues())
            {
                Console.Write(t + " ");
            }
            
            var counter = 1;
            Console.WriteLine("\nКоличество узлов на каждом уровне:");
            foreach (var t in tree.GetNodeCountByLevel())
            {
                Console.WriteLine($"{counter++}-й уровень: {t}");
            }
            
            Console.Write("\nВведите уровень дерева, который хотите вывести: ");
            var level = int.Parse(Console.ReadLine());
            
            Console.Write($"Узлы {level}-го уровеня: ");
            foreach (var t in tree.GetTreeLevel(level))
            {
                Console.Write(t + " ");
            }
            
            Console.WriteLine("\nДерево, целиком:"); // Удалить!
            tree.Print(); // Удалить!
            Console.ReadLine(); // Удалить!
        }
    }
}