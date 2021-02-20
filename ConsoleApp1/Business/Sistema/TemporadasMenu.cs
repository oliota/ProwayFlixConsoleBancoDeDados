using ConsoleApp1.Model;

namespace ConsoleApp1.Business.Sistema
{
    class TemporadasMenu : Menu, IMenu, ICadastro
    {
        public enum OpcoesExtras
        {
            Episodios = 5
        }
        public Serie Serie { get; set; }
        public Episodio Episodio { get; set; }

        public bool Adicionar()
        {
            throw new System.NotImplementedException();
        }

        public object Converter(string opcao)
        {
            throw new System.NotImplementedException();
        }

        public bool Deletar()
        {
            throw new System.NotImplementedException();
        }

        public bool Editar()
        {
            throw new System.NotImplementedException();
        }

        public void ExecutarEscolha(object opcao)
        {
            throw new System.NotImplementedException();
        }

        public void ExibirMenu()
        {
            throw new System.NotImplementedException();
        }

        public void ExibirOpcoes()
        {
            throw new System.NotImplementedException();
        }

        public object FormularioCompleto()
        {
            throw new System.NotImplementedException();
        }

        public object FormularioSimples()
        {
            throw new System.NotImplementedException();
        }

        public void Listar()
        {
            throw new System.NotImplementedException();
        }

        public bool ValidarCompleto()
        {
            throw new System.NotImplementedException();
        }

        public bool ValidarSimples()
        {
            throw new System.NotImplementedException();
        }
    }
}
