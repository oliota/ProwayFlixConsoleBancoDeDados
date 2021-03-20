using ConsoleApp1.Business;
using System;
using System.Linq;

namespace ConsoleApp1.Model.Repositorio
{
    public class UsuarioREP : IRepositorio
    {
        public bool Adicionar(object item)
        {

            var usuario = (Usuario)item;
            if (Buscar(item) != null)
            {
                Utils.Pausar($"Já existe um usuario com o email {usuario.Email}");
                return false;
            }
            Repositorios.banco.Usuario.Add(usuario);
            Repositorios.Salvar();
            Utils.Pausar($"Usuario cadastrado com sucesso!!!");
            return true;
        }

        public object Buscar(object item)
        {
            var usuario = (Usuario)item;
            var select = Repositorios.banco.Usuario
                .Where(x => x.Email.Equals(usuario.Email))
                .SingleOrDefault();
            return select;
        }



        public object Buscar(string logon, string senha)
        {
            var select = Repositorios.banco.Usuario
                 .Where(x => x.Logon.Equals(logon))
                .Where(x => x.Senha.Equals(senha))
                .SingleOrDefault();
            return select;
        }

        public bool Deletar(object item)
        {
            var atual = (Usuario)Buscar(item);
            Repositorios.banco.Usuario.Remove(atual);
            Repositorios.Salvar();
            return true;
        }

        public bool Editar(object item, object original)
        {
            var editado = (Usuario)item;
            var referencia = (Usuario)original;
            referencia.Nome = editado.Nome;
            referencia.Email = editado.Email;
            referencia.Logon = editado.Logon;
            referencia.Senha = editado.Senha;
            referencia.Perfil = editado.Perfil;

            Repositorios.Salvar();
            Utils.Pausar($"Usuario atualizado com sucesso!!!");
            return true;

        }
        public void Listar()
        {
            if (!Repositorios.banco.Usuario.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return;

            }
            Console.WriteLine($"{"Nome",-30}{"Email",-30}{"Logon",-20}{"Senha",-10}{"Perfil",-20}");

            foreach (var item in Repositorios.banco.Usuario)
            {
                Console.WriteLine($"{item.Nome,-30}{item.Email,-30}{item.Logon,-20}{item.Senha,-10}{item.Perfil.Nome,-20}");
            }
            Utils.Pausar();
        }
        public void Listar(object item)
        {

        }

        public void ListarPerfis()
        {

            Console.WriteLine($"{"Id",-5}{"Nome",-30}");

            foreach (var item in Repositorios.banco.Perfil.ToList())
            {
                Console.WriteLine($"{item.IdPerfil,-5}{item.Nome,-30}");
            }
        }
    }
}
