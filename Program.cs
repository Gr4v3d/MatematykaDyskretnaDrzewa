using System.IO;
using System.Threading.Tasks.Dataflow;
//Ograniczenia
const int n = 8;
//Maksymalny stopień
const int gr = 5; //mamy mieć granice 5 i 6
//Set-up
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

//Etap 1
int help = rnd.Next(n);
T.Add(V[help]);
V.RemoveAt(help);
help = rnd.Next(n-1);
T.Add(V[help]);
V.RemoveAt(help);
join(T[0], T[1], edges);

//Etap 2
int ErrorCount = 0;
bool result = false;
for(int i = 0; i < n-3; i++)
{
    result = false;
    do
    {
        int Vn = rnd.Next(V.Count);
        int Tn = rnd.Next(T.Count);
        result = join(T[Tn], V[Vn], edges);
        if (result == true)
        {
            T.Add(V[Vn]);
            V.RemoveAt(Vn);
        } 
        else { ErrorCount++; }
    } while (result == false);

}
//Etap 3
do
{
    result = false;
    int Tn = rnd.Next(T.Count);
    result = join(T[Tn], V[0], edges);
    if (result==true)
    {
        T.Add(V[0]);
        V.RemoveAt(0);
    }
    else { ErrorCount++; }
    

}while(result == false);

string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MDDrzewoWynik.txt";
using (StreamWriter streamWriter = new StreamWriter(path, true))
{
    streamWriter.Write(Archiwizuj(edges, ErrorCount));
}
Console.WriteLine(Archiwizuj(edges,ErrorCount));

/*Funkcja służąca do deklarowania krawędzi naszego drzewa, przymuje punkt T i punkt V a także Macierz krawędzi, 
 * sprawdza czy stopień punkta T jest wyższy od granicy, zwraca false, jeśli nie, deklaruje połączenia w macierzy a potem zwraca true*/
bool join(int T, int V, bool[,] A)
{
    int deg = 0;
    for(int j = 0; j < n; j++)
    {
        if (A[T-1,j]==true) { deg++; }
    }
    if(deg < gr - 1)
    {
        A[T - 1, V - 1] = true;
        A[V - 1, T - 1] = true;
        return true;
    }
    else
    {
        return false;
    }
}
/*Funkcja służąca do stworzenia łancucha używanego do zapisu wyniku do pliku, a także wyświetlania go w ekranie konsoli*/
string Archiwizuj(bool[,] edges, int ErCount)
{
    string zapis = "";
    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MDDrzewoWynik.txt";
    if (!File.Exists(path))
    {
        using (FileStream fs = File.Create(path))
        zapis += "UBB WBMiI Informatyka Stacjonarne 4 Semestr Grupa 2a\nMateusz Piegzik\nOliwia Nieradzik\nJakub Lis\nPiotr Owczorz\nTemat Projektu: Drzewa";
    }
    zapis+=$"\n\n{DateTime.Now}\nMacierz krawędzi:\n |";
    for (int i = 0; i < n; i++)
    {
        zapis+=$"{i + 1}|";
    }
    zapis += "\n";
    for (int i = 0; i < n; i++)
    {
        zapis+=$"{i + 1}|";
        for (int j = 0; j < n; j++)
        {
           zapis+= (Convert.ToInt32(edges[i, j])).ToString() + '|';
        }
        zapis += "\n";
    }
    //Ciąg Wag
    zapis += "\nCiąg wag punktów";
    List<int> deg = new List<int>();
    for (int i = 0; i < n; i++)
    {
        int count = 0;
        for (int j = 0; j < n; j++)
        {
            if (edges[i, j]) count++;
        }
        if (count != 0) deg.Add(count);
    }
    deg.Sort();
    deg.Reverse();
    string ciagDeg = "{ ";
    for (int i = 0; i < deg.Count; i++)
    {
        ciagDeg += $"{deg[i]}, ";
    }
    ciagDeg = ciagDeg.Remove(ciagDeg.Length - 2);
    ciagDeg += " }";
    zapis+="\n"+ciagDeg+"\n";
    //Zbiór Krawędzi
    double m = 0;
    zapis += "\nZbiór krawędzi:\n{";
    for (int i = 0; i < n; i++)
    {
        for (int j = i; j < n; j++)
        {
            if (edges[i, j])
            {
                m+= 1;
                zapis += "{" + (i+1) + "," + (j+1) + "} , ";
            };
        }
    }
    zapis = zapis.Remove(zapis.Length - 3);
    zapis += "}\n";
    //Gęstość Grafu
    var d = (double)((2 * (double)m) / ((double)n * (double)n - (double)n));
    zapis += $"\nGęstość Grafu wynosi: {d}\n";
    //Ilość błędów
    zapis += $"\nIlość \"Błędów\" podczas prób podłączenia punktu zbioru V pod punkt zbioru T: {ErCount}\n";
    for(int i = 0; i<30;i++) { zapis += "-"; }
    return zapis;
}