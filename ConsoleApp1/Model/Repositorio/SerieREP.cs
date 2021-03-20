using ConsoleApp1.Business;
using System;
using System.Linq;

namespace ConsoleApp1.Model.Repositorio
{
    class SerieREP : IRepositorio
    {
        public bool Adicionar(object item)
        { 
            var serie = (Serie)item;
            if (Buscar(item) != null)
            {
                Utils.Pausar($"Já existe uma serie com o nome {serie.Nome}");
                return false;
            }
            Repositorios.banco.Serie.Add(serie);
            Repositorios.Salvar();
            Utils.Pausar($"Serie cadastrada com sucesso!!!");
            return true;
        }

        public object Buscar(object item)
        {
            var serie = (Serie)item;
            var select = Repositorios.banco.Serie
                .Where(x => x.Nome.Equals(serie.Nome))
                .SingleOrDefault();
            return select;
        }

        public bool Deletar(object item)
        {
            var atual = (Serie)Buscar(item);
            Repositorios.banco.Serie.Remove(atual);
            Repositorios.Salvar();
            return true;
        }

        public bool Editar(object item, object original)
        {
            var editado = (Serie)item;
            var referencia = (Serie)original;
            referencia.Nome = editado.Nome;
            referencia.Ano = editado.Ano;
            referencia.Sinopse = editado.Sinopse;
            referencia.Categoria = editado.Categoria;

            Repositorios.Salvar();
            Utils.Pausar($"Serie atualizada com sucesso!!!");
            return true; 
        }

    
        public void Listar()
        {
            if (!Repositorios.banco.Serie.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return; 
            }
            Console.WriteLine($"{"Id",-10}{"Nome",-30}{"Ano",-5}{"Categoria",-30}");

            foreach (var item in Repositorios.banco.Serie)
            {
                Console.WriteLine($"{item.IdSerie,-10}{item.Nome,-30}{item.Ano,-5}{item.Categoria.Nome,-30}");
            }
            Utils.Pausar();
        }

        public void Listar(object item)
        {

        }

        public void ListarCategorias()
        { 
            Console.WriteLine($"{"Id",-5}{"Nome",-30}"); 
            foreach (var item in Repositorios.banco.Categoria.ToList())
            {
                Console.WriteLine($"{item.IdCategoria,-5}{item.Nome,-30}");
            }
        }

    }
}
