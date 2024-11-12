public interface IArvore<T> where T : IComparable<T>
{
    void Inserir(T valor);
    void Remover(T valor);
    No<T> Buscar(T valor);
    // bool Contem(T valor);
    // void Limpar();
    // int Contar();
    // int Altura();
    // void EmOrdem(Action<T> action);
    // void PreOrdem(Action<T> action);
    // void PosOrdem(Action<T> action);
}