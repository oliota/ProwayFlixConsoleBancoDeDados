use BancoProway;

select f.id_filme,f.nome,f.ano,f.sinopse,c.nome as categoria
from filmes f
inner join categorias c on c.id_categoria= f.categoria;
 

INSERT filmes  
   (nome, ano, sinopse ,categoria)  
VALUES  
   (
	   'Matrix',
	   1999 ,
	   'Um jovem programador é atormentado por estranhos pesadelos nos quais sempre está conectado por cabos a um imenso sistema de computadores do futuro. À medida que o sonho se repete, ele começa a levantar dúvidas sobre a realidade. E quando encontra os misteriosos Morpheus e Trinity, ele descobre que é vítima do Matrix, um sistema inteligente e artificial que manipula a mente das pessoas e cria a ilusão de um mundo real enquanto usa os cérebros e corpos dos indivíduos para produzir energia.',
	   ( select id_categoria from categorias where nome = 'Ficção')
   ); 


INSERT filmes  
   (nome, ano, sinopse ,categoria)  
VALUES  
   (
	   'Extraordinário',
	   2017  ,
	   'Auggie Pullman é um garoto que nasceu com uma deformidade facial e precisou passar por 27 cirurgias plásticas. Aos 10 anos, ele finalmente começa a frequentar uma escola regular, como qualquer outra criança, pela primeira vez. No quinto ano, ele precisa se esforçar para conseguir se encaixar em sua nova realidade.',
	   ( select id_categoria from categorias where nome = 'Drama')
   ); 


   INSERT filmes  
   (nome, ano, sinopse ,categoria)  
VALUES  
   (
	   'Jumanji',
	   1995   ,
	   'Alan Parrish desapareceu quando era menino e ninguém acredita na história de seu amigo de que ele foi sugado por um jogo. Vinte e seis anos depois, duas crianças acham o jogo no sótão e, quando começam a jogar, Alan é libertado. Mas o jogo ainda não acabou e Alan precisa terminar antes de ser realmente libertado.',
	   ( select id_categoria from categorias where nome = 'Aventura')
   ); 


