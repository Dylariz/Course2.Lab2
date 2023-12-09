using System;
using System.Collections.Generic;

namespace Number11
{
    public class BinaryTree
    {
        /// <summary>
        /// Класс, описывающий узел дерева
        /// </summary>
        private class Node
        {
            public int Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(int value)
            {
                Value = value;
                Left = null;
                Right = null;
            }
        }

        /// <summary>
        /// Корень дерева
        /// </summary>
        private Node _root;

        /// <summary>
        /// Конструктор, создающий дерево из списка значений
        /// </summary>
        /// <param name="list">Список значений</param>
        public BinaryTree(List<int> list)
        {
            _root = null;
            foreach (var t in list)
            {
                Insert(t);
            }
        }
        
        /// <summary>
        /// Приватная перегрузка метода, вставляющего новый узел в дерево
        /// </summary>
        /// <param name="current">Стартовый узел рекурсии</param>
        /// <param name="value">Значение нового узла</param>
        /// <returns>Новый узел</returns>
        private Node Insert(Node current, int value)
        {
            if (current == null)
            {
                return new Node(value);
            }

            if (value < current.Value)
            {
                current.Left = Insert(current.Left, value);
            }
            else if (value > current.Value)
            {
                current.Right = Insert(current.Right, value);
            }

            return current;
        }

        /// <summary>
        /// Вставляет новый узел в дерево
        /// </summary>
        /// <param name="value">Значение нового узла</param>
        public void Insert(int value)
        {
            _root = Insert(_root, value);
        }
        
        /// <summary>
        /// Удаляет стартовый узел из дерева, таким образом отчищая его.
        /// </summary>
        public void Clear()
        {
            _root = null;
        }

        /// <summary>
        /// Приватная перегрузка метода, возвращающего все значения дерева в виде списка.
        /// </summary>
        /// <param name="current">Стартовый узел рекурсии</param>
        /// <param name="result">Результирующий список, необходимый для рекурсии</param>
        /// <returns>Список, содержащий все значения дерева.</returns>
        private List<int> GetTreeValues(Node current, List<int> result)
        {
            if (current == null)
            {
                return null;
            }

            GetTreeValues(current.Left, result);
            result.Add(current.Value);
            GetTreeValues(current.Right, result);

            return result;
        }

        /// <summary>
        /// Публичная перегрузка метода, возвращающего все значения дерева в виде списка.
        /// </summary>
        /// <returns>Список, содержащий все значения дерева.</returns>
        public List<int> GetTreeValues()
        {
            return GetTreeValues(_root, new List<int>());
        }
        
        /// <summary>
        /// Возвращает высоту дерева
        /// </summary>
        /// <param name="current">Стартовый узел рекурсии</param>
        /// <returns>Высота дерева</returns>
        private int GetHeight(Node current)
        {
            if (current == null)
            {
                return 0;
            }

            int leftHeight = GetHeight(current.Left);
            int rightHeight = GetHeight(current.Right);

            return leftHeight > rightHeight ? leftHeight + 1 : rightHeight + 1;
        }
        
        /// <summary>
        /// Приватная перегрузка метода, возвращающего все узлы указанного уровня в виде списка.
        /// </summary>
        /// <param name="current">Cтартовый узел рекурсии</param>
        /// <param name="level">Уровень</param>
        /// <param name="result">Результирующий список, необходимый для рекурсии</param>
        /// <returns>Список, содержащий все узлы указанного уровня.</returns>
        private List<int> GetTreeLevel(Node current, int level, List<int> result) 
        {
            if (current == null)
            {
                return null;
            }

            if (level == 1)
            {
                result.Add(current.Value);
            }
            else if (level > 1)
            {
                GetTreeLevel(current.Left, level - 1, result);
                GetTreeLevel(current.Right, level - 1, result);
            }

            return result;
        }
        
        /// <summary>
        /// Публичная перегрузка метода, возвращающего все узлы указанного уровня в виде списка.
        /// </summary>
        /// <param name="level">Уровень</param>
        /// <returns>Список, содержащий все узлы указанного уровня.</returns>
        public List<int> GetTreeLevel(int level)
        {
            return GetTreeLevel(_root, level, new List<int>());
        }
        
        /// <summary>
        /// Возвращает количество узлов на указанном уровне.
        /// </summary>
        /// <param name="current">Стартовый узел рекурсии</param>
        /// <param name="level">Уровень</param>
        /// <returns></returns>
        private int GetNodeCountAtLevel(Node current, int level)
        {
            if (current == null)
                return 0;

            if (level == 1)
                return 1;

            if (level > 1)
                return GetNodeCountAtLevel(current.Left, level - 1) + GetNodeCountAtLevel(current.Right, level - 1);

            return 0;
        }
        
        /// <summary>
        /// Возвращает список, содержащий количество узлов на каждом уровне.
        /// </summary>
        /// <returns>Список, содержащий количество узлов на каждом уровне.</returns>
        public List<int> GetNodeCountByLevel()
        {
            var height = GetHeight(_root);
            var result = new List<int>();
            for (int i = 1; i <= height; i++)
            {
                result.Add(GetNodeCountAtLevel(_root, i));
            }
            
            return result;
        }
        
        // Удалить, все что ниже!
        #region ConsolePrint

        private class NodeInfo
        {
            public Node Node;
            public string Text;
            public int StartPos;
            public int Size => Text.Length;

            public int EndPos
            {
                get => StartPos + Size;
                set => StartPos = value - Size;
            }

            public NodeInfo Parent, Left, Right;
        }
        
        public void Print(int topMargin = 1, int leftMargin = 2)
        {
            Print(_root, topMargin, leftMargin);
        }

        private void Print(Node current, int topMargin, int leftMargin)
        {
            if (current == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = current;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { Node = next, Text = next.Value.ToString(" 0 ") };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + 1;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }

                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.Left)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos);
                    }
                }

                next = next.Left ?? next.Right;
                for (; next == null; item = item.Parent)
                {
                    Print(item, rootTop + 2 * level);
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos;
                        next = item.Parent.Node.Right;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos;
                        else
                            item.Parent.StartPos += (item.StartPos - item.Parent.EndPos) / 2;
                    }
                }
            }

            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private void Print(NodeInfo item, int top)
        {
            SwapColors();
            Print(item.Text, top, item.StartPos);
            SwapColors();
            if (item.Left != null)
                PrintLink(top + 1, "┌", "┘", item.Left.StartPos + item.Left.Size / 2, item.StartPos);
            if (item.Right != null)
                PrintLink(top + 1, "└", "┐", item.EndPos - 1, item.Right.StartPos + item.Right.Size / 2);
        }

        private void PrintLink(int top, string start, string end, int startPos, int endPos)
        {
            Print(start, top, startPos);
            Print("─", top, startPos + 1, endPos);
            Print(end, top, endPos);
        }

        private void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }

        private void SwapColors()
        {
            (Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);
        }

        #endregion
    }
}