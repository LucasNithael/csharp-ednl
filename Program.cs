//http://webgraphviz.com/

var a = new ArvoreAVL<int>();

List<int> lista = new List<int>(){10, 5, 15, 2, 8, 22, 21, 23};

foreach (var item in lista)
{
    a.Inserir(item);
}

foreach (var item in lista)
{
    a.VerificarNo(item);
    Console.WriteLine("=====================================");
}

a.EhAVL();