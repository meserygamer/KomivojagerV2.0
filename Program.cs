namespace KomivojagerV2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            travelling_salesman_problem Vojager = new travelling_salesman_problem();
            Vojager.BodyOfMethod();
        }
    }
    class travelling_salesman_problem
    {
        protected uint[][] TableOfTransport; //Таблица для хранения стоимостей переходов между вершинами
        readonly int KolVersh;
        TreeNode[] Graphs;
        public travelling_salesman_problem()
        {
            Console.WriteLine("Введите количество вершин");
            KolVersh = Convert.ToInt32(Console.ReadLine());
            TableOfTransport = new uint[KolVersh][];
            ZapolnTranTable();
            Graphs = new TreeNode[KolVersh];
        }
        public void BodyOfMethod()
        {
            for (int i = 0; i < KolVersh; i++)
            {
                Graphs[i] = new TreeNode(i+1);
            }
        }
        private void ZapolnTranTable() //Для заполнения транспортной таблицы
        {
            for (int i = 0; i < KolVersh; i++)
            {
                Console.WriteLine($"Введите строку №{i + 1}");
                TableOfTransport[i] = Array.ConvertAll(Console.ReadLine().Split(" "), n => Convert.ToUInt32(n));
            }
        }
    }
}