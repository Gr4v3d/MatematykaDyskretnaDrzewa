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
//Wypis na Terminal
UIexit(edges);
Archiwizuj(edges,ErrorCount);
/*Funkcja generująca ładny ekranik na końcu programu z macierzą, tymi potęgami i czymś jeszcze idk */
void UIexit(bool[,] edges)
{
    Console.Clear();
    //Przedstawienie Grupy
    Console.WriteLine("Informatyka Stacjonarne 4 Semestr Grupa 2a\nMateusz Piegzik\nOliwia Nieradzik\nJakub Lis\nPiotr Owczorz");
    //Przedstawienie macierzy 
    Console.WriteLine("Macierz krawędzi:");
    Console.Write("\n |");
    for (int i = 0; i < n; i++)
    {
        Console.Write($"{i + 1}|");
    }
    Console.WriteLine();
    for (int i = 0; i < n; i++)
    {
        Console.Write($"{i + 1}|");
        for (int j = 0; j < n; j++)
        {
            Console.Write((Convert.ToInt32(edges[i, j])).ToString() + '|');
        }
        Console.WriteLine();
    }
    Console.WriteLine("Ciąg wag punktów");
    List<int> deg = new List<int>();
    for (int i = 0; i < n; i++)
    {
        int count = 0;
        for (int j = i; j < n; j++)
        {
            if (edges[i, j]) count++;
        }
        if(count!=0)deg.Add(count);
    }
    deg.Sort();
    string ciagDeg = "{ ";
    for (int i = 0;i < deg.Count; i++) 
    {
        ciagDeg += $"{deg[i]}, ";
    }
    ciagDeg = ciagDeg.Remove(ciagDeg.Length-2);
    ciagDeg += " }";
    Console.WriteLine(ciagDeg);
}
/*Funkcja służąca do deklarowania edge'ów naszego drzewa, przymuje punkt T i punkt V a także Macierz edge'ów, 
 * sprawdza czy stopień punkta T jest wyższy od granicy, jak tak to robi returna z 0, jak nie, deklaruje połączenia w macierzy a potem zwraca true
 BOOL'owy ZWROT jest ważny bo pozwala na sprawdzenie czy połączenie w ogule zostało zrobione, czy nie trzeba powtarzać z innym punktem T */
bool join(int T, int V, bool[,] A)
{
    int deg = 0;
    for(int j = 0; j < n; j++)
    {
        if (A[T-1,j]==true) { deg++; }
    }
    if(deg < gr)
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
/*To samo co UIExit tylko tutaj wynik zapisuje do pliku MDDrzewoWynik.txt w folderze Dokumenty*/
void Archiwizuj(bool[,] edges, int ErCount)
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
        for (int j = i; j < n; j++)
        {
            if (edges[i, j]) count++;
        }
        if (count != 0) deg.Add(count);
    }
    deg.Sort();
    string ciagDeg = "{ ";
    for (int i = 0; i < deg.Count; i++)
    {
        ciagDeg += $"{deg[i]}, ";
    }
    ciagDeg = ciagDeg.Remove(ciagDeg.Length - 2);
    ciagDeg += " }";
    zapis+="\n"+ciagDeg+"\n";
    //Zbiór Krawędzi
    zapis += "\nZbiór krawędzi:\n{";
    for (int i = 0; i < n; i++)
    {
        for (int j = i; j < n; j++)
        {
            if (edges[i, j])
            {
                zapis += "{" + (i+1) + "," + (j+1) + "} , ";
            };
        }
    }
    zapis = zapis.Remove(zapis.Length - 3);
    zapis += "}\n";
    //Ilość błędów
    zapis += $"\n Ilość \"Błędów\" podczas prób podłączenia punktu zbioru V pod punkt zbioru T: {ErCount}\n";
    for(int i = 0; i<30;i++) { zapis += "-"; }
    using(StreamWriter streamWriter = new StreamWriter(path,true))
    {
        streamWriter.Write(zapis);
    }

}