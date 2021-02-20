use BancoProway;

select f.IdFilme,f.nome,f.ano,f.sinopse,c.nome as categoria
from Filme f
inner join Categoria c on c.IdCategoria= f.CategoriaId;
 

INSERT Filme  
   (nome, ano, sinopse ,CategoriaId)  
VALUES  
   (
	   'Matrix',
	   1999 ,
	   'Um jovem programador é atormentado por estranhos pesadelos nos quais sempre está conectado por cabos a um imenso sistema de computadores do futuro. À medida que o sonho se repete, ele começa a levantar dúvidas sobre a realidade. E quando encontra os misteriosos Morpheus e Trinity, ele descobre que é vítima do Matrix, um sistema inteligente e artificial que manipula a mente das pessoas e cria a ilusão de um mundo real enquanto usa os cérebros e corpos dos indivíduos para produzir energia.',
	   ( select IdCategoria from Categoria where nome = 'Ficção')
   ); 


INSERT Filme  
   (nome, ano, sinopse ,CategoriaId)  
VALUES  
   (
	   'Extraordinário',
	   2017  ,
	   'Auggie Pullman é um garoto que nasceu com uma deformidade facial e precisou passar por 27 cirurgias plásticas. Aos 10 anos, ele finalmente começa a frequentar uma escola regular, como qualquer outra criança, pela primeira vez. No quinto ano, ele precisa se esforçar para conseguir se encaixar em sua nova realidade.',
	   ( select IdCategoria from Categoria where nome = 'Drama')
   ); 


   INSERT Filme  
   (nome, ano, sinopse ,CategoriaId)  
VALUES  
   (
	   'Jumanji',
	   1995   ,
	   'Alan Parrish desapareceu quando era menino e ninguém acredita na história de seu amigo de que ele foi sugado por um jogo. Vinte e seis anos depois, duas crianças acham o jogo no sótão e, quando começam a jogar, Alan é libertado. Mas o jogo ainda não acabou e Alan precisa terminar antes de ser realmente libertado.',
	   ( select IdCategoria from Categoria where nome = 'Aventura')
   ); 


