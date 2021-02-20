using ConsoleApp1.Business;
using System;
using System.Linq;

namespace ConsoleApp1.Model.Repositorio
{
    class CategoriaREP : IRepositorio
    {
        public bool Adicionar(object item)
        {

            var categoria = (Categoria)item;
            if (Buscar(item) != null)
            {
                Utils.Pausar($"Já existe uma categoria com o nome {categoria.Nome}");
                return false;
            }
            Repositorios.banco.Categoria.Add(categoria);
            Repositorios.Salvar();
            Utils.Pausar($"Categoria cadastrado com sucesso!!!");
            return true;
        }

        public object Buscar(object item)
        {
            var categoria = (Categoria)item;
            var select = Repositorios.banco.Categoria
                .Where(x => x.Nome.Equals(categoria.Nome))
                .SingleOrDefault();
            return select;
        }

        public bool Deletar(object item)
        {
            var atual = (Categoria)Buscar(item);
            Repositorios.banco.Categoria.Remove(atual);
            Repositorios.Salvar();
            return true;
        }

        public bool Editar(object item, object original)
        {
            var editado = (Categoria)item;
            var referencia = (Categoria)original;
            referencia.Nome = editado.Nome;

            Repositorios.Salvar();
            Utils.Pausar($"categoria atualizada com sucesso!!!");
            return true;

        }
        public void Listar()
        {
            if (!Repositorios.banco.Categoria.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return;

            }
            Console.WriteLine($"{"Nome",-30}");

            foreach (var item in Repositorios.banco.Categoria)
            {
                Console.WriteLine($"{item.Nome,-30}");
            }
            Utils.Pausar();
        }


    }
}
