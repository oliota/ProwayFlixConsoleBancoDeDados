 
create database BancoProway;

use BancoProway;
 
 -- owner padrao LAPTOP-TT37ONLI\algor
 --caso de erro alterar para sa

 --drop table perfis
 CREATE TABLE perfis (
    id_perfil int IDENTITY(1,1)  ,
    nome varchar(50),
);


  
 --drop table usuarios
 CREATE TABLE usuarios (
    id_usuario int IDENTITY(1,1) ,
    nome varchar(50),
    email varchar(50),
    login varchar(50),
    senha varchar(50),
    perfil int
);
 





---------------------------------------



 CREATE TABLE categorias (
    id_categoria int IDENTITY(1,1),
    nome varchar(50)
);

  
 CREATE TABLE filmes (
    id_filme int IDENTITY(1,1),
    nome varchar(50),
    ano int,
    sinopse varchar(1024), 
    categoria int
);




-------------------------------
 
 CREATE TABLE series (
    id_serie int IDENTITY(1,1),
    nome varchar(50), 
    ano int,
    sinopse varchar(1024), 
    categoria int
);


    
	

    CREATE TABLE temporadas (
    id_temporada int IDENTITY(1,1), 
    serie int,
    sequencial int
);

 

	----------------------------
	 
	 CREATE TABLE episodios (
    id_episodio int IDENTITY(1,1), 
	 nome varchar(50),  
    sinopse varchar(1024),
    temporada int,
    sequencial int
);


	 CREATE TABLE assistidos (
    id_assistido int IDENTITY(1,1), 
	filme int,  
    serie int,
    dataehora date
);





   --seleciona os episidos das series
   select s.nome,s.ano,s.sinopse,
   t.sequencial temporada,
   e.sequencial episodio,
   e.nome titulo,
   e.sinopse
   from episodios e
   inner join temporadas t on t.id_temporada= e.temporada
   inner join series s on s.id_serie=t.serie
   order by s.nome,t.sequencial,e.sequencial






   

INSERT perfis (nome) VALUES ('Administrador'); 
INSERT perfis (nome) VALUES ('Usuário'); 


select * from perfis;

------------------------------------------

INSERT usuarios  
   (nome, email, login ,senha,perfil)  
VALUES  
   (
	   'Rubem Oliota',
	   'rubemoliota@gmail.com',
	   'admin',
	   '123',
	   ( select id_perfil from perfis where nome = 'Administrador')
   ); 

INSERT usuarios  
   (nome, email, login ,senha,perfil)  
VALUES  
   (
	   'Fulano',
	   'fulano@gmail.com',
	   'fulano',
	   '123',
	   ( select id_perfil from perfis where nome = 'Usuário')
   ); 
    
select * from usuarios;

select u.id_usuario,u.nome,u.email,u.login,u.senha,f.nome as perfil
from usuarios u
inner join perfis f on f.id_perfil= u.perfil;



------------------------------------------




INSERT categorias (nome) VALUES ('Ação'); 
INSERT categorias (nome) VALUES ('Comédia'); 
INSERT categorias (nome) VALUES ('Terror'); 
INSERT categorias (nome) VALUES ('Drama'); 
INSERT categorias (nome) VALUES ('Ficção'); 
INSERT categorias (nome) VALUES ('Aventura'); 
INSERT categorias (nome) VALUES ('Fantasia'); 

select * from categorias;


