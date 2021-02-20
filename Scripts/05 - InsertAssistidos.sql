


select * from assistido


select * from filme


select f.idfilme,f.nome,f.ano,f.sinopse,c.nome as categoria
from filme f
inner join categoria c on c.idcategoria= f.categoriaid;



select top 5
*,count(*) exibicoes from (
select f.nome,'filme' as tipo 
from assistido a
inner join filme f on f.idfilme= a.filmeid

union all

select s.nome,'serie' as tipo 
from assistido a
inner join serie s on s.idserie= a.serieid
) as uniao
group by nome,tipo
order by count(*)  desc

--------------------------------------------
INSERT assistido   (filmeid, em) VALUES  
   (
	   ( select idfilme from filme where nome = 'Matrix'),
	   '2020-02-01 10:00:00.000'
   ); 
   INSERT assistido   (filmeid, em) VALUES  
   (
	   ( select idfilme from filme where nome = 'Matrix'),
	   '2020-02-02 10:00:00.000'
   ); 
      INSERT assistido   (filmeid, em) VALUES  
   (
	   ( select idfilme from filme where nome = 'Matrix'),
	   '2020-02-03 10:00:00.000'
   ); 

--------------------------------------------------
INSERT assistido
   (serieid, em)  
VALUES  
   (
	   ( select idserie from serie where nome = 'Avatar: A Lenda de Aang'),
	   '2020-02-02 10:00:00.000'
   ); 
   INSERT assistido
   (serieid, em)  
VALUES  
   (
	   ( select idserie from serie where nome = 'Avatar: A Lenda de Aang'),
	   '2020-02-03 10:00:00.000'
   ); 


