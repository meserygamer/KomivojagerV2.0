﻿namespace KomivojagerV2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*List<int> ints = new List<int>() {1,3};
            ints.Remove(1);
            ints.Remove(3);
            Console.WriteLine($"Count:{ints.Count} Capacity:{ints.Capacity}");*/
            travelling_salesman_problem Vojager = new travelling_salesman_problem();
            Vojager.BodyOfMethod();
        }
    }
    class travelling_salesman_problem
    {
        protected int[][] TableOfTransport; //Таблица для хранения стоимостей переходов между вершинами
        readonly int KolVersh;
        TreeNode[] Graphs;
        public travelling_salesman_problem()
        {
            Console.WriteLine("Введите количество вершин");
            KolVersh = Convert.ToInt32(Console.ReadLine());
            TableOfTransport = new int[KolVersh][];
            ZapolnTranTable();
            Graphs = new TreeNode[KolVersh];
        }
        public void BodyOfMethod() //Тело метода
        {
            int min = 0;
            //TreeNode.TableOfTransport = this.TableOfTransport;
            for (int i = 0; i < KolVersh; i++)
            {
                Graphs[i] = new TreeNode(i+1, TableOfTransport);
                if (Graphs[i].Target < Graphs[min].Target) min = i;
            }
            Console.WriteLine($"Длинна кротчайшего маршрута: {Graphs[min].Target}");
            string Marsh = "";
            foreach(var i in Graphs[min].TargetWay)
            {
                Marsh = Marsh + i + "-";
            }
            Marsh = Marsh[0..(Marsh.Length - 1)];
            Console.WriteLine($"Кротчайший маршрут: {Marsh}");
        }
        private void ZapolnTranTable() //Для заполнения транспортной таблицы
        {
            for (int i = 0; i < KolVersh; i++)
            {
                Console.WriteLine($"Введите строку №{i + 1}");
                TableOfTransport[i] = Array.ConvertAll(Console.ReadLine().Split(" "), n => Convert.ToInt32(n));
            }
        }
    }
}