using ConsoleApp1.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Model.Repositorio
{
    public static class AssistidoREP
    {
        public static void ListarTop5()
        {

            var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);


            var series = Repositorios.banco.Assistido
                   .Where(x => x.Serie != null && x.Em >= primeiroDiaMes && x.Em <= ultimoDiaMes)
                   .GroupBy(p => p.Serie)
                   .Select(g => new { Item = g.Key, Quantidade = g.Count() })
                  .Where(g => g.Quantidade >= 1).Take(5);


            var filmes = Repositorios.banco.Assistido
                  .Where(x => x.Filme != null && x.Em >= primeiroDiaMes && x.Em <= ultimoDiaMes)
                  .GroupBy(p => p.Filme)
                  .Select(g => new { Item = g.Key, Quantidade = g.Count() })
                  .Where(g=> g.Quantidade>=1).Take(5);
            


            if (!series.Any() && !filmes.Any() )
            {
                Console.WriteLine( "Não há itens para exibir");
                Utils.Pausar();

                return;
            }

            if (series.Any())
            {
                Console.WriteLine("============================================");
                Console.WriteLine("TOP 5 - Series");
                Console.WriteLine($"{"Nome",-40}{"Quantidade",-20}");
                foreach (var assistido in series)
                { 
                    Console.WriteLine($"{assistido.Item.Nome,-40}{assistido.Quantidade,-20}");
                }
            }

            if (filmes.Any())
            {
                Console.WriteLine("============================================");
                Console.WriteLine("TOP 5 - Filmes");
                Console.WriteLine($"{"Nome",-40}{"Quantidade",-20}");
                foreach (var assistido in filmes)
                {

                    Console.WriteLine($"{assistido.Item.Nome,-40}{assistido.Quantidade,-20}");
                }
            }

            Utils.Pausar();
             
          
        }
    }
}
