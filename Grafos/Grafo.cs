namespace estrutura_de_dados_nao_linear.Grafos
{
    public class Grafo
    {
        public List<Vertice> Vertices { get; set; }
        public List<Aresta> Arestas { get; set; }

        public Grafo()
        {
            Vertices = new List<Vertice>();
            Arestas = new List<Aresta>();
        }

        public List<Vertice> FinalVertices(Aresta aresta)
        {
            List<Vertice> vertices = new List<Vertice>();
            vertices.Add(aresta.VerticeIn);
            vertices.Add(aresta.VerticeOut);
            return vertices;
        }
        
        public Vertice Oposto(Vertice vertice, Aresta aresta)
        {
            if (aresta.VerticeIn == vertice)
            {
                return aresta.VerticeOut;
            }
            else if (aresta.VerticeOut == vertice)
            {
                return aresta.VerticeIn;
            }
            
            throw new Exception("Vértice não pertence a aresta");
        }

        public bool EhAdjacente(Vertice v, Vertice w)
        {
            foreach (Aresta aresta in Arestas)
            {
                if ((aresta.VerticeIn == v && aresta.VerticeOut == w) || (aresta.VerticeIn == w && aresta.VerticeOut == v))
                {
                    return true;
                }
            }

            throw new Exception("Vértices não são adjacentes");
        }

        public Vertice Substituir(Vertice v, object o)
        {
            v.obj = o;
            return v;
        }
        
        public Aresta Substituir(Aresta a, object o)
        {
            a.obj = o;
            return a;
        }

        public Vertice InserirVertice(object vertice)
        {
            Vertice v = new Vertice();
            v.obj = vertice;
            Vertices.Add(v);
            return v;
        }

        // Aresta não dirigida
        public Aresta InserirAresta(Vertice v, Vertice w, object o)
        {
            Aresta a = new Aresta
            {
                obj = o,
                VerticeIn = v,
                VerticeOut = w
            };

            v.ArestaOut.Add(a);
            v.ArestaIn.Add(a);
            w.ArestaOut.Add(a);
            w.ArestaIn.Add(a);

            Arestas.Add(a);

            return a;
        }

        public object RemoverVertice(Vertice vertice)
        {
            var arestasIn = vertice.ArestaIn;
            var arestasOut = vertice.ArestaOut;

            foreach (Aresta aresta in arestasIn)
            {
                RemoverAresta(aresta);
            }
            foreach (Aresta aresta in arestasOut)
            {
                RemoverAresta(aresta);
            }

           return vertice.obj;
        }
        public object RemoverAresta(Aresta aresta)
        {
            Vertice v = aresta.VerticeIn;
            Vertice w = aresta.VerticeOut;

            v.ArestaOut.Remove(aresta);
            v.ArestaIn.Remove(aresta);
            w.ArestaOut.Remove(aresta);
            w.ArestaIn.Remove(aresta);

            return aresta.obj;
        }

        public List<Vertice> GetVizinhos(Vertice vertice)
        {
            List<Vertice> vizinhos = new List<Vertice>();
            foreach (Aresta aresta in Arestas)
            {
                if (aresta.VerticeIn == vertice)
                {
                    vizinhos.Add(aresta.VerticeOut);
                }
                else if (aresta.VerticeOut == vertice)
                {
                    vizinhos.Add(aresta.VerticeIn);
                }
            }
            return vizinhos;
        }

        public void Mostrar()
        {
            foreach (Vertice vertice in Vertices)
            {
                Console.WriteLine("Vertice: " + vertice.obj);
                Console.WriteLine("Arestas de saída: ");
                foreach (Aresta aresta in vertice.ArestaOut)
                {
                    Console.WriteLine("  " + aresta.obj);
                }
                Console.WriteLine("Arestas de entrada: ");
                foreach (Aresta aresta in vertice.ArestaIn)
                {
                    Console.WriteLine("  " + aresta.obj);
                }
            }
        }
    }
}
