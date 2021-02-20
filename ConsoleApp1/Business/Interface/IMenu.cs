namespace ConsoleApp1.Business
{
    interface IMenu
    {
        void ExibirMenu();
        object Converter(string opcao);
        void ExibirOpcoes();
        void ExecutarEscolha(object opcao);
    }
}
