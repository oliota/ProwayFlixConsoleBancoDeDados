using ConsoleApp1.Business;
using System;
using System.Linq;

namespace ConsoleApp1.Model.Repositorio
{
    class EpisodioREP : IRepositorio
    {
        public bool Adicionar(object item)
        {
            var episodio = (Episodio)item;
            if (Buscar(item) != null)
            {
                Utils.Pausar($"Já existe um Episodio com o nome {episodio.Nome}");
                return false;
            }

            episodio.Sequencial = Repositorios.banco.Episodio.Where(x => x.TemporadaId == episodio.Temporada.IdTemporada).Count() + 1;
            Repositorios.banco.Episodio.Add(episodio);
            Repositorios.Salvar();
            Utils.Pausar($"Episodio cadastrado com sucesso!!!");
            return true;
        }

        public object Buscar(object item)
        {
            var episodio = (Episodio)item;
            var select = Repositorios.banco.Episodio
                .Where(x => x.TemporadaId == episodio.Temporada.IdTemporada)
                .Where(x => x.Sequencial == episodio.Sequencial)
                .SingleOrDefault();
            return select;
        }

        public bool Deletar(object item)
        {
            var atual = (Episodio)Buscar(item);
            Repositorios.banco.Episodio.Remove(atual);
            Repositorios.Salvar();
            return true;
        }

        public bool Editar(object item, object original)
        {
            var editado = (Episodio)item;
            var referencia = (Episodio)original;
            referencia.Nome = editado.Nome;
            referencia.Sinopse = editado.Sinopse;

            Repositorios.Salvar();
            Utils.Pausar($"Episodio atualizado com sucesso!!!");
            return true;
        }
        public void Listar()
        {
           
        }
        public void Listar(object item)
        {
            var temporada = (Temporada) item;
            var episodios = Repositorios.banco.Episodio.Where(x => x.TemporadaId == temporada.IdTemporada).ToArray().OrderBy(x => x.Sequencial);

            if (!episodios.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return;
            }
            Console.WriteLine($"{"Sequencial",-20}{"Nome",-40}");

            foreach (var ep in episodios)
            {
                Console.WriteLine($"{ep.Sequencial,-20}{ep.Nome,-40}");
            }
            Utils.Pausar();
        }


    }
}
