using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomivojagerV2._0
{
    internal class TreeNode //Элемент графа
    {
        int IDDotInside; //Значение точки внутри
        TreeNode? Predok; //Родитель
        TreeNode[]? Children; //Потомки
        int Path; //Путь до этого узла
        int level; //Уровень в иерархии
        public TreeNode(int IDDotIside) //Конструктор для головы графа
        {
            this.IDDotInside = IDDotIside;
            Path = 0;
            level = 0;
        }
        public void PoiskPotomkov(uint[][] TableOfTransport)
        {

        }
        private List<int>? ListOfNextDot(int[] VectorOfDot)
        {

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
                if(MinMarsh is not null && VectorOfDot[i] > MinMarsh)
                {
                    MinMarsh = VectorOfDot[i];
                }
            }
            return MinMarsh;
        }
    }
}
