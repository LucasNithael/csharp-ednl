class ArvoreAVL<T> where T : IComparable<T>{
    public NoAVL<T>? Raiz {get; set;} = null;
    public void Mostrar(){
        if(Raiz == null){
            Console.WriteLine("Árvore vazia");
            return;
        }

        // Lista auxiliar para armazenar nós em ordem
        List<NoAVL<T>> lista = new List<NoAVL<T>>();
        EmOrdem(Raiz, lista);

        int altura = Altura(Raiz);
        int largura = lista.Count;

        // Matriz para armazenar os nós por nível
        string[,] matriz = new string[altura + 1, largura];

        // Popular a matriz com os valores dos nós
        for (int i = 0; i < lista.Count; i++)
        {
            NoAVL<T> no = lista[i];
            int profundidade = Profundidade(no);
            matriz[profundidade, i] = $"{no.Valor}[{ObterFB(no)}]";
        }

        // Exibir a matriz
        for (int i = 0; i <= altura; i++)
        {
            for (int j = 0; j < largura; j++)
            {
                if (matriz[i, j] == null)
                {
                    Console.Write("\t");
                }
                else
                {
                    Console.Write($"\t{matriz[i, j]}");
                }
            }
            Console.WriteLine();
        }
    }
    private void EmOrdem(NoAVL<T>? no, List<NoAVL<T>> lista){
        if(no != null){
            EmOrdem(no.Esquerda, lista);
            lista.Add(no);
            EmOrdem(no.Direita, lista);
        }
    }
    private int Altura(NoAVL<T>? no){
        return no == null ? -1 : no.Altura;
    }
    private int Profundidade(NoAVL<T>? no){
            int profundidade = 0;
            while (no != null && no != Raiz)
            {
                no = no.Pai;
                profundidade++;
            }
            return profundidade;
    }
    private int ObterFB(NoAVL<T>? no){
        return no == null ? 0 : Altura(no.Direita) - Altura(no.Esquerda);
    }
    private NoAVL<T> RotacaoDireita(NoAVL<T> y){
        NoAVL<T> x = y.Esquerda!;
        NoAVL<T> T2 = x.Direita!;

        // Realiza a rotação
        x.Direita = y;
        y.Esquerda = T2;

        // Atualiza as alturas
        y.Altura = Math.Max(Altura(y.Esquerda), Altura(y.Direita)) + 1;
        x.Altura = Math.Max(Altura(x.Esquerda), Altura(x.Direita)) + 1;

        // Retorna a nova raiz
        return x;
    }
    private NoAVL<T> RotacaoEsquerda(NoAVL<T> x){
        NoAVL<T> y = x.Direita!;
        NoAVL<T> T2 = y.Esquerda!;

        // Realiza a rotação
        y.Esquerda = x;
        x.Direita = T2;

        // Atualiza as alturas
        x.Altura = Math.Max(Altura(x.Esquerda), Altura(x.Direita)) + 1;
        y.Altura = Math.Max(Altura(y.Esquerda), Altura(y.Direita)) + 1;

        // Retorna a nova raiz
        return y;
    }
    private NoAVL<T> RotacaoEsquerdaDireita(NoAVL<T> no){
        no.Esquerda = RotacaoEsquerda(no.Esquerda!);
        return RotacaoDireita(no);
    }
    private NoAVL<T> RotacaoDireitaEsquerda(NoAVL<T> no){
        no.Direita = RotacaoDireita(no.Direita!);
        return RotacaoEsquerda(no);
    }
    private NoAVL<T> Inserir(NoAVL<T> no, T valor, NoAVL<T>? pai){
        // Passo 1: realiza a inserção normal de uma árvore binária de busca
        if(no == null){
            return new NoAVL<T>(valor);
        }

        if(valor.CompareTo(no.Valor) < 0){
            no.Esquerda = Inserir(no.Esquerda, valor, no);
        }else if(valor.CompareTo(no.Valor) > 0){
            no.Direita = Inserir(no.Direita, valor, no);
        }else{
            return no;
        }

        no.Altura = 1 + Math.Max(Altura(no.Esquerda), Altura(no.Direita));

        int fb = ObterFB(no);

        // Rotação simples à direita
        if(fb > 1 && valor.CompareTo(no.Direita!.Valor) > 0){
            return RotacaoEsquerda(no);
        }

        // Rotação simples à esquerda
        if(fb < -1 && valor.CompareTo(no.Esquerda!.Valor) < 0){
            return RotacaoDireita(no);
        }

        // Rotação dupla à direita
        if(fb > 1 && valor.CompareTo(no.Direita!.Valor) < 0){
            return RotacaoDireitaEsquerda(no);
        }

        // Rotação dupla à esquerda
        if(fb < -1 && valor.CompareTo(no.Esquerda!.Valor) > 0){
            return RotacaoEsquerdaDireita(no);
        }

        return no;
    }
    public void Inserir(T valor){
        Raiz = Inserir(Raiz, valor, null);
    }
    public NoAVL<T>? Remover(NoAVL<T> no, T valor){
        if(no == null){
            return null;
        }

        if(valor.CompareTo(no.Valor) < 0){
            no.Esquerda = Remover(no.Esquerda, valor);
        }else if(valor.CompareTo(no.Valor) > 0){
            no.Direita = Remover(no.Direita, valor);
        }else{
            if(no.Esquerda == null || no.Direita == null){
                NoAVL<T>? temp = null;
                if(temp == no.Esquerda){
                    temp = no.Direita;
                }else{
                    temp = no.Esquerda;
                }

                if(temp == null){
                    temp = no;
                    no = null;
                }else{
                    no = temp;
                }
            }else{
                NoAVL<T> temp = ObterMenor(no.Direita!);
                no.Valor = temp.Valor;
                no.Direita = Remover(no.Direita, temp.Valor);
            }
        }

        if(no == null){
            return null;
        }

        no.Altura = 1 + Math.Max(Altura(no.Esquerda), Altura(no.Direita));

        int fb = ObterFB(no);

        // Rotação simples à direita
        if(fb > 1 && ObterFB(no.Direita) >= 0){
            return RotacaoEsquerda(no);
        }

        // Rotação simples à esquerda
        if(fb < -1 && ObterFB(no.Esquerda) <= 0){
            return RotacaoDireita(no);
        }

        // Rotação dupla à direita
        if(fb > 1 && ObterFB(no.Direita) < 0){
            return RotacaoDireitaEsquerda(no);
        }

        // Rotação dupla à esquerda
        if(fb < -1 && ObterFB(no.Esquerda) > 0){
            return RotacaoEsquerdaDireita(no);
        }

        return no;
    }
    public void Remover(T valor){
        Raiz = Remover(Raiz, valor);
    }
    private NoAVL<T> ObterMenor(NoAVL<T> no){
        NoAVL<T> atual = no;
        while(atual.Esquerda != null){
            atual = atual.Esquerda!;
        }
        return atual;
    }
    public NoAVL<T>? Buscar(T valor){
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

}


class NoAVL<T>
{
    public T Valor { get; set; }
    public NoAVL<T>? Esquerda { get; set; }
    public NoAVL<T>? Direita { get; set; }
    public NoAVL<T>? Pai { get; set; }
    public int Altura { get; set; }

    public NoAVL(T valor)
    {
        Valor = valor;
        Esquerda = null;
        Direita = null;
        Pai = null;
        Altura = 0;
    }
}
