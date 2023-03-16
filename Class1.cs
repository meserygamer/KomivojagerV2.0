using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomivojagerV2._0
{
    internal class TreeNode //Элемент графа
    {
        //public static uint[][] TableOfTransport; //Матрица путей
        int IDDotInside; //Значение точки внутри
        TreeNode? Predok; //Родитель
        TreeNode[]? Children; //Потомки
        public int? Target {get; private set;} //Длинна самого короткого пути
        public List<int>? TargetWay{get; private set;} //Самый короткий маршрут
        int Path; //Путь до этого узла
        int level; //Уровень в иерархии
        public TreeNode(int IDDotIside, int[][] TableOfTransport) //Конструктор для головы графа
        {
            this.IDDotInside = IDDotIside;
            Path = 0;
            level = 0;
            Target = null;
            Console.WriteLine($"Я Корень,Узел№{IDDotInside}, Path:{Path}");
            PoiskPotomkov(TableOfTransport);
        }
        public TreeNode(int IDDotIside, TreeNode Parent, int[][] TableOfTransport) //Конструктор для потомков
        {
            this.IDDotInside = IDDotIside;
            Path = Parent.Path + TableOfTransport[Parent.IDDotInside - 1][IDDotInside - 1];
            level = Parent.level + 1;
            Predok = Parent;
            Console.WriteLine($"Я узел, Узел№{IDDotInside}, Path:{Path}, Уровень:{level}");
            PoiskPotomkov(TableOfTransport);
        }
        public void PoiskPotomkov(int[][] TableOfTransport) //Ищет и создаёт потомков
        {
            List<int>? NextDot = ListOfNextDot(TableOfTransport[IDDotInside - 1]);
            if (NextDot is not null)
            {
                Children = new TreeNode[NextDot.Count];
                for (int i = 0; i < NextDot.Count; i++)
                {
                    Children[i] = new TreeNode(NextDot[i], this, TableOfTransport);
                }
            }
            else FindMinWay(this); //Для поиска минимального пути, когда маршрут закончился
        }
        private List<int>? ListOfNextDot(int[] VectorOfDot) //Возвращает список потомков
        {
            int[] VectorOfDotCopy = new int[VectorOfDot.Length]; //Создаём копию, так как все элементы зубчатого массива являются ссылками
            Array.Copy(VectorOfDot, VectorOfDotCopy, VectorOfDot.Length);
            List<int> ListOfMaybeDot, ListOfMaybeDotCopy; //Список возможных потомков (Он изменяется при работе), и его копия(Не изменяется, нужна чтобы проверить все точки и вычеркнуть из оригинала)
            if (Min(VectorOfDotCopy) is not null) //Если предполагаемые точки существуют, то идём дальше, иначе возвращаем null, так как граф закончен
            {
                ListOfMaybeDot = PoiskDotSMinMarsh((int)Min(VectorOfDotCopy), VectorOfDotCopy); //Составляем набор точек
                ListOfMaybeDotCopy = new List<int>(ListOfMaybeDot);
                foreach (var i in ListOfMaybeDotCopy) //Проходим по всем предполагаемым точкам
                {
                    if (ToBeDot(this, i)) //Если точка уже встречалась вычёркиваем
                    {
                        ListOfMaybeDot.Remove(i);
                        VectorOfDotCopy[(i - 1)] = 0;
                    }
                }
                if (ListOfMaybeDot.Count == 0) return ListOfNextDot(VectorOfDotCopy);
                else return ListOfMaybeDot;
            }
            return null;
        }
        private List<int> PoiskDotSMinMarsh(int Min, int[] VectorOfDot) //Возвращает набор точек с минимальным маршрутом
        {
            List<int> list = new List<int>();
            for (int i = 0; i < VectorOfDot.Length; i++)
            {
                if (VectorOfDot[i] == Min)
                {
                    list.Add(i + 1);
                }
            }
            return list;
        }
        private int? Min(int[] VectorOfDot) //Поиск минимального маршрута, возвращет минимальную длинну маршрута
        {
            int? MinMarsh = null;
            for (int i = 0; i < VectorOfDot.Length; i++)
            {
                if (MinMarsh is null && VectorOfDot[i] != 0)
                {
                    MinMarsh = VectorOfDot[i];
                }
                if (MinMarsh is not null && VectorOfDot[i] != 0 && VectorOfDot[i] < MinMarsh)
                {
                    MinMarsh = VectorOfDot[i];
                }
            }
            return MinMarsh;
        }
        private bool ToBeDot(TreeNode Node, int ID) //Проверка на наличие точки в цепи
        {
            if (Node.IDDotInside == ID)
            {
                return true;
            }
            if (Node.level == 0)
            {
                return false;
            }
            return ToBeDot(Node.Predok, ID);
        }
        private void FindMinWay(TreeNode Node) //Ищет самый короткий путь
        {
            List<int> TargWay = new List<int>(); //Кротчайший маршрут цепочки
            TreeNode Head = Unwinding(Node, TargWay); //Ищет головной элемент
            if (Head.Target is null)
            {
                Head.Target = Node.Path;
                Head.TargetWay = Enumerable.Reverse(TargWay).ToList(); //Переворачивает маршрут, так как метод возвращает из конца в начало, а нам нужен из начала в конец
                return;
            }
            if (Head.Target > Node.Path)
            {
                Head.Target = Node.Path;
                Head.TargetWay = Enumerable.Reverse(TargWay).ToList(); //Переворачивает маршрут, так как метод возвращает из конца в начало, а нам нужен из начала в конец
                return;
            }
        }
        private TreeNode Unwinding(TreeNode Node, List<int> Way) //Ищет головной элемент, попутно также простраивает маршрут до него из конца в начало
        {
            if (Node.level == 0)
            {
                Way.Add(Node.IDDotInside);
                return Node;
            }
            else
            {
                Way.Add(Node.IDDotInside);
                return Unwinding(Node.Predok, Way);
            }
        }
    }
}
