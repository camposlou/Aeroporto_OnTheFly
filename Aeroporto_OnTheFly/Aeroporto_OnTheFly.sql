CREATE DATABASE Aeroporto_OnTheFly;

USE Aeroporto_OnTheFly;

CREATE TABLE Passageiro(
	CPF varchar (11) NOT NULL,
	Nome varchar (50) NOT NULL,
	Sexo char (1) NOT NULL,
	Data_Nascimento Date NOT NULL,
	Situacao char (1) NOT NULL,
	Data_UltimaCompra Date NOT NULL,
	Data_Cadastro Date NOT NULL,

	CONSTRAINT PK__Passageiro_CPF PRIMARY KEY (CPF));
	   	 

drop table Passageiro;
select * from Passageiro

CREATE TABLE Companhia_Aerea(
	CNPJ varchar (14) NOT NULL,
	Razao_Social varchar (50) NOT NULL,
	Data_Abertura Date NOT NULL,
	Situacao char (1) NOT NULL,
	Data_Cadastro Date NOT NULL,
	Data_UltimoVoo DateTime NOT NULL,

	CONSTRAINT PK_Companhia_Aerea_CNPJ PRIMARY KEY (CNPJ));

drop table Companhia_Aerea;
SELECT *FROM Companhia_Aerea;	   	  

CREATE TABLE Aeronave(
	Inscricao varchar (6) NOT NULL,
	CNPJ VARCHAR (14) not null,
	Capacidade int NOT NULL,
	Situacao char (1) NOT NULL,
	Data_Cadastro Date NOT NULL,
	Data_UltimaVenda Date NOT NULL,


	CONSTRAINT PK_Aeronave_Incricao PRIMARY KEY (Inscricao),
	CONSTRAINT FK_CNPJ FOREIGN KEY (CNPJ) REFERENCES Companhia_Aerea(CNPJ));

CREATE TABLE InscricaoAeronave(
		Inscricao varchar (6) NOT NULL);

INSERT INTO InscricaoAeronave(Inscricao) 
VALUES  ('PT-010'),
		('PR-020'),
		('PP-030'),
		('PS-040'),
		('PH-050')

select *from InscricaoAeronave;

drop table Aeronave;



CREATE TABLE Voo(
	IDVoo varchar (5) NOT NULL,
	Inscricao_Aero varchar (6) NOT NULL, 
	Iatas varchar (3) NOT NULL,
	Data_HoraVoo Date NOT NULL,
	Situacao char (1),
	Assentos_Ocupados int NOT NULL

	CONSTRAINT PK_Voo_IDVoo PRIMARY KEY (IDVoo),
	FOREIGN KEY (Inscricao_Aero) REFERENCES Aeronave(Inscricao));
	
		
drop table Voo;
SELECT *FROM Voo;

CREATE TABLE IATAS(
	Iata varchar (3) NOT NULL, 
	Destino varchar(50) NOT NULL);
	
INSERT INTO IATAS (Iata, Destino) 
VALUES ('FLN', 'FLORIANÓLIS'),
       ('SDU', 'SANTOS DUMMONT'),
	   ('FOR', 'FORTALEZA'),
	   ('MCZ', 'MACEIÓ'),
	   ('REC', 'RECIFE'),
	   ('JPA', 'JOÃO PESSOA'),
	   ('SLZ', 'SÃO LUIZ'),
	   ('NAT', 'NATAL'),
	   ('PMW', 'PALMAS'),
	   ('SSA', 'SALVADOR')

SELECT * FROM IATAS;
DROP TABLE IATAS;


CREATE TABLE Passagem(
	IDPassagem varchar (6) NOT NULL,
	IDVoo varchar (5) NOT NULL,
	Valor float NOT NULL,
	Situacao char (1) NOT NULL,
	Data_UltimaOperacao DateTime NOT NULL,

	CONSTRAINT PK_Passagem_IDPassagem PRIMARY KEY (IDPassagem),
	CONSTRAINT FK_IDVoo FOREIGN KEY (IDVoo) REFERENCES Voo(IDVoo));

drop table Passagem;
select * from Passagem;

CREATE TABLE Venda(
	IDVenda varchar (5) NOT NULL, 
	CPF varchar (11) NOT NULL,	
	Data_Venda DateTime NOT NULL,	
	Valor_Total float NOT NULL,
	Valor_Unitario float NOT NULL,
	IDPassagem varchar (6) NOT NULL,

	CONSTRAINT PK_Venda_IDVenda PRIMARY KEY (IDVenda),
	CONSTRAINT FK_CPF FOREIGN KEY (CPF) REFERENCES Passageiro(CPF),
	CONSTRAINT FK_IDPassagem FOREIGN KEY (IDPassagem) REFERENCES Passagem(IDPassagem));


drop table Venda;
select * from Venda;


CREATE TABLE CadastrosBloqueados(
	CPF varchar (11) NOT NULL,

	CONSTRAINT PK__CadastrosBloqueados_CPF PRIMARY KEY (CPF));

INSERT INTO CadastrosBloqueados(CPF) 
VALUES ('22608272843')

drop table CadastrosBloqueados;
SELECT *FROM CadastrosBloqueados;

CREATE TABLE CadastrosRestritos(
	CNPJ varchar (14) NOT NULL,

	CONSTRAINT PK__CadastrosRestritos_CNPJ PRIMARY KEY (CNPJ));

INSERT INTO CadastrosRestritos(CNPJ) 
VALUES ('05432274000160')

drop table CadastrosRestritos;
SELECT *FROM CadastrosRestritos;


