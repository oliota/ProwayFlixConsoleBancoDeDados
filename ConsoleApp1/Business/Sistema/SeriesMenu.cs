
using ConsoleApp1.Model;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Business.Sistema
{
    class SeriesMenu : Menu, IMenu, ICadastro
    {
        public enum OpcoesExtras
        {
            Temporadas = 5
        }
        public Serie Serie { get; set; }
        public SerieREP Rep { get; set; } = new SerieREP();

        public bool Adicionar()
        {
            Serie = (Serie)FormularioCompleto();
            if (ValidarCompleto())
                return Rep.Adicionar(Serie);
            else
                return false;
        }
        public object Converter(string opcao)
        {  
            Enum.TryParse(opcao, out Opcoes convertido);

            if (Enum.IsDefined(typeof(Opcoes), convertido))
                return convertido;
            else
            {
                Enum.TryParse(opcao, out OpcoesExtras extra);
                return extra;
            }
        }
        public bool Deletar()
        {
            Serie = (Serie)FormularioSimples();
            if (ValidarSimples())
            {
                var atual = (Serie)Rep.Buscar(Serie);
                if (atual == null)
                {
                    Utils.Pausar("Serie não localizada");
                    return false;
                }

                return Rep.Deletar(Serie);

            }
            else
                return false;
        }
        public bool Editar()
        {
            Serie = (Serie)FormularioSimples();

            var atual = (Serie)Rep.Buscar(Serie);
            if (atual == null)
            {
                Utils.Pausar("Serie não localizada");
                return false;
            }

            if (ValidarSimples())
            {
                Serie = (Serie)FormularioCompleto();
                if (ValidarCompleto())
                    return Rep.Editar(Serie, atual);
                else
                    return false;
            }
            else
                return false;
        }
        public void ExecutarEscolha(object opcao)
        {
            switch (opcao)
            {
                case Opcoes.Voltar:
                    Escolha = "0";
                    return;
                case Opcoes.Listar:
                    Listar(null);
                    break;
                case Opcoes.Adicionar:
                    Adicionar();
                    break;
                case Opcoes.Editar:
                    Editar();
                    break;
                case Opcoes.Deletar:
                    Deletar();
                    break;
                case OpcoesExtras.Temporadas:

                    Serie = (Serie)FormularioSimples();
                    if (ValidarSimples())
                    {
                        Serie = (Serie)Rep.Buscar(Serie);
                        if (Serie == null)
                        {
                            Utils.Pausar("Serie não localizada");
                            return;
                        }
                        var TemporadasMenu = new TemporadasMenu();
                        TemporadasMenu.Serie = Serie;
                        TemporadasMenu.ExibirMenu();
                    }
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
            Console.WriteLine("Menu de Series, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");
            foreach (var item in Enum.GetValues(typeof(OpcoesExtras)))
                 Console.WriteLine($"\t {(int)item} : {item}");
        }
        public object FormularioCompleto()
        {
            Serie = new Serie();
            Console.Clear();
            Console.WriteLine("Informe o Nome");
            Serie.Nome = Console.ReadLine();

            Console.WriteLine("Informe o Ano");
            Serie.Ano = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Informe o Sinopse");
            Serie.Sinopse = Console.ReadLine();

            Serie.Categoria = SelecionarCategoria();


            return Serie;
        }

        public object FormularioSimples()
        {
            Serie = new Serie();
            Console.Clear();

            Console.WriteLine("Informe o Nome atual da serie");
            Serie.Nome = Console.ReadLine();
            return Serie;
        }
        public void Listar(object item)
        {
            if (item == null)
                Rep.Listar();
            else
                Rep.Listar(item);
        }
        public bool ValidarCompleto()
        {
            var MensagemErro = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Serie.Nome))
                MensagemErro.AppendLine($"Nome não pode ficar em branco");

            if (string.IsNullOrWhiteSpace(Serie.Ano.ToString()))
                MensagemErro.AppendLine($"Ano não pode ficar em branco");

            if (string.IsNullOrWhiteSpace(Serie.Sinopse))
                MensagemErro.AppendLine($"Sinopse não pode ficar em branco");

            if (!string.IsNullOrWhiteSpace(MensagemErro.ToString()))
            {
                Utils.Pausar(MensagemErro.ToString());
                return false;
            }

            return true;
        }
        public bool ValidarSimples()
        {
            var MensagemErro = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Serie.Nome))
                MensagemErro.AppendLine($"Nome não pode ficar em branco");
            if (!string.IsNullOrWhiteSpace(MensagemErro.ToString()))
            {
                Utils.Pausar(MensagemErro.ToString());
                return false;
            }

            return true;
        }

        private Categoria SelecionarCategoria()
        {
            Console.WriteLine("=================================");
            Rep.ListarCategorias();
            Console.WriteLine("Selecione uma categoria da lista");
            string escolha;
            Categoria categoria;
            do
            {
                escolha = Console.ReadLine();
                categoria = Repositorios.banco.Categoria
                .Where(x => x.Nome.Equals(escolha) ||
                       x.IdCategoria.ToString().Equals(escolha)
                       )
                .SingleOrDefault();
                if (categoria == null)
                    Console.WriteLine("Escolha uma categoria válida!");
            } while (categoria == null);


            return categoria;
        }

    }
}
