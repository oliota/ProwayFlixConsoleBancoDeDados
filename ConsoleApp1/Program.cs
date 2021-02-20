using ConsoleApp1.Business;
using ConsoleApp1.Model.Repositorio;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Repositorios.BancoInicializado())
            {
                new Seguranca().ExibirMenu();
            }

            Utils.Pausar();
        }
    }
}
