using System.Data;

class ArvoreBinaria<T> where T : IComparable<T>{
        public No<T>? Raiz {get; set;} = null;
        public void Inserir(T valor){
            var novoNo = new No<T>(valor);

            if(Raiz == null){
                Raiz = novoNo;
            }else{
                var atual = Raiz;
                while(true){
                    var pai = atual;
                    // Esquerda
                    if(valor.CompareTo(atual.Valor) < 0){
                        atual = atual.Esquerda;
                        if(atual == null){
                            pai.Esquerda = novoNo;
                            novoNo.Pai = pai;
                            return;
                        }
                    }
                    // Direita
                    else{
                        atual = atual.Direita;
                        if(atual == null){
                            pai.Direita = novoNo;
                            novoNo.Pai = pai;
                            return;
                        }
                    }
                }
            }
        }
        public No<T>? Buscar(T valor){
            var atual = Raiz;
            while(atual != null){
                if(atual.Valor.CompareTo(valor) == 0){
                    return atual;
                }
                if(valor.CompareTo(atual.Valor) < 0){
                    atual = atual.Esquerda;
                }else{
                    atual = atual.Direita;
                }
            }
            return null;
        }
        public void Remover(T valor){
            var no = Buscar(valor);
            
            if(no == null){
                throw new Exception("Valor não encontrado");
            }

            if(no == Raiz){
                Raiz = null;
                return;
            }

            var pai = no.Pai;

            // Caso 1: Nó folha
            if(no.Esquerda == null && no.Direita == null){
                if(no == Raiz)
                    Raiz = null;
                else if(no == pai.Esquerda)
                    pai.Esquerda = null;
                else
                    pai.Direita = null;
            }

            // Caso 2: Nó não possui filho na direita
            else if(no.Direita == null){
                if(no == Raiz){
                    Raiz = no.Esquerda;
                    Raiz.Pai = null;
                }
                else{
                    no.Esquerda.Pai = pai;
                    if(no == pai.Esquerda) 
                        pai.Esquerda = no.Esquerda;
                    else 
                        pai.Direita = no.Esquerda;
                }
            }

            // Caso 3: Nó não possui filho na esquerda
            else if(no.Esquerda == null){
                if(no == Raiz){
                    Raiz = no.Esquerda;
                    Raiz.Pai = null;
                }
                else{
                    no.Direita.Pai = pai;
                    if(no == pai.Esquerda) 
                        pai.Esquerda = no.Direita;
                    else 
                        pai.Direita = no.Direita;
                }
            }

            // Caso 4: Nó possui dois filhos
            else
            {
                var sucessor = BuscarSucessor(no);

                // Ajustar o pai do sucessor (remover o sucessor de sua posição original)
                if (sucessor.Pai != no)
                {
                    sucessor.Pai.Esquerda = sucessor.Direita;
                    if (sucessor.Direita != null)
                    {
                        sucessor.Direita.Pai = sucessor.Pai;
                    }
                }

                // Atualizar referências do sucessor
                sucessor.Esquerda = no.Esquerda;
                sucessor.Direita = no.Direita;

                if (no.Esquerda != null)
                {
                    no.Esquerda.Pai = sucessor;
                }
                if (no.Direita != null)
                {
                    no.Direita.Pai = sucessor;
                }

                // Atualizar referência do pai do nó removido
                if (no == Raiz)
                {
                    Raiz = sucessor;
                    Raiz.Pai = null;
                }
                else if (no == pai.Esquerda)
                {
                    pai.Esquerda = sucessor;
                }
                else
                {
                    pai.Direita = sucessor;
                }
                sucessor.Pai = pai;
            }

        }
        public void Mostrar(){
            if(Raiz == null){
                throw new Exception("Árvore vazia");
            } 
        }

        private No<T>? BuscarSucessor(No<T> no){
            var atual = no.Direita;
            while(atual != null && atual.Esquerda != null){
                atual = atual.Esquerda;
            }
            return atual;
        }
        public int Altura(No<T>? no){
            if (no == null) {
                return -1;
            }
            int alturaEsquerda = Altura(no.Esquerda);
            int alturaDireita = Altura(no.Direita);
            return Math.Max(alturaEsquerda, alturaDireita) + 1;
        }
        public int Profundidade(No<T>? no){
            int profundidade = 0;
            var atual = no;

            while(atual != null && atual.Pai != null){
                profundidade++;
                atual = atual.Pai;
            }

            return profundidade;
        }
        public int Tamanho(No<T>? no){
            if(no == null){
                return 0;
            }
            return 1 + Tamanho(no.Esquerda) + Tamanho(no.Direita);
        }
        // Pré-ordem
        public void ImprimirPreOrdem(No<T>? no){
            if(no != null){
                Console.WriteLine(no.Valor);
                ImprimirPreOrdem(no.Esquerda);
                ImprimirPreOrdem(no.Direita);
            }
        }
        // Em-ordem
        public void ImprimirEmOrdem(No<T>? no){
            if(no != null){
                ImprimirEmOrdem(no.Esquerda);
                Console.WriteLine(no.Valor);
                ImprimirEmOrdem(no.Direita);
            }
        }
        // Pós-ordem
        public void ImprimirPosOrdem(No<T>? no){
            if(no != null){
                ImprimirPosOrdem(no.Esquerda);
                ImprimirPosOrdem(no.Direita);
                Console.WriteLine(no.Valor);
            }
        }

}


public class No<T> where T : IComparable<T>{

    public No(T valor){
        Valor = valor;
    }
    public T Valor {get; set;}
    public No<T>? Esquerda {get; set;} = null;
    public No<T>? Direita {get; set;} = null;
    public No<T>? Pai {get; set;} = null;
}