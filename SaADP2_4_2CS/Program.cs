using System;

namespace SaADP2_4_1CS
{

    class Program
    {
        private const int m = 10;  //Размер массива
        private static int amount = 0;

        static void Main(string[] args)
        {
            string[] keys = { "Алексей", "Константин", "Евгений", "Ислам", "Данис", "Павел", "Егор", "Кирилл" };
            //string[] keys = { "WHILE", "AND", "RETURN", "STRUct", "OUT", "Ref", "FOREACH", "PROGRAM", "THEN", "Class" };
            string[] hashTable = new string[m];
            Interface(ref hashTable, ref keys);
        }

        public static int Hash(string key)
        {
            int code = 0;
            for (int i = 0; i < key.Length; i++)
            {
                code += (int)key[i];
            }
            return code % m;
        }

        public static void Add(ref string[] hashTable, int hash, string key, ref int compares)
        {
            compares++;
            if (hashTable[hash] == null)
            {
                hashTable[hash] = key;
                amount++;
                return;
            }
            compares++;
            if(hashTable[hash] == key)
            {
                return;
            }
            else
            {
                int index = SearchEmpty(hashTable, key, hash, ref compares);
                if (index == -1) { return; }
                hashTable[index] = key;
                amount++;
            }
        }

        public static int SearchEmpty(string[] hashTable, string key, int hash, ref int compares)
        {
            int j = -1;
            for (int i = 0; i < m - 2; i++)
            {
                j = ((hash + i) % m) + 1;
                if(j == m){ j = 0; }
                compares++;
                if (hashTable[j] == key) { return -1; }
                if (hashTable[j] == null)
                {
                    return j;
                }
            }
            return -1;
        }

        public static int Search(string[] hashTable, int hash, string key, ref int compares)
        {
            compares++;
            if (hashTable[hash] == null)
            {
                return -1;
            }
            else
            {
                compares++;
                if (hashTable[hash] == key)
                {
                    return hash;
                }
                else
                {
                    int j = -1;
                    for (int k = 0; k < m - 2; k++)
                    {
                        j = ((hash + k) % m) + 1;
                        if(j == m)
                        {
                            j = 0;
                        }
                        compares++;
                        if (hashTable[j] == null)
                        {
                            return -1;
                        }
                        compares++;
                        if(hashTable[j] == key)
                        {
                            return j;
                        }
                    }
                    return -1;
                }
            }
        }


        public static void CreateHashTable(ref string[] hashTable, string[] keys)
        {
            int hash;
            int compares = 0;
            int delta = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                hash = Hash(keys[i]);
                Add(ref hashTable, hash, keys[i], ref delta);
                compares += delta;
            }
            Console.WriteLine($"Количество сравнений при добавлении {keys.Length} элементов: {compares}" );
        }
        public static void PrintHashTable(string[] hashTable)
        {
            for (int i = 0; i < m; i++)
            {
                if(hashTable[i] != null)
                {
                    Console.Write($" | {i} - {hashTable[i]}");
                }
                
            }
            Console.WriteLine();
        }

        public static int Input()
        {
            string strInput; bool stop = false;
            int number = -1;
            while (!stop)
            {
                try
                {
                    strInput = Console.ReadLine();
                    number = int.Parse(strInput); stop = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Не верный ввод.");
                    stop = false;
                    break;
                }
            }
            return number;
        }

        public static void PrintMenu()
        {
            Console.WriteLine(
                 "1. Заполнить хеш таблицу.\n"
               + "2. Добавить ключ.\n"
               + "3. Вывести хеш таблицу на экран.\n"
               + "4. Найти ключ в хеш таблице.\n"
               + "5. Завершить работу.");
        }

        public static void CaseAdd(ref string[] hashTable)
        {
            if(amount != m)
            {
                int compares = 0;
                Console.Write("Введите ключ для добавления: ");
                string key = Console.ReadLine();
                
                int hash = Hash(key);
                Add(ref hashTable, hash, key, ref compares);
                Console.WriteLine($"Количество сравнений: {compares}");
            }
            else
            {
                Console.WriteLine("Таблица заполнена.");
            }
           
        }
        public static void CaseSearch(string[] hashTable)
        {
            if (!(amount == 0))
            {
                int compares = 0;
                Console.Write("Введите ключ для поиска: ");
                string key = Console.ReadLine();
                int hash = Hash(key);
                int index = Search(hashTable, hash, key, ref compares);
                if (index == -1) { Console.WriteLine($"Такого ключа нет. Количество сравнений: {compares}"); }
                else
                    Console.WriteLine($"Ключ {key} в хеш таблице имеет место {index}. Количесвто сравнений: {compares}");
            }
            else
                Console.WriteLine("Хеш таблица пуста.");
        }

        public static void ClearMemory(ref string[] hashTable, ref string[] keys)
        {
            for (int i = 0; i < m; i++)
            {
                hashTable[i] = null;
            }
            for (int j = 0; j < m - 2; j++)
            {
                keys[j] = null;
            }
            hashTable = keys = null;
        }

        public static void Interface(ref string[] hashTable, ref string[] keys)
        {
            bool stop = false; PrintMenu();
            while (!stop)
            {
                switch (Input())
                {
                    case 0:
                        PrintMenu();
                        break;
                    case 1:
                        CreateHashTable(ref hashTable, keys); Console.WriteLine("Хеш таблица заполнена.");
                        break;
                    case 2:
                        CaseAdd(ref hashTable);
                        break;
                    case 3:
                        PrintHashTable(hashTable);
                        break;
                    case 4:
                        CaseSearch(hashTable);
                        break;
                    case 5:
                        ClearMemory(ref hashTable, ref keys);
                        stop = true;
                        break;

                    default:
                        Console.WriteLine("Такого пункта меню нет.");
                        break;
                }
                Console.WriteLine("0. Показать меню.");
            }
        }
       

    }

}