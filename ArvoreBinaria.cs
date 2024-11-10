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
        public void Remover(int valor){
            No? atual = Raiz;
            No? pai = Raiz;
            bool e_esquerda = true;

            while (atual != null && atual.Valor != valor){
                pai = atual;
                if(valor < atual.Valor){
                    e_esquerda = true;
                    atual = atual.Esquerda;
                }else{
                    e_esquerda = false;
                    atual = atual.Direita;
                }
                if(atual == null){
                    return;
                }
            }

            // Caso 1: Nó folha
            if(atual != null && atual.Esquerda == null && atual.Direita == null){
                if(atual == Raiz){
                    Raiz = null;
                }else if(e_esquerda){
                    Ligacoes.Remove($"{pai.Valor} -> {atual.Valor}");
                    pai.Esquerda = null;
                }else{
                    Ligacoes.Remove($"{pai.Valor} -> {atual.Valor}");
                    pai.Direita = null;
                }
            }

            // Caso 2: Nó não possui filho na direita
            else if(atual != null && atual.Direita == null){
                Ligacoes.Remove($"{pai.Valor} -> {atual.Valor}");
                Ligacoes.Remove($"{atual.Valor} -> {atual.Esquerda.Valor}");
                if(atual == Raiz){
                    Raiz = atual.Esquerda;
                    Ligacoes.Add($"{pai.Valor} -> {atual.Esquerda.Valor}");
                }else if(e_esquerda){
                    pai.Esquerda = atual.Esquerda;
                    Ligacoes.Add($"{pai.Valor} -> {atual.Esquerda.Valor}");
                }else{
                    pai.Direita = atual.Esquerda;
                    Ligacoes.Add($"{pai.Valor} -> {atual.Esquerda.Valor}");
                }
            }

            // Caso 3: Nó não possui filho na esquerda
            else if(atual != null && atual.Esquerda == null){
                Ligacoes.Remove($"{pai.Valor} -> {atual.Valor}");
                Ligacoes.Remove($"{atual.Valor} -> {atual.Direita.Valor}");
                if(atual == Raiz){
                    Raiz = atual.Direita;
                    Ligacoes.Add($"{pai.Valor} -> {atual.Direita.Valor}");
                }else if(e_esquerda){
                    pai.Esquerda = atual.Direita;
                    Ligacoes.Add($"{pai.Valor} -> {atual.Direita.Valor}");
                }else{
                    pai.Direita = atual.Direita;
                    Ligacoes.Add($"{pai.Valor} -> {atual.Direita.Valor}");
                }
            }

            //Caso 4: Nó possui dois filhos
            else{
                No? sucessor = BuscarSucessor(atual);
                Ligacoes.Remove($"{pai.Valor} -> {atual.Valor}");
                Ligacoes.Remove($"{atual.Valor} -> {sucessor.Valor}");
                Ligacoes.Remove($"{atual.Valor} -> {atual.Esquerda.Valor}");
                Ligacoes.Remove($"{atual.Valor} -> {atual.Direita.Valor}");
                if(atual == Raiz){
                    Ligacoes.Add($"{Raiz.Valor} -> {sucessor.Valor}");
                    Raiz = sucessor;
                }else if(e_esquerda){
                    Ligacoes.Add($"{pai.Valor} -> {sucessor.Valor}");
                    pai.Esquerda = sucessor;
                }else{
                    Ligacoes.Add($"{pai.Valor} -> {sucessor.Valor}");
                    pai.Direita = sucessor;
                }

                Ligacoes.Add($"{sucessor.Valor} -> {atual.Esquerda.Valor}");
                //Ligacoes.Add($"{sucessor.Valor} -> {atual.Direita.Valor}");

                sucessor.Esquerda = atual.Esquerda;
            }

        }

        private No? BuscarSucessor(No? no){
            No? paiSucessor = no;
            No? sucessor = no;
            No? atual = no.Direita;

            while(atual != null){
                paiSucessor = sucessor;
                sucessor = atual;
                atual = atual.Esquerda;
            }

            if(sucessor != no.Direita){
                paiSucessor.Esquerda = sucessor.Direita;
                sucessor.Direita = no.Direita;
            }

            return sucessor;
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