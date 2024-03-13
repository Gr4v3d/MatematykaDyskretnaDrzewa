using System.Threading.Tasks.Dataflow;

const int n = 8;
const int gr = 4;
bool[,] edges = new bool[n, n];
for(int i = 0; i < n; i++)
{
    for(int j = 0; j < n; j++)
    {
        edges[i, j] = false;
    }
}
List<int> T = new List<int>();
List<int> V = new List<int>();
Random rnd = new Random();


for (int i = 0; i < n; i++)
{
    V.Add(i+1);
}
for (int i = 0; i < n; i++)
{
    Console.WriteLine(V[i]);
}
int help = rnd.Next(n);
T.Add(V[help]);
V.RemoveAt(help);
help = rnd.Next(n-1);
T.Add(V[help]);
V.RemoveAt(help);
Console.WriteLine(join(T[0], T[1], edges));
bool result = false;
for(int i = 0; i < n-3; i++)
{
    result = false;
    do
    {
        int Vn = rnd.Next(V.Count);
        int Tn = rnd.Next(T.Count);
        result = join(T[Tn], V[Vn], edges);
        if (result)
        {
            T.Add(V[Vn]);
            V.RemoveAt(Vn);
        }
    } while (result == false);

}
do
{
    result = false;
    int Tn = rnd.Next(T.Count);
    result = join(T[Tn], V[0], edges);
    if (result)
    {
        T.Add(V[0]);
        V.RemoveAt(0);
    }
    

}while(result == false);
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        Console.Write((Convert.ToInt32(edges[i, j])).ToString() + '|');
    }
    Console.WriteLine();
}
/*Funkcja służąca do deklarowania edge'ów naszego drzewa, przymuje punkt T i punkt V a także Macierz edge'ów, 
 * sprawdza czy stopień punkta T jest wyższy od granicy, jak tak to robi returna z 0, jak nie, deklaruje połączenia w macierzy a potem zwraca true
 BOOL'owy ZWROT jest ważny bo pozwala na sprawdzenie czy połączenie w ogule zostało zrobione, czy nie trzeba powtarzać z innym punktem T */
bool join(int T, int V, bool[,] A)
{
    int deg = 0;
    for(int j = T; j > n; j++)
    {
        if (A[T-1,j]) { deg++; }
    }
    if(deg >= gr) { return false; }
    else
    {
        A[T - 1, V - 1] = true;
        A[V-1,T-1] = true;
        return true;
    }
}