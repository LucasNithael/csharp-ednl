using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estrutura_de_dados_nao_linear.Grafos
{
    public class Vertice
    {
        public object obj { get; set; }
        public List<Aresta> ArestaIn { get; set; }
        public List<Aresta> ArestaOut { get; set; }

    }
}
