using ConsoleApp1.Business;
using System;
using System.Linq;

namespace ConsoleApp1.Model.Repositorio
{
    class TemporadaREP : IRepositorio
    {
        public bool Adicionar(object item)
        {
            var temporada = (Temporada)item;

            temporada.Sequencial = Repositorios.banco.Temporada.Where(x => x.SerieId == temporada.Serie.IdSerie).Count() + 1;
            Repositorios.banco.Temporada.Add(temporada);
            Repositorios.Salvar();
            Utils.Pausar($"Temporada cadastrada com sucesso!!!");
            return true;
        }

        public object Buscar(object item)
        {
            var temporada = (Temporada)item;
            var select = Repositorios.banco.Temporada
                .Where(x => x.SerieId==temporada.Serie.IdSerie)
                .Where(x => x.Sequencial==temporada.Sequencial)
                .SingleOrDefault();
            return select;
        }

        public bool Deletar(object item)
        {
            var atual = (Temporada)Buscar(item);
            Repositorios.banco.Temporada.Remove(atual);
            Repositorios.Salvar();
            return true;
        }

        public bool Editar(object item, object original)
        {
            //var editado = (Temporada)item;
            //var referencia = (Temporada)original;
            //referencia.IdTemporada = editado.Nome;
            //referencia.Sequencial = editado.Ano;
            //referencia.Sinopse = editado.Sinopse;
            //referencia.Categoria = editado.Categoria;

            //Repositorios.Salvar();
            //Utils.Pausar($"Serie atualizada com sucesso!!!");
            return true;
        }
        public void Listar()
        {
           
        }

        public void Listar(object item)
        {

            var serie = (Serie)item;
            var temporadas = Repositorios.banco.Temporada.Where(x => x.SerieId == serie.IdSerie).ToArray().OrderBy(x=> x.Sequencial);
            if (!temporadas.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return;
            }
            Console.WriteLine($"{"Temporada",-30}");

            foreach (var temp in temporadas)
            {
                Console.WriteLine($"{temp.Sequencial,-30}");
            }
            Utils.Pausar();
        }



    }
}
