class ArvoreBinaria{
        public No? Raiz {get; set;} = null;
        public List<string> Ligacoes {get; set;} = new List<string>();
        public void Inserir(int valor){
            var novoNo = new No(valor);

            if(Raiz == null){
                Raiz = novoNo;
            }else{
                var atual = Raiz;
                while(true){
                    var pai = atual;
                    // Esquerda
                    if(valor < atual.Valor){
                        atual = atual.Esquerda;
                        if(atual == null){
                            pai.Esquerda = novoNo;
                            Ligacoes.Add($"{pai.Valor} -> {novoNo.Valor}");
                            return;
                        }
                    }
                    // Direita
                    else{
                        atual = atual.Direita;
                        if(atual == null){
                            pai.Direita = novoNo;
                            Ligacoes.Add($"{pai.Valor} -> {novoNo.Valor}");
                            return;
                        }
                    }
                }
        }
    }

        public No? Buscar(int valor){
            var atual = Raiz;
            while(atual != null){
                if(atual.Valor == valor){
                    return atual;
                }
                if(valor < atual.Valor){
                    atual = atual.Esquerda;
                }else{
                    atual = atual.Direita;
                }
            }
            return null;
        }

        // Pré-ordem
        public void ImprimirPreOrdem(No? no){
            if(no != null){
                Console.WriteLine(no.MostrarNo());
                ImprimirPreOrdem(no.Esquerda);
                ImprimirPreOrdem(no.Direita);
            }
        }

        // Em-ordem
        public void ImprimirEmOrdem(No? no){
            if(no != null){
                ImprimirEmOrdem(no.Esquerda);
                Console.WriteLine(no.MostrarNo());
                ImprimirEmOrdem(no.Direita);
            }
        }

        // Pós-ordem
        public void ImprimirPosOrdem(No? no){
            if(no != null){
                ImprimirPosOrdem(no.Esquerda);
                ImprimirPosOrdem(no.Direita);
                Console.WriteLine(no.MostrarNo());
            }
        }
        
        public void Imprimir(){
            Console.WriteLine($"Ligações: \n{string.Join("\n", Ligacoes)}");
        }
}


public class No(int valor){
    public int Valor {get; set;} = valor;
    public No? Esquerda {get; set;} = null;
    public No? Direita {get; set;} = null;

    public string MostrarNo(){
        return Valor.ToString();
    }
}