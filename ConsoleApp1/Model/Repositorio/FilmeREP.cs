using ConsoleApp1.Business;
using System;
using System.Linq;

namespace ConsoleApp1.Model.Repositorio
{
    class FilmeREP : IRepositorio
    {
        public bool Adicionar(object item)
        {
            var filme = (Filme)item;
            if (Buscar(item) != null)
            {
                Utils.Pausar($"Já existe um filme com o nome {filme.Nome}");
                return false;
            }
            Repositorios.banco.Filme.Add(filme);
            Repositorios.Salvar();
            Utils.Pausar($"Filme cadastrado com sucesso!!!");
            return true;
        }

        public object Buscar(object item)
        {
            var filme = (Filme)item;
            var select = Repositorios.banco.Filme
                .Where(x => x.Nome.Equals(filme.Nome))
                .SingleOrDefault();
            return select;
        }

        public bool Deletar(object item)
        {
            var atual = (Filme)Buscar(item);
            Repositorios.banco.Filme.Remove(atual);
            Repositorios.Salvar();
            return true;
        }

        public bool Editar(object item, object original)
        {
            var editado = (Filme)item;
            var referencia = (Filme)original;
            referencia.Nome = editado.Nome;
            referencia.Ano = editado.Ano;
            referencia.Sinopse = editado.Sinopse;
            referencia.Categoria = editado.Categoria;

            Repositorios.Salvar();
            Utils.Pausar($"Filme atualizado com sucesso!!!");
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

            foreach (var item in Repositorios.banco.Filme)
            {
                Console.WriteLine($"{item.IdFilme,-10}{item.Nome,-30}{item.Ano,-5}{item.Categoria.Nome,-30}");
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
