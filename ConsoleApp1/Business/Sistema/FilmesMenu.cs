using ConsoleApp1.Model;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Business.Sistema
{
    class FilmesMenu : Menu, IMenu, ICadastro
    {
        public enum OpcoesExtras
        {
            VerSinopse = 5
        }
        public Filme Filme { get; set; }
        public FilmeREP Rep { get; set; } = new FilmeREP();

        public bool Adicionar()
        {
            Filme = (Filme)FormularioCompleto();
            if (ValidarCompleto())
                return Rep.Adicionar(Filme);
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
            Filme = (Filme)FormularioSimples();
            if (ValidarSimples())
            {
                var atual = (Filme)Rep.Buscar(Filme);
                if (atual == null)
                {
                    Utils.Pausar("Filme não localizado");
                    return false;
                }

                return Rep.Deletar(Filme);

            }
            else
                return false;
        }
        public bool Editar()
        {
            Filme = (Filme)FormularioSimples();

            var atual = (Filme)Rep.Buscar(Filme);
            if (atual == null)
            {
                Utils.Pausar("Filme não localizado");
                return false;
            }

            if (ValidarSimples())
            {
                Filme = (Filme)FormularioCompleto();
                if (ValidarCompleto())
                    return Rep.Editar(Filme, atual);
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
                case OpcoesExtras.VerSinopse:

                    Filme = (Filme)FormularioSimples();
                    if (ValidarSimples())
                    {
                        Filme = (Filme)Rep.Buscar(Filme);
                        if (Filme == null)
                        {
                            Utils.Pausar("Filme não localizado");
                            return;
                        }
                        Utils.Pausar(Filme.Sinopse);

                       //Perguntar se que assistir ao filme
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
            Console.WriteLine("Menu de Filmes, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");
            foreach (var item in Enum.GetValues(typeof(OpcoesExtras)))
                Console.WriteLine($"\t {(int)item} : {item}");
        }
        public object FormularioCompleto()
        {
            Filme = new Filme();
            Console.Clear();
            Console.WriteLine("Informe o Nome");
            Filme.Nome = Console.ReadLine();

            Console.WriteLine("Informe o Ano");
            Filme.Ano = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Informe o Sinopse");
            Filme.Sinopse = Console.ReadLine();

            Filme.Categoria = SelecionarCategoria();


            return Filme;
        }

        public object FormularioSimples()
        {
            Filme = new Filme();
            Console.Clear();

            Console.WriteLine("Informe o Nome atual do filme");
            Filme.Nome = Console.ReadLine();
            return Filme;
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
            if (string.IsNullOrWhiteSpace(Filme.Nome))
                MensagemErro.AppendLine($"Nome não pode ficar em branco");

            if (string.IsNullOrWhiteSpace(Filme.Ano.ToString()))
                MensagemErro.AppendLine($"Ano não pode ficar em branco");

            if (string.IsNullOrWhiteSpace(Filme.Sinopse))
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
            if (string.IsNullOrWhiteSpace(Filme.Nome))
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

