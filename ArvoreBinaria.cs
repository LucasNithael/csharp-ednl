class ArvoreBinaria<T> where T : IArvore<T>, IComparable<T>{
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
            No<T>? atual = Raiz;
            No<T>? pai = Raiz;
            bool e_esquerda = true;

            while (atual != null && atual.Valor.CompareTo(valor) != 0){
                pai = atual;
                if(valor.CompareTo(atual.Valor) < 0){
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
                    pai.Esquerda = null;
                }else{
                    pai.Direita = null;
                }
            }

            // Caso 2: Nó não possui filho na direita
            else if(atual != null && atual.Direita == null){
                if(atual == Raiz){
                    Raiz = atual.Esquerda;
                }else if(e_esquerda){
                    pai.Esquerda = atual.Esquerda;
                }else{
                    pai.Direita = atual.Esquerda;
                }
            }

            // Caso 3: Nó não possui filho na esquerda
            else if(atual != null && atual.Esquerda == null){
                if(atual == Raiz){
                    Raiz = atual.Direita;
                }else if(e_esquerda){
                    pai.Esquerda = atual.Direita;
                }else{
                    pai.Direita = atual.Direita;
                }
            }

            //Caso 4: Nó possui dois filhos
            else{
                No<T>? sucessor = BuscarSucessor(atual.Valor);
                if(atual == Raiz){
                    Raiz = sucessor;
                }else if(e_esquerda){
                    pai.Esquerda = sucessor;
                }else{
                    pai.Direita = sucessor;
                }

                sucessor.Esquerda = atual.Esquerda;
            }

        }
        public void Mostrar(){

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
            return 0;
        }
        private No<T>? BuscarSucessor(T valor){
            var atual = Raiz;
            while (atual != null)
            {
            if (atual.Valor.CompareTo(valor) == 0)
            {
                return atual;
            }
            if (valor.CompareTo(atual.Valor) < 0)
            {
                atual = atual.Esquerda;
            }
            else
            {
                atual = atual.Direita;
            }
        }
        return null;
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