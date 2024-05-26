CREATE DATABASE VERDEVIDA
USE VERDEVIDA

CREATE TABLE ADMIN(
idAdmin int not null IDENTITY (1,1),
nomeAdmin varchar(50) not null,
cpfAdmin char(11) not null,
emailAdmin varchar(100) not null,
senhaAdmin varchar(100) not null,
funcaoAdmin char(15) not null,

PRIMARY KEY (idAdmin)
)

CREATE TABLE FAZENDA(
idFazenda int not null IDENTITY (1,1),
nome varchar(50) not null,
cnpj char(14) not null,
idAdmin int not null,

PRIMARY KEY (idFazenda),
FOREIGN KEY (idAdmin) REFERENCES ADMIN (idAdmin)
)

CREATE TABLE PRODUTO(
idProduto int not null,
nomeProduto char (15) not null,

PRIMARY KEY (idProduto),
)

CREATE TABLE FORNECEDOR(
idFornecedor int not null IDENTITY (1,1),
nomeFornecedor varchar(100) not null,
cnpjFornecedor char(14) not null,
cepFornecedor char (8) not null,
emailFornecedor varchar(100) not null,
telFornecedor char (11) not null,
tipoProduto char(50) not null,

PRIMARY KEY (idFornecedor),
)

CREATE TABLE ESTOQUE_FORNECEDOR(
idEstoqueFornecedor int not null IDENTITY (1,1),
qntProdutoFornecedor int,
idFornecedor int not null,

PRIMARY KEY (idEstoqueFornecedor),
FOREIGN KEY (idFornecedor) REFERENCES FORNECEDOR (idFornecedor)
)

CREATE TABLE DRONE(
idDrone int not null IDENTITY (1,1),
tipoDrone char (10) not null,
funcaoDrone char (20) not null,
idFazenda int not null,

PRIMARY KEY (idDrone),
FOREIGN KEY (idFazenda) REFERENCES FAZENDA (idFazenda)
)

CREATE TABLE SENSOR(
idSensor int not null IDENTITY (1,1),
funcaoSensor char (20) not null,
idFazenda int not null,

PRIMARY KEY (idSensor),
FOREIGN KEY (idFazenda) REFERENCES FAZENDA (idFazenda)
)

CREATE TABLE COLABORADOR(
idColaborador int not null IDENTITY (1,1),
nomeColaborador varchar (50) not null,
cpfColaborador char (11) not null,
funcaoColaborador varchar(50),
idFazenda int not null,

PRIMARY KEY (idColaborador),
FOREIGN KEY (idFazenda) REFERENCES FAZENDA (idFazenda)
)

CREATE TABLE CLIENTE(
idCliente int NOT NULL IDENTITY (1,1),
nomeCliente varchar (50) not null,
cpfCliente char (11) not null,
dataNascimento char (10) not null,
cepCliente char (8) not null,
telCliente char (11) not null,
emailCliente varchar (100) not null,

PRIMARY KEY (idCliente)
)

CREATE TABLE COMPRA(
idCompra int not null IDENTITY (1,1),
totalCompra DECIMAL not null,
dataCompra DATETIME not null DEFAULT GETDATE(),
metodoPagamento varchar(25) not null,

PRIMARY KEY (idCompra),
)

ALTER TABLE CLIENTE
ADD senhaCliente varchar(100) not null
ALTER TABLE CLIENTE
ALTER COLUMN dataNascimento DATE not null
ALTER TABLE CLIENTE
ADD endereco varchar(150) not null

ALTER TABLE PRODUTO
ADD precoProduto int not null

SELECT * FROM ADMIN
INSERT INTO ADMIN (nomeAdmin, cpfAdmin, emailAdmin, senhaAdmin, funcaoAdmin)
VALUES ('Jason', '13131313131', 'jasonvoultress@gmail.com', 'shishishi', 'gerente')
SELECT * FROM ADMIN
DELETE FROM ADMIN
WHERE idAdmin=3
SELECT * FROM ADMIN
UPDATE ADMIN
SET nomeAdmin = 'Ivanildo'
WHERE nomeAdmin = 'Ivan'
SELECT * FROM ADMIN

INSERT INTO PRODUTO (idProduto, nomeProduto, precoProduto)
VALUES (1, 'Tomate', 3.5);
INSERT INTO PRODUTO (idProduto, nomeProduto, precoProduto)
VALUES (2, 'Cenoura', 2.8);
INSERT INTO PRODUTO (idProduto, nomeProduto, precoProduto)
VALUES (3, 'Alface', 1.2);
INSERT INTO PRODUTO (idProduto, nomeProduto, precoProduto)
VALUES (4, 'Batata', 4.0);
INSERT INTO PRODUTO (idProduto, nomeProduto, precoProduto)
VALUES (5, 'Pepino', 2.3);
INSERT INTO PRODUTO (idProduto, nomeProduto, precoProduto)
VALUES (6, 'Beterraba', 2.7);
INSERT INTO PRODUTO (idProduto, nomeProduto, precoProduto)
VALUES (7, 'Cebola', 1.8);
INSERT INTO PRODUTO (idProduto, nomeProduto, precoProduto)
VALUES (8, 'Alho', 5.5);


SELECT * FROM ADMIN
SELECT * FROM PRODUTO
SELECT * FROM CLIENTE
SELECT * FROM COMPRA