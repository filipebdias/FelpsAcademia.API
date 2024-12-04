# FelpsAcademia API

## Pré-requisitos

Antes de rodar o projeto, você precisa garantir que as seguintes ferramentas estejam instaladas:

1. **.NET 6.0 ou superior**
   - Baixe o SDK do .NET Core [aqui](https://dotnet.microsoft.com/download).
   
2. **SQL Server** 
   - Este projeto utiliza o **SQL Server**, mas você pode adaptá-lo para outro banco de dados relacional, como o MySQL.
   - Caso não tenha o SQL Server, pode instalar o [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (versão gratuita).
   
3. **Ferramenta para gerenciar o banco de dados**
   - **SQL Server Management Studio (SSMS)** 
   - **Visual Studio Code** ou **Visual Studio** para editar o código e rodar o projeto.

## Para rodar o projeto
1 - Primeiro baixe o zip do projeto ou clone do git para o visual studio.
2- Vá para a pasta do projeto e clique com o botão direito em cima do arquivo "FelpsAcademia.API.csproj" e abra com o visual studio.
3- Quando abrir no visual studio vá em Ferramentas - Gerenciador de pacotes nuget- abra o console e de o comando "Update-Package".
4- Já com o sql server configurado crie o database "FelpsAcademia"
5- clique para iniciar.


## Descrição

Este projeto é uma API para gerenciar informações de uma academia, incluindo funcionalidades como registro de usuários, treinos, planos de treino, categorias de treino, e mais. A API é desenvolvida com o ASP.NET Core, utilizando o Entity Framework Core para interagir com o banco de dados.

## Tecnologias Utilizadas

* **ASP.NET Core 6.0** ou superior
* **Entity Framework Core**
* **SQL Server** (ou outro banco de dados relacional compatível)

## Configuração do Banco de Dados e Servidor

### Passo 1: Criação do Banco de Dados

1. **Abra o SQL Server Management Studio (SSMS)** e conecte-se ao servidor de banco de dados.
   
2. **Criação do Banco de Dados**:
   - Crie um banco de dados no SQL Server com o nome `FelpsAcademia` 

3. **Configuração da String de Conexão**:
   - No projeto, abra o arquivo `appsettings.json` e localize a seção `"ConnectionStrings"`. Configure a string de conexão com seu banco de dados SQL Server (ou outro banco de dados, se aplicável):
     ```json
    "ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-HO3RH2S;Database=FelpsAcademia;Trusted_Connection=True;Encrypt=False;"
}
     ```
    // no Server= "coloque o seu servidor"
   

### Passo 2: Executando as Migrations

O Entity Framework Core gerencia a criação das tabelas no banco de dados por meio de **migrations**.

1. **Abra o terminal ou Prompt de Comando**.
   
2. **Navegue até o diretório do projeto** onde está o arquivo `.csproj` do projeto.

3. **Crie a primeira migration** executando o seguinte comando:
   ```bash
   dotnet ef migrations add InitialCreate
