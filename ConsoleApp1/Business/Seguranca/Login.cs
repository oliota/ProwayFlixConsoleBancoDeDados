using ConsoleApp1.Business.Sistema;
using ConsoleApp1.Model.Repositorio;
using System;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Business
{
    public class Login
    {
        protected enum Opcoes
        {
            Sair = 0,
            Entrar = 1,
            Cadastrar = 2
        }
        protected Usuario Usuario { get; set; }

        public void FormularioEntrar()
        {
            Usuario = new Usuario();

            Console.Clear();
            Console.WriteLine("Informe o Logon");
            Usuario.Logon = Console.ReadLine();

            Console.WriteLine("Informe a Senha");
            Usuario.Senha = Console.ReadLine();
            if (ValidarEntrada())
                Entrar();
        }

        public bool ValidarEntrada()
        {
            var select = Repositorios.banco.Usuario
                      .Where(s => s.Logon.Equals(Usuario.Logon))
                      .Where(s => s.Senha.Equals(Usuario.Senha))
                      .FirstOrDefault<Usuario>();


            if (select != null)
            {
                Usuario = select;
                return true;
            }
            else
            {
                Utils.Pausar($"Falha ao logar");
                return false;
            }

        }
        public void Entrar()
        {
            Repositorios.UsuarioLogado = Usuario;
            var sistema = new PrincipalMenu();
            sistema.ExibirMenu();
        }


        public void FormularioCadastrar()
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

            Usuario.Perfil = Repositorios.PerfilPadrao();

            if (ValidarCadastro())
                Cadastrar();

        }

        public bool ValidarCadastro()
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
            if (Repositorios.banco
                .Usuario
                .Where(x => x.Email.Equals(Usuario.Email))
                .Any()
                )
                MensagemErro.AppendLine($"Já existe um usuario com o email {Usuario.Email}");

            if (!string.IsNullOrWhiteSpace(MensagemErro.ToString()))
            {
                Console.WriteLine(MensagemErro.ToString());
                Utils.Pausar($"Falha ao cadastrar");
                return false;
            }

            return true;
        }

        public void Cadastrar()
        {
            Repositorios.banco.Usuario.Add(Usuario);
            Repositorios.Salvar();
        }

    }
}
