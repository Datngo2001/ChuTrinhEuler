using System;
using System.Collections.Generic;

namespace ChuTrinhEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            const int max = 8;
            int[,] G = new int[,]
            {
                {0, 0, 0, 0, 1, 1, 0, 0},
                {0, 0, 0, 0, 1, 1, 0, 0},
                {0, 0, 0, 0, 1, 1, 0, 0},
                {0, 0, 0, 0, 1, 1, 1, 1},
                {1, 1, 1, 1, 0, 0, 0, 0},
                {1, 1, 1, 1, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0, 1},
                {0, 0, 0, 1, 0, 0, 1, 0},
            };

            // Khoi tao danh sach canh ke
            List<int>[] ke = new List<int>[max];
            for (int j = 0; j < max; j++)
            {
                ke[j] = new List<int>();
                for (int i = 0; i < max; i++)
                {
                    if (G[i, j] == 1)
                    {
                        ke[j].Add(i);
                    }
                }
            }

            // Kiem tra tinh lien thong
            int Sothplt = 0;
            bool[] chuaxet = new bool[max];
            for (int i = 0; i < max; i++)
            {
                chuaxet[i] = true;
            }
            for (int i = 0; i < max; i++)
            {
                if (chuaxet[i])
                {
                    Sothplt++;
                    BFS(i, ref chuaxet, ke);
                }
            }
            if(Sothplt > 1)
            {
                Console.WriteLine("Do thi ko lien thong!");
                return;
            }

            // Tim chu trinh EULER
            List<int> CE = new List<int>();

            Stack<int> STACK = new Stack<int>();

            if(isDirected(G, max) == true)
            {
                for (int i = 0; i < max; i++)
                {
                    if(TongDong(i, G, max) != 0)
                    {
                        Console.WriteLine("khong co chu trinh EULER ");
                        return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < max; i++)
                {
                    if (TongDong(i, G, max) % 2 != 0)
                    {
                        Console.WriteLine("khong co chu trinh EULER ");
                        return;
                    }
                }
            }

            //chong u là mot dinh bat ky cua do thi
            int u = 2;

            STACK.Push(u);

            while (STACK.Count != 0)
            {
                int x = STACK.Peek();
                if(ke[x].Count != 0)
                {
                    int y = ke[x][0];
                    STACK.Push(y);
                    //bo canh x y
                    ke[x].Remove(y);
                    ke[y].Remove(x);
                }
                else
                {
                    x = STACK.Pop();
                    CE.Add(x);
                }
            }
            for (int i = 0; i < CE.Count; i++)
            {
                Console.Write((CE[i] + 1) + " ");
            }
        }
        public static void BFS(int v, ref bool[] chuaxet, List<int>[] ke)
        {
            Queue<int> QUEUE = new Queue<int>();
            QUEUE.Enqueue(v);
            chuaxet[v] = false;
            while(QUEUE.Count != 0)
            {
                int p = QUEUE.Dequeue();
                for (int i = 0; i < ke[p].Count; i++)
                {
                    int u = ke[p][i];
                    if (chuaxet[u])
                    {
                        QUEUE.Enqueue(u);
                        chuaxet[u] = false;
                    }
                }
            }
        }
        public static bool isDirected(int[,] G, int max)
        {
            for (int i = 0; i < max-1; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if(G[i, j] != G[j, i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static int TongDong(int r, int[,] G, int max)
        {
            int sum = 0;
            for (int i = 0; i < max; i++)
            {
                sum += G[r, i];
            }
            return sum;
        }
    }
}
