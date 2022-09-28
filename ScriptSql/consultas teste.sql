create table restritos(
cpf varchar(11) constraint pk_restritos primary key
);

create table bloqueados(
cnpj varchar(14) constraint pk_bloqueados primary key
);

INSERT INTO dbo.companhiaAerea (cnpj, razaoSocial, dataAbertura, dataCadastro, ultimoVoo, situacao)VALUES ('11233468000142','Teste', '22/05/1995 00:00:00','27/09/2022 13:47:09','27/09/2022 13:47:09','A';

select * from aeronave.

select aeronave.inscAeronave, companhiaAerea.razaoSocial, aeronave.capacidade, aeronave.ultimaVenda, aeronave.dataCadastro, aeronave.situacao
from dbo.aeronave, dbo.companhiaAerea
where aeronave.cnpjCompAerea = companhiaAerea.cnpj