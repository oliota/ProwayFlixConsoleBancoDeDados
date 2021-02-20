using ConsoleApp1.Model;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Business.Sistema
{
    class UsuariosMenu : Menu, IMenu, ICadastro
    {
        public Usuario Usuario { get; set; }
        public UsuarioREP Rep { get; set; } = new UsuarioREP();
        public bool Adicionar()
        {
            Usuario = (Usuario)FormularioCompleto();
            if (ValidarCompleto())
                return Rep.Adicionar(Usuario);
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
            Usuario = (Usuario)FormularioSimples();
            if (ValidarSimples())
            {
                var atual = (Usuario)Rep.Buscar(Usuario);
                if (atual == null)
                {
                    Utils.Pausar("Usuario não localizado");
                    return false;
                }
                if (atual.Email.Equals(Repositorios.UsuarioLogado.Email))
                {
                    if (Utils.Perguntar("Ao deletar o usuario logado, será necessario retornar ao login, confirma Exclusão?"))
                    {
                        Repositorios.UsuarioLogado = null;
                        Rep.Deletar(Usuario);
                        ExecutarEscolha(Opcoes.Voltar);
                        return true;

                    }
                    else
                        return false;
                }
                else
                    return Rep.Deletar(Usuario);

            }
            else
                return false;
        }
        public bool Editar()
        {
            Usuario = (Usuario)FormularioSimples();

            var atual = (Usuario)Rep.Buscar(Usuario);
            if (atual == null)
            {
                Utils.Pausar("Usuario não localizado");
                return false;
            }

            if (ValidarSimples())
            {

                Usuario = (Usuario)FormularioCompleto();
                if (ValidarCompleto())
                    return Rep.Editar(Usuario, atual);
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
            Console.WriteLine("Menu de usuarios, escolha uma opção:");
            foreach (var item in Enum.GetValues(typeof(Opcoes)))
                Console.WriteLine($"\t {(int)item} : {item}");
        }
        public object FormularioCompleto()
        {
            Usuario = new Usuario();
            Console.Clear();
            Console.WriteLine("Informe o Nome");
            Usuario.Nome = Console.ReadLine();

            Console.WriteLine("Informe o Email");
            Usuario.Email = Console.ReadLine();

            Console.WriteLine("Informe o Logon");
            Usuario.Logon = Console.ReadLine();

            Console.WriteLine("Informe a Senha");
            Usuario.Senha = Console.ReadLine();

            if (Repositorios.UsuarioLogado.Perfil.Nome.Equals(
                    Repositorios.banco.Perfil
                    .Where(x => x.Nome.Equals("Administrador"))
                    .SingleOrDefault()
                    .Nome
                    )
                )
            {
                Usuario.Perfil = SelecionarPerfil();
                if (Usuario.Perfil == null)
                    Usuario.Perfil = Repositorios.PerfilPadrao();
            }
            else
            {

                Usuario.Perfil = Repositorios.PerfilPadrao();
            }


            return Usuario;
        }

        private Perfil SelecionarPerfil()
        {
            Console.WriteLine("=================================");
            Rep.ListarPerfis();
            Console.WriteLine("Selecione um perfil da lista");
            string escolha;
            Perfil perfil;
            do
            {
                escolha = Console.ReadLine();
                perfil = Repositorios.banco.Perfil
                .Where(x => x.Nome.Equals(escolha) ||
                       x.IdPerfil.ToString().Equals(escolha)
                       )
                .SingleOrDefault();
                if (perfil == null)
                    Console.WriteLine("Escolha um perfil válido!");
            } while (perfil == null);


            return perfil;
        }



        public object FormularioSimples()
        {
            Usuario = new Usuario();
            Console.Clear();

            Console.WriteLine("Informe o Email atual do usuario");
            Usuario.Email = Console.ReadLine();
            return Usuario;
        }
        public void Listar()
        {
            Rep.Listar();
        }
        public bool ValidarCompleto()
        {
            var MensagemErro = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Usuario.Nome))
                MensagemErro.AppendLine($"Nome não pode ficar em branco");
            if (string.IsNullOrWhiteSpace(Usuario.Email))
                MensagemErro.AppendLine($"Email não pode ficar em branco");
            if (string.IsNullOrWhiteSpace(Usuario.Logon))
                MensagemErro.AppendLine($"Logon não pode ficar em branco");
            if (string.IsNullOrWhiteSpace(Usuario.Senha))
                MensagemErro.AppendLine($"Senha não pode ficar em branco");
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
            if (string.IsNullOrWhiteSpace(Usuario.Email))
                MensagemErro.AppendLine($"Email não pode ficar em branco");
            if (!string.IsNullOrWhiteSpace(MensagemErro.ToString()))
            {
                Utils.Pausar(MensagemErro.ToString());
                return false;
            }

            return true;
        }
    }
}
