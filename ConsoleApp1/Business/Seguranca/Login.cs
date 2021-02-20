using ConsoleApp1.Business.Sistema;
using ConsoleApp1.Model;
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
        protected usuarios Usuario { get; set; } 
         
        public void FormularioEntrar()
        {
            Usuario = new usuarios();

            Console.Clear();
            Console.WriteLine("Informe o Logon");
            Usuario.login = Console.ReadLine();

            Console.WriteLine("Informe a Senha");
            Usuario.senha = Console.ReadLine(); 
            if (ValidarEntrada())
                Entrar();
        }

        public bool ValidarEntrada()
        {
            var select = Repositorios.banco.usuarios
                      .Where(s => s.login.Equals(Usuario.login))
                      .Where(s => s.senha.Equals(Usuario.senha))
                      .FirstOrDefault<usuarios>();


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
            Usuario = new usuarios();

            Console.Clear();
            Console.WriteLine("Informe o Nome");
            Usuario.nome = Console.ReadLine();

            Console.WriteLine("Informe o Email");
            Usuario.email = Console.ReadLine();

            Console.WriteLine("Informe o Logon");
            Usuario.login = Console.ReadLine();

            Console.WriteLine("Informe a Senha");
            Usuario.senha = Console.ReadLine();

            Usuario.perfis = Repositorios.banco.perfis.Where(x => x.nome.Equals("Usuário")).SingleOrDefault();

            if (ValidarCadastro())
                Cadastrar();

        }

        public bool ValidarCadastro()
        {
            var MensagemErro = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Usuario.nome))
                MensagemErro.AppendLine($"Nome não pode ficar em branco");
            if (string.IsNullOrWhiteSpace(Usuario.email))
                MensagemErro.AppendLine($"Email não pode ficar em branco");
            if (string.IsNullOrWhiteSpace(Usuario.login))
                MensagemErro.AppendLine($"Logon não pode ficar em branco");
            if (string.IsNullOrWhiteSpace(Usuario.senha))
                MensagemErro.AppendLine($"Senha não pode ficar em branco");
            if (Repositorios.banco
                .usuarios
                .Where(x => x.email.Equals(Usuario.email))
                .Any()
                )
                MensagemErro.AppendLine($"Já existe um usuario com o email {Usuario.email}");

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
            //new UsuarioREP().Adicionar(Usuario);
            Repositorios.banco.usuarios.Add(Usuario);
            Repositorios.banco.SaveChanges();
        }

    }
}
