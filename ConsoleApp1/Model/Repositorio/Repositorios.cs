using System.Linq;

namespace ConsoleApp1.Model.Repositorio
{
    public static class Repositorios
    {

        public static Usuario UsuarioLogado { get; set; }
        public static ModelEntities banco { get; set; } = new ModelEntities();

        public static Perfil PerfilPadrao()
        {
            if (Repositorios.banco.Usuario.Any())
                return banco.Perfil.Where(x => x.Nome.Equals("Usuário")).SingleOrDefault();
            else
                return banco.Perfil.Where(x => x.Nome.Equals("Administrador")).SingleOrDefault();

        }
        public static void Salvar()
        {
            banco.SaveChanges();
        }

        public static bool BancoInicializado()
        {
            bool validacao = true;
            if (!banco.Categoria.Any())
            {
                System.Console.WriteLine("A tabela de Categorias não foi inicializada");
                validacao = false;
            }
            if (!banco.Perfil.Any())
            {
                System.Console.WriteLine("A tabela de Perfis não foi inicializada");
                validacao = false;
            }
            return validacao;
        }
    }
}
