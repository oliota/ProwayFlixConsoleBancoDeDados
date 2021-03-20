using ConsoleApp1.Model;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Business.Sistema
{
    class TemporadasMenu : Menu, IMenu, ICadastro
    {
        public enum OpcoesExtras
        {
            Episodios = 5
        }
        public Serie Serie { get; set; }
        public Temporada Temporada { get; set; }

        public TemporadaREP Rep { get; set; } = new TemporadaREP();
        public Episodio Episodio { get; set; }

        public bool Adicionar()
        {
            Temporada = (Temporada)FormularioCompleto();
            if (ValidarCompleto())
                return Rep.Adicionar(Temporada);
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
            Temporada = (Temporada)FormularioSimples();
            if (ValidarSimples())
            {
                var atual = (Temporada)Rep.Buscar(Temporada);
                if (atual == null)
                {
                    Utils.Pausar("Temporada não localizada");
                    return false;
                }

                return Rep.Deletar(atual);

            }
            else
                return false;
        }
        public bool Editar()
        {
            Temporada = (Temporada)FormularioSimples();

            var atual = (Temporada)Rep.Buscar(Temporada);
            if (atual == null)
            {
                Utils.Pausar("Temporada não localizada");
                return false;
            }

            if (ValidarSimples())
            {
                Temporada = (Temporada)FormularioCompleto();
                if (ValidarCompleto())
                    return Rep.Editar(Temporada, atual);
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
                    Listar(Serie);
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
                case OpcoesExtras.Episodios:

                    Temporada = (Temporada)FormularioSimples();
                    if (ValidarSimples())
                    {
                        Temporada = (Temporada)Rep.Buscar(Temporada);
                        if (Temporada == null)
                        {
                            Utils.Pausar("Temporada não localizada");
                            return;
                        }
                        var episodiossMenu = new EpisodiosMenu();
                        episodiossMenu.Temporada = Temporada;
                        episodiossMenu.ExibirMenu();
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
            Console.WriteLine($"Menu de Temporadas da serie [{Serie.Nome}], escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");
            foreach (var item in Enum.GetValues(typeof(OpcoesExtras)))
                Console.WriteLine($"\t {(int)item} : {item}");
        }
        public object FormularioCompleto()
        {
            Temporada = new Temporada();
            Console.Clear();

            Temporada.Serie = Serie;
            return Temporada;
        }

        public object FormularioSimples()
        {
            Temporada = new Temporada();
            Console.Clear();

            Console.WriteLine("Informe o Sequencial da temporada");
            Temporada.Serie = Serie;
            Temporada.Sequencial = Int32.Parse(Console.ReadLine());
            return Temporada;
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



    }
}
