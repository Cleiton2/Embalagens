IF DB_ID('EmbalagensDb') IS NULL
BEGIN
    CREATE DATABASE EmbalagensDb;
END
GO

USE EmbalagensDb;
GO

-- Remove as tabelas se já existirem
IF OBJECT_ID('dbo.Pedido', 'U') IS NOT NULL
DROP TABLE dbo.Pedido;
GO

IF OBJECT_ID('dbo.Caixas', 'U') IS NOT NULL
DROP TABLE dbo.Caixas;
GO

-- Cria as tabelas
CREATE TABLE Caixas (
    IdCaixa NVARCHAR(50) PRIMARY KEY NOT NULL,
    Altura INT NOT NULL,
    Largura INT NOT NULL,
    Comprimento INT NOT NULL
);
GO

CREATE TABLE Pedido (
    Id INT PRIMARY KEY,
    IdCaixas NVARCHAR(200) NOT NULL
);
GO

-- Popula a tabela Caixas se estiver vazia
INSERT INTO Caixas (IdCaixa, Altura, Largura, Comprimento) VALUES
('Caixa 1', 30, 40, 80),
('Caixa 2', 80, 50, 40),
('Caixa 3', 50, 80, 60);
GO
