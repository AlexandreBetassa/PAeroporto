create database aeroporto;

create table passageiro(
cpf varchar(11) constraint pk_passageiro primary key not null,
nome varchar(50) not null,
dataNasc date not null,
sexo varchar(10) not null,
ultimaCompra date,
dataCad date not null, 
situacao varchar(10),
);


create table venda (
id int identity constraint pk_venda primary key not null,
dataVenda date not null,
passageiroCpf varchar(11) foreign key (passageiroCpf) references passageiro (cpf) not null,
valotTotal float not null,
);

create table itemVenda(
valorUnit float not null,
idVenda int foreign key references venda(id) not null,
idItemVenda int identity constraint pk_itemVenda primary key not null,
);

create table companhiaAerea(
cnpj varchar(14) constraint pk_companhiaAerea primary key not null,
razaoSocial varchar not null,
dataAbertura date not null,
dataCadastro date not null,
ultimoVoo date not null,
situacao varchar not null,


);

create table aeronave(
inscAeronave varchar(6) constraint pk_aeronave primary key not null,
cnpjCompAerea varchar(14) foreign key references companhiaAerea(cnpj) not null,
capacidade int not null,
ultimaVenda date not null,
situacao varchar not null,
dataCadastro date not null,
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
dataVoo date not null,
dataCadastro date not null,
situacao varchar not null,
);

create table passagem(
idVoo int foreign key references voo(idVoo),
idPassagem int identity primary key,
aeronave varchar(6) foreign key references aeronave (inscAeronave),
valor float not null,
situacao varchar not null
);


