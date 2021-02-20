namespace ConsoleApp1.Model.Repositorio
{
    public static class Repositorios
    {
       
        public static usuarios UsuarioLogado { get; set; }
        public static BancoProwayEntities banco { get; set; }  = new BancoProwayEntities();
    }
}
