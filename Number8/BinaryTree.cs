using System;
using System.Collections.Generic;

namespace Number8
{
    public class BinaryTree
    {
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

        class NodeInfo
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

        private Node _root;

        public BinaryTree()
        {
            _root = null;
        }

        public BinaryTree(List<int> list)
        {
            _root = null;
            foreach (var t in list)
            {
                Insert(t);
            }
        }


        public void Clear()
        {
            _root = null;
        }

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

        public void Insert(int value)
        {
            _root = Insert(_root, value);
        }

        private List<int> GetTreeInOrder(Node current, List<int> result)
        {
            if (current == null)
            {
                return null;
            }

            GetTreeInOrder(current.Left, result);
            result.Add(current.Value);
            GetTreeInOrder(current.Right, result);


            return result;
        }

        public List<int> GetTreeInOrder()
        {
            return GetTreeInOrder(_root, new List<int>());
        }

        private int GetTreeHeight(Node current)
        {
            if (current == null)
            {
                return 0;
            }

            int leftHeight = GetTreeHeight(current.Left);
            int rightHeight = GetTreeHeight(current.Right);

            return leftHeight > rightHeight ? leftHeight + 1 : rightHeight + 1;
        }

        public int GetTreeHeight()
        {
            return GetTreeHeight(_root);
        }

        private int GetTreeWidth(Node current, int level)
        {
            if (current == null)
            {
                return 0;
            }

            if (level == 1)
            {
                return 1;
            }

            if (level > 1)
            {
                return GetTreeWidth(current.Left, level - 1) + GetTreeWidth(current.Right, level - 1);
            }

            return 0;
        }

        public int GetTreeWidth()
        {
            int maxWidth = 0;
            int height = GetTreeHeight(_root);

            for (int i = 1; i <= height; i++)
            {
                var width = GetTreeWidth(_root, i);
                if (width > maxWidth)
                {
                    maxWidth = width;
                }
            }

            return maxWidth;
        }

        #region ConsolePrint

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