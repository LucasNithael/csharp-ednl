
// Códigos de Testes

string[] linhas = File.ReadAllLines("TrabalhoGrafo/labirinto.dat");
int linhasLabirinto = linhas.Length;
int colunasLabirinto = linhas[0].Length;
int[,] matrizLabirinto = new int[linhasLabirinto, colunasLabirinto];
for (int i = 0; i < linhasLabirinto; i++)
{
    for (int j = 0; j < colunasLabirinto; j++)
    {
        matrizLabirinto[i, j] = int.Parse(linhas[i][j].ToString());
    }
}



var alg = new Algoritmos(matrizLabirinto);
 alg.MostrarMatriz();
// alg.MostrarSaidas();
// alg.MostrarPartida();


//tempo inicial
var tempoInicial = DateTime.Now;
var caminho = alg.Dijkstra();
//tempo final
var tempoFinal = DateTime.Now;
var tempoTotal = tempoFinal - tempoInicial;
Console.WriteLine($"Tempo total Dijkstra: {tempoTotal.TotalMilliseconds}ms"); // 5.0608ms

Console.WriteLine("Caminho Dijkstra:");
Console.Write("{");
foreach (var c in caminho)
{
    Console.Write($"({c.Item1}, {c.Item2}),");
}
Console.WriteLine("}");

//tempo inicial
tempoInicial = DateTime.Now;
var caminho2 = alg.Aestrela();
//tempo final
tempoFinal = DateTime.Now;
tempoTotal = tempoFinal - tempoInicial;
Console.WriteLine($"Tempo total Aestrela: {tempoTotal.TotalMilliseconds}ms"); // 5.9272ms

Console.WriteLine("Caminho Aestrela:");
Console.Write("{");
foreach (var c in caminho2)
{
    Console.Write($"({c.Item1}, {c.Item2}),");
}
Console.WriteLine("}");
