using ConsoleApp1.Model;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Text;

namespace ConsoleApp1.Business.Sistema
{
    class CategoriasMenu : Menu, IMenu, ICadastro
    {
        public Categoria Categoria { get; set; }
        public CategoriaREP Rep { get; set; } = new CategoriaREP();

        public bool Adicionar()
        {
            Categoria = (Categoria)FormularioCompleto();
            if (ValidarCompleto())
                return Rep.Adicionar(Categoria);
            else
                return false;
        }
        public object Converter(string opcao)
        {
            Enum.TryParse(opcao, out Opcoes convertido);
            return convertido;
        }
        public bool Deletar()
        {
            Categoria = (Categoria)FormularioSimples();
            if (ValidarSimples())
            {
                var atual = (Categoria)Rep.Buscar(Categoria);
                if (atual == null)
                {
                    Utils.Pausar("Categoria não localizada");
                    return false;
                }

                return Rep.Deletar(Categoria);

            }
            else
                return false;
        }
        public bool Editar()
        {
            Categoria = (Categoria)FormularioSimples();

            var atual = (Categoria)Rep.Buscar(Categoria);
            if (atual == null)
            {
                Utils.Pausar("Categoria não localizada");
                return false;
            }

            if (ValidarSimples())
            {

                Categoria = (Categoria)FormularioCompleto();
                if (ValidarCompleto())
                    return Rep.Editar(Categoria, atual);
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
                    Listar();
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
            Console.WriteLine("Menu de categorias, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");
        }
        public object FormularioCompleto()
        {
            Categoria = new Categoria();
            Console.Clear();
            Console.WriteLine("Informe o Nome");
            Categoria.Nome = Console.ReadLine();

            return Categoria;
        }

        public object FormularioSimples()
        {
            Categoria = new Categoria();
            Console.Clear();

            Console.WriteLine("Informe o Nome atual da categoria");
            Categoria.Nome = Console.ReadLine();
            return Categoria;
        }
        public void Listar()
        {
            Rep.Listar();
        }
        public bool ValidarCompleto()
        {
            var MensagemErro = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Categoria.Nome))
                MensagemErro.AppendLine($"Nome não pode ficar em branco");
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
            if (string.IsNullOrWhiteSpace(Categoria.Nome))
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
