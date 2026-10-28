CREATE DATABASE Cadastro_Empregados

USE Cadastro_Empregados

CREATE TABLE Setores
(
    SetorId INT PRIMARY KEY IDENTITY(1,1),
    Nome    NVARCHAR(100) NOT NULL
);

CREATE TABLE Empregados
(
    Id       INT PRIMARY KEY IDENTITY(1,1),
    Nome     NVARCHAR(100) NOT NULL,
    Email    NVARCHAR(100) NOT NULL UNIQUE,
    Telefone NVARCHAR(30),
    SetorId  INT NULL,
    CONSTRAINT FK_Empregados_Setores
        FOREIGN KEY (SetorId) REFERENCES Setores(SetorId)
);