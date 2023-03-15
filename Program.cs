namespace KomivojagerV2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
    class travelling_salesman_problem
    {
        protected uint[][] TableOfTransport; //Таблица для хранения стоимостей переходов между вершинами
        readonly int KolVersh;
        public travelling_salesman_problem()
        {
            Console.WriteLine("Введите количество вершин");
            KolVersh = Convert.ToInt32(Console.ReadLine());
            TableOfTransport = new uint[KolVersh][];
            ZapolnTranTable();
        }
        public void BodyOfMethod()
        {
            for (int i = 0; i < KolVersh; i++)
            {
                List<int[]> ListOfPath = new List<int[]>();
                ListOfPath.Add(new int[KolVersh]);
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