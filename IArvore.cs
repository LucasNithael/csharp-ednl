public interface IArvore<T> where T : IComparable<T>
{
    void Inserir(T valor);
    void Remover(T valor);
    No<T> Buscar(T valor);
    void Mostrar();
}