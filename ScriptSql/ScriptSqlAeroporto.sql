create database aeroporto;

create table passageiro(
cpf varchar(11) constraint pk_passageiro primary key not null,
nome varchar(50) not null,
dataNasc date not null,
sexo varchar(10) not null,
ultimaCompra datetime,
dataCad datetime not null, 
situacao char(1)
);

create table venda (
id int identity constraint pk_venda primary key not null,
dataVenda datetime not null,
passageiroCpf varchar(11) foreign key (passageiroCpf) references passageiro (cpf) not null,
valotTotal float not null,
);

create table itemVenda(
idVenda int foreign key references venda(id) not null,
idItemVenda int identity,
idPassagem int 

constraint pk_itemVenda primary key(idVenda, idItemVenda, idPassagem)
);

create table companhiaAerea(
cnpj varchar(14) constraint pk_companhiaAerea primary key not null,
razaoSocial varchar not null,
dataAbertura date not null,
dataCadastro datetime not null,
ultimoVoo datetime not null,
situacao char(1)
);

create table aeronave(
inscAeronave varchar(6) constraint pk_aeronave primary key not null,
cnpjCompAerea varchar(14) foreign key references companhiaAerea(cnpj) not null,
capacidade int not null,
ultimaVenda datetime not null,
situacao char(1),
dataCadastrotime date not null,
);


create table iatas(
nomeAeroporto varchar not null,
sigla varchar(3) constraint pk_iata primary key not null,
);

create table voo(
IdVoo int identity constraint pk_voo primary key,
assentosOcupado int not null,
destino varchar(3) foreign key references iatas(sigla) not null,
aeronave varchar(6) foreign key references aeronave(inscAeronave) not null,
dataVoo datetime not null,
dataCadastro datetime not null,
situacao char(1),
);

create table passagem(
idVoo int foreign key references voo(idVoo),
idPassagem int,
valor float not null,
situacao char(1),
dataCadastro datetime,
constraint pk_passagem primary key(idPassagem,idVoo)
);

--procedure para geração de passagens automatica
--executada quando inserido novo voo
CREATE PROCEDURE CadastroPassagens (@valor float)
AS 
    BEGIN
	declare 
	@idPassagem int,
	@idVoo int,
	@count int = 0,
	@situacao char= 'L', 
	@qtd int,
	@dataCadastro DateTime = GetDate()

	SELECT @idVoo  = MAX(IdVoo) FROM dbo.voo
	SELECT @idPassagem = MAX(idPassagem) FROM dbo.passagem
	SELECT @qtd = capacidade FROM aeronave, voo WHERE inscAeronave = voo.aeronave AND iDvoo = @idVoo
	SELECT @idPassagem = ISNULL(@idPassagem,1)
	
	WHILE @count <= @qtd
		BEGIN
	        INSERT INTO dbo.passagem (idPassagem, idVoo, valor, situacao, dataCadastro) VALUES(@idPassagem, @idVoo, @valor, @situacao, @dataCadastro)
			SET @count = @count + 1
			SET @idPassagem = @idPassagem + 1
		END
END

--trigger para cancelamento de passagem automatico em caso de cancelamento de voo
CREATE TRIGGER CancelarPassagens ON voo
AFTER UPDATE
AS
IF UPDATE(situacao)
BEGIN
	DECLARE
	@idVoo int

	SELECT @idVoo = idVoo FROM inserted
	UPDATE dbo.passagem SET situacao = 'C' WHERE idVoo = @idVoo
END

--trigger para incremento de assentos ocupados a acada venda de passagem em voo
create trigger AssenctosOcupados on passagem
after update
as if
update(situacao)
begin
	declare 
	@idVoo int,
	@idPassagem int,
	@assentosOcupados int,
	@situacao char


	select @idVoo = idVoo, @situacao = situacao, @idPassagem = idPassagem from inserted
	select @assentosOcupados = ISNULL(assentosOcupado,0)
	from voo
	where idVoo = @idVoo

	if(@situacao <> 'L')
		begin
		update dbo.voo set assentosOcupado += 1 where voo.idVoo = @idVoo
		end
	else
		begin
		update dbo.voo set assentosOcupado -= 1 where voo.idVoo = @idVoo
		end
end

