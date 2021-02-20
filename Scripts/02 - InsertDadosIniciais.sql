use BancoProway;


INSERT Perfil (nome) VALUES ('Administrador'); 
INSERT Perfil (nome) VALUES ('Usuário'); 


select * from Perfil;

------------------------------------------

INSERT Usuario  
   (nome, email, Logon ,senha,perfilid)  
VALUES  
   (
	   'Rubem Oliota',
	   'rubemoliota@gmail.com',
	   'admin',
	   '123',
	   ( select IdPerfil from Perfil where nome = 'Administrador')
   ); 

INSERT Usuario  
   (nome, email, Logon ,senha,perfilid)  
VALUES  
   (
	   'Fulano',
	   'fulano@gmail.com',
	   'fulano',
	   '123',
	   ( select IdPerfil from Perfil where nome = 'Usuário')
   ); 
    
select * from Usuario;

select u.IdUsuario,u.nome,u.email,u.Logon,u.senha,f.nome as perfil
from Usuario u
inner join Perfil f on f.IdPerfil= u.perfilid;



------------------------------------------




INSERT Categoria (nome) VALUES ('Ação'); 
INSERT Categoria (nome) VALUES ('Comédia'); 
INSERT Categoria (nome) VALUES ('Terror'); 
INSERT Categoria (nome) VALUES ('Drama'); 
INSERT Categoria (nome) VALUES ('Ficção'); 
INSERT Categoria (nome) VALUES ('Aventura'); 
INSERT Categoria (nome) VALUES ('Fantasia'); 
INSERT Categoria (nome) VALUES ('Anime'); 

select * from Categoria;




------------------------------------------

