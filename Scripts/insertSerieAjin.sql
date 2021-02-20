--inserir dados da serie Ajin: Demi-Human

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


   --conta as temporadas de cada serie
	select s.nome, count(*)as temporadas
	from series s
	inner join temporadas t on t.serie= s.id_serie
	group by s.nome;




INSERT series  
   (nome, ano, sinopse ,categoria)  
VALUES  
   (
	   'Ajin: Demi-Human',
	   2016  ,
	   'Quando é atropelado por um caminhão, Kei descobre que é um Ajin e rapidamente passa a ser procurado pelo governo. Só há uma pessoa em quem ele pode confiar.',
	   ( select id_categoria from categorias where nome = 'Aventura')
   ); 



   
-- incrementa um temporada na serie
INSERT temporadas  
   (serie, sequencial )  
VALUES  
	( 
		( select id_serie from series where nome = 'Ajin: Demi-Human'),
		( 
			select isnull(max(t.sequencial),0)+1 
			from temporadas t
			inner join series s on s.id_serie=t.serie and s.nome = 'Ajin: Demi-Human'
		)
	); 


	

--incrementa um episodio na temporada da serie
	INSERT episodios  
   (nome, sinopse, temporada ,sequencial)  
VALUES  
   (
	   'Não tem nada a ver conosco',
	   ''  ,
	   ( 
	   --busca o id da temporada
	   select t.id_temporada from temporadas t
	   inner join series s on s.id_serie= t.serie and sequencial=1 and s.nome='Ajin: Demi-Human' 
	   ),
	   ( 
			--busca o ultimo sequencial da tamporada
			select isnull(max(e.sequencial),0)+1 
			from episodios e
			inner join temporadas t on t.id_temporada= e.temporada and t.sequencial=1   
			inner join series s on s.id_serie=t.serie and  s.nome='Ajin: Demi-Human'
		)
   ); 


   

--incrementa um episodio na temporada da serie
	INSERT episodios  
   (nome, sinopse, temporada ,sequencial)  
VALUES  
   (
	   'Como eu cheguei nisso? Eu não tenho a culpa',
	   ''  ,
	   ( 
	   --busca o id da temporada
	   select t.id_temporada from temporadas t
	   inner join series s on s.id_serie= t.serie and sequencial=1 and s.nome='Ajin: Demi-Human' 
	   ),
	   ( 
			--busca o ultimo sequencial da tamporada
			select isnull(max(e.sequencial),0)+1 
			from episodios e
			inner join temporadas t on t.id_temporada= e.temporada and t.sequencial=1   
			inner join series s on s.id_serie=t.serie and  s.nome='Ajin: Demi-Human'
		)
   ); 

	