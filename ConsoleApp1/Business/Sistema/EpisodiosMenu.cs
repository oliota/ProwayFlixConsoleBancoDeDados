using ConsoleApp1.Model;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Text;

namespace ConsoleApp1.Business.Sistema
{
    class EpisodiosMenu : Menu, IMenu, ICadastro
    {
        public enum OpcoesExtras
        {
            verSinopse = 5
        }
        public Temporada Temporada { get; set; }

        public EpisodioREP Rep { get; set; } = new EpisodioREP();
        public Episodio Episodio { get; set; }

        public bool Adicionar()
        {
            Episodio = (Episodio)FormularioCompleto();
            if (ValidarCompleto())
                return Rep.Adicionar(Episodio);
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
            Episodio = (Episodio)FormularioSimples();
            if (ValidarSimples())
            {
                var atual = (Episodio)Rep.Buscar(Episodio);
                if (atual == null)
                {
                    Utils.Pausar("Episodio não localizado");
                    return false;
                }

                return Rep.Deletar(Episodio);

            }
            else
                return false;
        }
        public bool Editar()
        {
            Episodio = (Episodio)FormularioSimples();

            var atual = (Episodio)Rep.Buscar(Episodio);
            if (atual == null)
            {
                Utils.Pausar("Episodio não localizado");
                return false;
            }

            if (ValidarSimples())
            {
                Episodio = (Episodio)FormularioCompleto();
                if (ValidarCompleto())
                    return Rep.Editar(Episodio, atual);
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
                    Listar(Temporada);
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
                case OpcoesExtras.verSinopse:

                    Episodio = (Episodio)FormularioSimples();
                    if (ValidarSimples())
                    {
                        Episodio = (Episodio)Rep.Buscar(Episodio);
                        if (Episodio == null)
                        {
                            Utils.Pausar("Episodio não localizado");
                            return;
                        }
                        Utils.Pausar(Episodio.Sinopse);
                         
                        if (Utils.Perguntar("Gostaria de assistir esse titulo?"))
                        {
                            Repositorios.banco.Assistido.Add(
                                new Assistido()
                                {
                                    Em = DateTime.Now,
                                    Serie = Episodio.Temporada.Serie,
                                    Usuario = Repositorios.UsuarioLogado
                                }
                            );
                            Repositorios.Salvar();

                        }
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
            Console.WriteLine($"Menu de Episodios da {Temporada.Sequencial}º Temporada da Serie [{Temporada.Serie.Nome}] , escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");
            foreach (var item in Enum.GetValues(typeof(OpcoesExtras)))
                Console.WriteLine($"\t {(int)item} : {item}");
        }
        public object FormularioCompleto()
        {
            Episodio = new Episodio();
            Console.Clear();
            Console.WriteLine("Informe o Nome");
            Episodio.Nome = Console.ReadLine();


            Console.WriteLine("Informe o Sinopse");
            Episodio.Sinopse = Console.ReadLine();

            Episodio.Temporada = Temporada;


            return Episodio;
        }

        public object FormularioSimples()
        {
            Episodio = new Episodio();
            Console.Clear();

            Console.WriteLine("Informe o Sequencial do Episodio");
            Episodio.Sequencial = Int32.Parse(Console.ReadLine());
            Episodio.Temporada = Temporada;
            return Episodio;
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
            if (string.IsNullOrWhiteSpace(Episodio.Nome))
                MensagemErro.AppendLine($"Nome não pode ficar em branco");

            if (string.IsNullOrWhiteSpace(Episodio.Sinopse))
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
            if (string.IsNullOrWhiteSpace(Episodio.Sequencial.ToString()))
                MensagemErro.AppendLine($"Sequencial não pode ficar em branco");
            if (!string.IsNullOrWhiteSpace(MensagemErro.ToString()))
            {
                Utils.Pausar(MensagemErro.ToString());
                return false;
            }

            return true;
        }


    }
}
