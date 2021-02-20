


select * from assistidos


select * from filmes


select f.id_filme,f.nome,f.ano,f.sinopse,c.nome as categoria
from filmes f
inner join categorias c on c.id_categoria= f.categoria;



select top 5
*,count(*) exibicoes from (
select f.nome,'filme' as tipo 
from assistidos a
inner join filmes f on f.id_filme= a.filme

union all

select s.nome,'serie' as tipo 
from assistidos a
inner join series s on s.id_serie= a.serie
) as uniao
group by nome,tipo
order by count(*)  desc

--------------------------------------------
INSERT assistidos   (filme, dataehora) VALUES  
   (
	   ( select id_filme from filmes where nome = 'Matrix'),
	   '2020-01-01 10:00:00.000'
   ); 
   INSERT assistidos   (filme, dataehora) VALUES  
   (
	   ( select id_filme from filmes where nome = 'Matrix'),
	   '2020-01-02 10:00:00.000'
   ); 
      INSERT assistidos   (filme, dataehora) VALUES  
   (
	   ( select id_filme from filmes where nome = 'Matrix'),
	   '2020-01-03 10:00:00.000'
   ); 

--------------------------------------------------
INSERT assistidos  
   (serie, dataehora)  
VALUES  
   (
	   ( select id_serie from series where nome = 'Avatar: A Lenda de Aang'),
	   '2020-01-02 10:00:00.000'
   ); 
   INSERT assistidos  
   (serie, dataehora)  
VALUES  
   (
	   ( select id_serie from series where nome = 'Avatar: A Lenda de Aang'),
	   '2020-01-03 10:00:00.000'
   ); 


