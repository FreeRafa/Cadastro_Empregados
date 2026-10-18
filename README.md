# Sistema de Cadastro de Empregados

Aplicação desenvolvida em C# para gerir o cadastro de empregados, com persistência de dados em SQL Server por meio do ADO.NET.

## Tecnologias utilizadas

- **C#** — linguagem de programação
- **ADO.NET** — acesso e comunicação com o banco de dados
- **SQL Server** — armazenamento dos dados

## Funcionalidades

- Cadastrar empregados
- Listar empregados cadastrados
- Atualizar os dados de um empregado
- Remover empregados

## Banco de dados

Os scripts SQL necessários estão disponíveis na pasta [`SQL`](./SQL). Execute-os no SQL Server para criar e preparar o banco de dados.

## Requisitos

- SQL Server
- .NET SDK compatível com o projeto

## Como executar

1. Clone ou baixe este repositório.
2. Execute os scripts SQL da pasta `SQL`.
3. Configure a connection string para apontar para a sua instância do SQL Server.
4. Abra a solução no Visual Studio ou execute o projeto pelo terminal:

