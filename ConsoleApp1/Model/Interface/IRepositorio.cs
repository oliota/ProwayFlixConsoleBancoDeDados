namespace ConsoleApp1.Model.Repositorio
{
    interface IRepositorio
    {
        bool Adicionar(object item);
        bool Editar(object item, object original);
        bool Deletar(object item);
        void Listar();
        void Listar(object item);
        object Buscar(object item);

    }
}
