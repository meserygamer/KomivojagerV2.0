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
        int Path; //Путь до этого узла
        int level; //Уровень в иерархии
        public TreeNode(int IDDotIside, int[][] TableOfTransport) //Конструктор для головы графа
        {
            this.IDDotInside = IDDotIside;
            Path = 0;
            level = 0;
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
            if(NextDot is not null)
            {
                Children = new TreeNode[NextDot.Count];
                for(int i = 0; i < NextDot.Count; i++)
                {
                    Children[i] = new TreeNode(NextDot[i], this, TableOfTransport);
                }
            }
        }
        private List<int>? ListOfNextDot(int[] VectorOfDot) //Возвращает список потомков
        {
            List<int> ListOfMaybeDot, ListOfMaybeDotCopy; //Список возможных потомков (Он изменяется при работе), и его копия(Не изменяется, нужна чтобы проверить все точки и вычеркнуть из оригинала)
            if(Min(VectorOfDot) is not null) //Если предполагаемые точки существуют, то идём дальше, иначе возвращаем null, так как граф закончен
            {
                ListOfMaybeDot = PoiskDotSMinMarsh((int)Min(VectorOfDot), VectorOfDot); //Составляем набор точек
                ListOfMaybeDotCopy = new List<int>(ListOfMaybeDot);
                foreach (var i in ListOfMaybeDotCopy) //Проходим по всем предполагаемым точкам
                {
                    if(ToBeDot(this, i)) //Если точка уже встречалась вычёркиваем
                    {
                        ListOfMaybeDot.Remove(i);
                        VectorOfDot[(i - 1)] = 0;
                    }
                }
                if (ListOfMaybeDot.Count == 0) return ListOfNextDot(VectorOfDot);
                else return ListOfMaybeDot;
            }
            return null;
        }
        private List<int> PoiskDotSMinMarsh(int Min, int[] VectorOfDot) //Возвращает набор точек с минимальным маршрутом
        {
            List<int> list = new List<int>();
            for(int i = 0; i < VectorOfDot.Length; i++)
            {
                if (VectorOfDot[i] == Min)
                {
                    list.Add(i+1);
                }
            }
            return list;
        }
        private int? Min(int[] VectorOfDot) //Поиск минимального маршрута
        {
            int? MinMarsh = null; 
            for (int i = 0; i < VectorOfDot.Length; i++)
            {
                if(MinMarsh is null && VectorOfDot[i] != 0)
                {
                    MinMarsh = VectorOfDot[i];
                }
                if(MinMarsh is not null && VectorOfDot[i] != 0 && VectorOfDot[i] < MinMarsh)
                {
                    MinMarsh = VectorOfDot[i];
                }
            }
            return MinMarsh;
        }
        private bool ToBeDot(TreeNode Node, int ID)
        {
            if(Node.IDDotInside == ID)
            {
                return true;
            }
            if(Node.level == 0)
            {
                return false;
            }
            return ToBeDot(Node.Predok, ID);
        }
    }
}
