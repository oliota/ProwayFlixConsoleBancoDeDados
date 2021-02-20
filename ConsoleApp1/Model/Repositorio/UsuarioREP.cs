using ConsoleApp1.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApp1.Model.Repositorio
{
    public class UsuarioREP : IRepositorio
    { 
        public bool Adicionar(object item)
        {

            var usuario = (usuarios)item;
            if (Buscar(item) != null)
            {
                Utils.Pausar($"Já existe um usuario com o email {usuario.email}");
                return false;
            }
            Repositorios.banco.usuarios.Add(usuario);
            Salvar();
            Utils.Pausar($"Usuario cadastrado com sucesso!!!");
            return true;
        }

        public object Buscar(object item)
        {
            var usuario = (usuarios)item; 
            var select = Repositorios.banco.usuarios
                .Where(x => x.email.Equals(usuario.email))
                .SingleOrDefault(); 
            return select;
        }

        public void Salvar()
        {
            Repositorios.banco.SaveChanges();
        }

        public object Buscar(string logon, string senha)
        { 
            var select = Repositorios.banco.usuarios
                 .Where(x => x.login.Equals(logon))
                .Where(x => x.senha.Equals(senha))
                .SingleOrDefault(); 
            return select;
        }

        public bool Deletar(object item)
        {
            var atual = (usuarios)Buscar(item);
            Repositorios.banco.usuarios.Remove(atual);
            Salvar(); 
            return true;
        }

        public bool Editar(object item, object original)
        {
            var usuario = (usuarios)item;
            var atual = (usuarios)original;

            int posicao = Repositorios.banco.usuarios.ToList().IndexOf(atual);
            Repositorios.banco.usuarios.ToList()[posicao] = usuario;
            Salvar();
            Utils.Pausar($"Usuario atualizado com sucesso!!!");
            return true;

        }
        public void Listar()
        {
            if (!Repositorios.banco.usuarios.Any())
            {
                Utils.Pausar("Não há itens para exibir");
                return;

            }
            Console.WriteLine($"{"Nome",-30}{"Email",-30}{"Logon",-20}{"Senha",-10}{"Perfil",-20}");

            foreach (var item in Repositorios.banco.usuarios)
            {
                Console.WriteLine($"{item.nome,-30}{item.email,-30}{item.login,-20}{item.senha,-10}{item.perfis.nome,-20}");
            }
            Utils.Pausar();
        }


        public void ListarPerfis()
        {
             
            Console.WriteLine($"{"Id",-5}{"Nome",-30}");
            
            foreach (var item in Repositorios.banco.perfis.ToList())
            {
                Console.WriteLine($"{item.id_perfil,-5}{item.nome,-30}");
            } 
        }
    }
}
