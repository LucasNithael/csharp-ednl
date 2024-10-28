//http://webgraphviz.com/

var a = new ArvoreBinaria();
a.Inserir(10);
a.Inserir(5);
a.Inserir(15);
a.Inserir(3);
a.Inserir(7);
a.Inserir(12);
a.Inserir(17);
a.Inserir(1);

// var no = a.Buscar(15);
// Console.WriteLine(no?.MostrarNo());

a.ImprimirPreOrdem(a.Raiz);
//a.Imprimir();