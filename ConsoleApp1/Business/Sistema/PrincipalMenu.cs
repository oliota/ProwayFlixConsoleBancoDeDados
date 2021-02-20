using ConsoleApp1.Model.Repositorio;
using System;

namespace ConsoleApp1.Business.Sistema
{
    class PrincipalMenu : IMenu
    {
        public string Escolha;
        private enum Opcoes
        {
            Sair = 0,
            Usuarios = 1,
            Categorias = 2,
            Filmes = 3,
            Series = 4,
            Relatorios = 5,
            Assistidos = 6
        }


        public object Converter(string opcao)
        {
            Enum.TryParse(opcao, out Opcoes convertido);
            return convertido;
        }

        public void ExecutarEscolha(object opcao)
        {
            switch (opcao)
            {
                case Opcoes.Sair:
                    Escolha = "0";
                    return;
                case Opcoes.Usuarios:
                    new UsuariosMenu().ExibirMenu();
                    break;
                case Opcoes.Categorias:
                    new CategoriasMenu().ExibirMenu();
                    break;
                case Opcoes.Filmes:
                    break;
                case Opcoes.Series:
                    new SeriesMenu().ExibirMenu(); 
                    break;
                case Opcoes.Assistidos:
                    break;
                case Opcoes.Relatorios:
                    Console.WriteLine("Menu de relatorios");
                    break;
                default:
                    Utils.Pausar("Opção inválida");
                    break;
            }
        }

        public void ExibirMenu()
        {
            do
            {
                ExibirOpcoes();
                Escolha = Console.ReadLine();
                ExecutarEscolha(Converter(Escolha));
            } while (!Escolha.Equals("0"));
        }

        public void ExibirOpcoes()
        {
            Console.Clear();
            if (Repositorios.UsuarioLogado == null)
            {
                ExecutarEscolha(Opcoes.Sair);
                return;
            }
            Console.WriteLine($"Bem vindo, {Repositorios.UsuarioLogado.Nome}");
            Console.WriteLine("Menu de principal, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");

        }
    }
}
