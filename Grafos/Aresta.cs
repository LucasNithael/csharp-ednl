using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estrutura_de_dados_nao_linear.Grafos
{
    public class Aresta
    {
        public object obj { get; set; }
        public Vertice VerticeIn { get; set; }
        public Vertice VerticeOut { get; set; }
    }
}
