


--inserir dados da serie One Punch-Man
use BancoProway;
 
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

   select * from series ;
     
   

   --conta as temporadas de cada serie
	select s.nome, count(*)as temporadas
	from series s
	inner join temporadas t on t.serie= s.id_serie
	group by s.nome;


   
INSERT series  
   (nome, ano, sinopse ,categoria)  
VALUES  
   (
	   'One Punch-Man',
	   2015  ,
	   'Um jovem incrivelmente forte que possui a força necessária para derrotar vilões com um único soco. Devido à sua incomparável força, ele se tornou entediado e busca por adversários que possam igualmente contra ele',
	   ( select id_categoria from categorias where nome = 'Anime')
   ); 




   
-- incrementa uma temporada na serie
INSERT temporadas  
   (serie, sequencial )  
VALUES  
	( 
		( select id_serie from series where nome = 'One Punch-Man'),
		( 
			select isnull(max(t.sequencial),0)+1 
			from temporadas t
			inner join series s on s.id_serie=t.serie and s.nome = 'One Punch-Man'
		)
	); 




--incrementa um episodio na temporada da serie
	INSERT episodios  
   (nome, sinopse, temporada ,sequencial)  
VALUES  
   (
	   'Homem Mais Forte',
	   ''  ,
	   ( 
	   --busca o id da temporada
	   select t.id_temporada from temporadas t
	   inner join series s on s.id_serie= t.serie and sequencial=1 and s.nome='One Punch-Man' 
	   ),
	   ( 
			--busca o ultimo sequencial da tamporada
			select isnull(max(e.sequencial),0)+1 
			from episodios e
			inner join temporadas t on t.id_temporada= e.temporada and t.sequencial=1   
			inner join series s on s.id_serie=t.serie and  s.nome='One Punch-Man'
		)
   ); 


   
--incrementa um episodio na temporada da serie
	INSERT episodios  
   (nome, sinopse, temporada ,sequencial)  
VALUES  
   (
	   'Ciborgue Solitário',
	   ''  ,
	   ( 
	   --busca o id da temporada
	   select t.id_temporada from temporadas t
	   inner join series s on s.id_serie= t.serie and sequencial=1 and s.nome='One Punch-Man' 
	   ),
	   ( 
			--busca o ultimo sequencial da tamporada
			select isnull(max(e.sequencial),0)+1 
			from episodios e
			inner join temporadas t on t.id_temporada= e.temporada and t.sequencial=1   
			inner join series s on s.id_serie=t.serie and  s.nome='One Punch-Man'
		)
   ); 

  
--incrementa um episodio na temporada da serie
	INSERT episodios  
   (nome, sinopse, temporada ,sequencial)  
VALUES  
   (
	   'Cientista Obsessivo	',
	   ''  ,
	   ( 
	   --busca o id da temporada
	   select t.id_temporada from temporadas t
	   inner join series s on s.id_serie= t.serie and sequencial=1 and s.nome='One Punch-Man' 
	   ),
	   ( 
			--busca o ultimo sequencial da tamporada
			select isnull(max(e.sequencial),0)+1 
			from episodios e
			inner join temporadas t on t.id_temporada= e.temporada and t.sequencial=1   
			inner join series s on s.id_serie=t.serie and  s.nome='One Punch-Man'
		)
   ); 

   ---------------------------------






   

--incrementa um episodio na temporada da serie
	INSERT episodios  
   (nome, sinopse, temporada ,sequencial)  
VALUES  
   (
	   'Retorno do Herói',
	   ''  ,
	   ( 
	   --busca o id da temporada
	   select t.id_temporada from temporadas t
	   inner join series s on s.id_serie= t.serie and sequencial=2 and s.nome='One Punch-Man' 
	   ),
	   ( 
			--busca o ultimo sequencial da tamporada
			select isnull(max(e.sequencial),0)+1 
			from episodios e
			inner join temporadas t on t.id_temporada= e.temporada and t.sequencial=2   
			inner join series s on s.id_serie=t.serie and  s.nome='One Punch-Man'
		)
   ); 


   
--incrementa um episodio na temporada da serie
	INSERT episodios  
   (nome, sinopse, temporada ,sequencial)  
VALUES  
   (
	   'Criatura-Humana',
	   ''  ,
	   ( 
	   --busca o id da temporada
	   select t.id_temporada from temporadas t
	   inner join series s on s.id_serie= t.serie and sequencial=2 and s.nome='One Punch-Man' 
	   ),
	   ( 
			--busca o ultimo sequencial da tamporada
			select isnull(max(e.sequencial),0)+1 
			from episodios e
			inner join temporadas t on t.id_temporada= e.temporada and t.sequencial=2   
			inner join series s on s.id_serie=t.serie and  s.nome='One Punch-Man'
		)
   ); 

  
--incrementa um episodio na temporada da serie
	INSERT episodios  
   (nome, sinopse, temporada ,sequencial)  
VALUES  
   (
	   'Começa a Caçada',
	   ''  ,
	   ( 
	   --busca o id da temporada
	   select t.id_temporada from temporadas t
	   inner join series s on s.id_serie= t.serie and sequencial=2 and s.nome='One Punch-Man' 
	   ),
	   ( 
			--busca o ultimo sequencial da tamporada
			select isnull(max(e.sequencial),0)+1 
			from episodios e
			inner join temporadas t on t.id_temporada= e.temporada and t.sequencial=2  
			inner join series s on s.id_serie=t.serie and  s.nome='One Punch-Man'
		)
   ); 

   

    