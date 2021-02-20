use BancoProway;


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
INSERT categorias (nome) VALUES ('Anime'); 

select * from categorias;




------------------------------------------

