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
        Console.WriteLine("=======================================================");
    }
    private void EmOrdem(NoAVL<T>? no, List<NoAVL<T>> lista){
        if(no != null){
            EmOrdem(no.Esquerda, lista);
            lista.Add(no);
            EmOrdem(no.Direita, lista);
        }
    }
    private int Altura(NoAVL<T>? no){
        return no == null ? 0 : no.Altura;
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
        return no == null ? 0 : Altura(no.Esquerda) - Altura(no.Direita);
    }
    private NoAVL<T> RotacaoDireita(NoAVL<T> y)
    {
        NoAVL<T> x = y.Esquerda!;
        NoAVL<T>? T2 = x.Direita;

        // Realiza a rotação
        x.Direita = y;
        y.Esquerda = T2;

        // Ajusta os pais
        if (T2 != null)
        {
            T2.Pai = y; // T2 agora é filho esquerdo de y
        }
        x.Pai = y.Pai; // x herda o pai de y
        y.Pai = x;     // y agora é filho de x

        if (x.Pai != null)
        {
            // Atualiza o ponteiro no pai de x
            if (x.Pai.Esquerda == y)
            {
                x.Pai.Esquerda = x;
            }
            else
            {
                x.Pai.Direita = x;
            }
        }

        // Atualiza as alturas
        y.Altura = Math.Max(Altura(y.Esquerda), Altura(y.Direita)) + 1;
        x.Altura = Math.Max(Altura(x.Esquerda), Altura(x.Direita)) + 1;

        return x; // x é a nova raiz da subárvore
    }
    private NoAVL<T> RotacaoEsquerda(NoAVL<T> x)
    {
        NoAVL<T> y = x.Direita!;
        NoAVL<T>? T2 = y.Esquerda;

        // Realiza a rotação
        y.Esquerda = x;
        x.Direita = T2;

        // Ajusta os pais
        if (T2 != null)
        {
            T2.Pai = x; // T2 agora é filho direito de x
        }
        y.Pai = x.Pai; // y herda o pai de x
        x.Pai = y;     // x agora é filho de y

        if (y.Pai != null)
        {
            // Atualiza o ponteiro no pai de y
            if (y.Pai.Esquerda == x)
            {
                y.Pai.Esquerda = y;
            }
            else
            {
                y.Pai.Direita = y;
            }
        }

        // Atualiza as alturas
        x.Altura = Math.Max(Altura(x.Esquerda), Altura(x.Direita)) + 1;
        y.Altura = Math.Max(Altura(y.Esquerda), Altura(y.Direita)) + 1;

        return y; // y é a nova raiz da subárvore
    }
    private NoAVL<T> RotacaoEsquerdaDireita(NoAVL<T> no){
        no.Esquerda = RotacaoEsquerda(no.Esquerda);
        return RotacaoDireita(no);
    }
    private NoAVL<T> RotacaoDireitaEsquerda(NoAVL<T> no){
        no.Direita = RotacaoDireita(no.Direita);
        return RotacaoEsquerda(no);
    }
    private NoAVL<T> Inserir(NoAVL<T>? no, T valor, NoAVL<T>? pai){
        // Passo 1: Inserção padrão da árvore binária de busca
        if (no == null)
        {
            return new NoAVL<T> { Valor = valor, Pai = pai };
        }

        if (valor.CompareTo(no.Valor) < 0)
        {
            no.Esquerda = Inserir(no.Esquerda, valor, no);
        }
        else if (valor.CompareTo(no.Valor) > 0)
        {
            no.Direita = Inserir(no.Direita, valor, no);
        }
        else
        {
            // Valor duplicado não é permitido em AVL
            return no;
        }

        // Atualiza a altura do nó atual
        no.Altura = 1 + Math.Max(Altura(no.Esquerda), Altura(no.Direita));

        // Calcula o fator de balanceamento
        int fb = ObterFB(no);

        // Caso 1: Rotação simples à direita (desbalanceamento à esquerda)
        if (fb > 1 && valor.CompareTo(no.Esquerda!.Valor) < 0)
        {
            return RotacaoDireita(no);
        }

        // Caso 2: Rotação simples à esquerda (desbalanceamento à direita)
        if (fb < -1 && valor.CompareTo(no.Direita!.Valor) > 0)
        {
            return RotacaoEsquerda(no);
        }

        // Caso 3: Rotação dupla (esquerda-direita)
        if (fb > 1 && valor.CompareTo(no.Esquerda!.Valor) > 0)
        {
            return RotacaoEsquerdaDireita(no);
        }

        // Caso 4: Rotação dupla (direita-esquerda)
        if (fb < -1 && valor.CompareTo(no.Direita!.Valor) < 0)
        {
            return RotacaoDireitaEsquerda(no);
        }

        return no;
    }
    public void Inserir(T valor)
    {
        Raiz = Inserir(Raiz, valor, null);
    }
    private NoAVL<T>? Remover(NoAVL<T>? no, T valor)
    {
        if (no == null)
        {
            return null;
        }

        // Passo 1: Busca o nó a ser removido
        if (valor.CompareTo(no.Valor) < 0)
        {
            no.Esquerda = Remover(no.Esquerda, valor);
        }
        else if (valor.CompareTo(no.Valor) > 0)
        {
            no.Direita = Remover(no.Direita, valor);
        }
        else
        {
            // Caso 1: Nó com no máximo 1 filho ou sem filhos
            if (no.Esquerda == null || no.Direita == null)
            {
                NoAVL<T>? temp = no.Esquerda ?? no.Direita;

                if (temp == null)
                {
                    // Sem filhos
                    temp = no;
                    no = null;
                }
                else
                {
                    // Substituição pelo único filho
                    no = temp;
                }
            }
            else
            {
                // Caso 2: Nó com dois filhos, substituído pelo menor na subárvore direita
                NoAVL<T> temp = ObterMenor(no.Direita!);
                no.Valor = temp.Valor;
                no.Direita = Remover(no.Direita, temp.Valor);
            }
        }

        // Se a árvore tiver apenas um nó
        if (no == null)
        {
            return null;
        }

        // Passo 2: Atualiza a altura
        no.Altura = 1 + Math.Max(Altura(no.Esquerda), Altura(no.Direita));

        // Passo 3: Calcula o fator de balanceamento
        int fb = ObterFB(no);

        // Caso 1: Rotação simples à direita
        if (fb > 1 && ObterFB(no.Esquerda) >= 0)
        {
            return RotacaoDireita(no);
        }

        // Caso 2: Rotação simples à esquerda
        if (fb < -1 && ObterFB(no.Direita) <= 0)
        {
            return RotacaoEsquerda(no);
        }

        // Caso 3: Rotação dupla (esquerda-direita)
        if (fb > 1 && ObterFB(no.Esquerda) < 0)
        {
            return RotacaoEsquerdaDireita(no);
        }

        // Caso 4: Rotação dupla (direita-esquerda)
        if (fb < -1 && ObterFB(no.Direita) > 0)
        {
            return RotacaoDireitaEsquerda(no);
        }

        return no;
    }
    public void Remover(T valor)
    {
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
    public void EhAVL(){
         if (EhAVL(Raiz)) Console.WriteLine("É uma árvore AVL essa porra");
         else Console.WriteLine("Não é uma árvore AVL essa porra");
    }
    private bool EhAVL(NoAVL<T>? no){
        
        if(no is null) return true;

        int fb = ObterFB(no);

        if(Math.Abs(fb) > 1) return false;

        return EhAVL(no.Esquerda) && EhAVL(no.Direita);
    }
    public void VerificarNo(T valor){
        var no = Buscar(valor);
        if(no == null){
            Console.WriteLine("Valor não encontrado");
            return;
        }

        Console.WriteLine($"Valor: {no.Valor}");
        Console.WriteLine($"Pai: {(no.Pai != null ? no.Pai.Valor.ToString() : "null")}");
        Console.WriteLine($"Altura: {no.Altura}");
        Console.WriteLine($"FB: {ObterFB(no)}");
        Console.WriteLine($"Filho Esquerdo: {(no.Esquerda != null ? no.Esquerda.Valor.ToString() : "null")}");
        Console.WriteLine($"Filho Direito: {(no.Direita != null ? no.Direita.Valor.ToString() : "null")}");
    }
}

class NoAVL<T>
{
    public T Valor { get; set; } = default!;
    public NoAVL<T>? Esquerda { get; set; } = null;
    public NoAVL<T>? Direita { get; set; } = null;
    public NoAVL<T>? Pai { get; set; }
    public int Altura { get; set; } = 1;

    // public NoAVL(T valor)
    // {
    //     Valor = valor;
    //     Esquerda = null;
    //     Direita = null;
    //     Pai = null;
    //     Altura = 0;
    // }
}
