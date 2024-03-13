const int n = 8;
const int gr = 4;
bool[,] edges = new bool[n, n];
List<int> T = new List<int>();
List<int> V = new List<int>();
for(int i = 0; i < n; i++)
{
    V.Add(i+1);
}
for (int i = 0; i < n; i++)
{
    Console.WriteLine(V[i]);
}
Random rnd = new Random();

/*Funkcja służąca do deklarowania edge'ów naszego drzewa, przymuje punkt T i punkt V a także Macierz edge'ów, 
 * sprawdza czy stopień punkta T jest wyższy od granicy, jak tak to robi returna z 0, jak nie, deklaruje połączenia w macierzy a potem zwraca true
 BOOL'owy ZWROT jest ważny bo pozwala na sprawdzenie czy połączenie w ogule zostało zrobione czy nie */
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
        return true;pdgjofhposoihd
    }
}