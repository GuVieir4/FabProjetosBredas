# Tutorial de Instalação e Execução do Projeto KiraShop

Este tutorial irá guiá-lo através do processo de instalação e execução do projeto KiraShop, que consiste em um back-end em C# e um front-end em Vue.js.

## Índice

1. [Requisitos](#requisitos)
2. [Configuração do Ambiente](#configuração-do-ambiente)
3. [Instalação do Back-end](#instalação-do-back-end)
4. [Instalação do Front-end](#instalação-do-front-end)
5. [Execução do Projeto](#execução-do-projeto)
6. [Solução de Problemas Comuns](#solução-de-problemas-comuns)

## Requisitos

Antes de começar, certifique-se de que você tem os seguintes requisitos instalados em seu computador:

- **.NET SDK 8.0** ou superior
- **MySQL Server 8.0** ou superior
- **Node.js 16** ou superior
- **npm** ou **yarn**

## Configuração do Ambiente

### Instalação do .NET SDK

1. Acesse o site oficial do .NET: https://dotnet.microsoft.com/download
2. Baixe e instale o .NET SDK 8.0 ou superior para o seu sistema operacional
3. Verifique a instalação executando o seguinte comando no terminal:
   ```bash
   dotnet --version
   ```

### Instalação do MySQL Server

1. Acesse o site oficial do MySQL: https://dev.mysql.com/downloads/mysql/
2. Baixe e instale o MySQL Server 8.0 ou superior para o seu sistema operacional
3. Durante a instalação, defina a senha do usuário root
4. Verifique a instalação acessando o MySQL:
   ```bash
   mysql -u root -p
   ```

### Instalação do Node.js e npm

1. Acesse o site oficial do Node.js: https://nodejs.org/
2. Baixe e instale o Node.js 16 ou superior para o seu sistema operacional
3. O npm será instalado automaticamente junto com o Node.js
4. Verifique a instalação executando os seguintes comandos no terminal:
   ```bash
   node --version
   npm --version
   ```

## Instalação do Back-end

1. **Extraia o arquivo ZIP do back-end**:
   ```bash
   unzip Back-end.zip -d backend
   ```

2. **Navegue até o diretório do back-end**:
   ```bash
   cd backend
   ```

3. **Restaure as dependências do projeto**:
   ```bash
   dotnet restore
   ```

4. **Configure o banco de dados**:

   Abra o arquivo `appsettings.json` e configure a string de conexão com o banco de dados:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "server=localhost;user id=root;password=SuaSenha;database=kira2db"
     }
   }
   ```

   Substitua `SuaSenha` pela senha do seu usuário root do MySQL.

5. **Crie o banco de dados**:

   Primeiro, instale a ferramenta de linha de comando do Entity Framework:
   ```bash
   dotnet tool install --global dotnet-ef
   ```

   Em seguida, crie o banco de dados e aplique as migrações:
   ```bash
   dotnet ef database update
   ```

## Instalação do Front-end

1. **Extraia o arquivo ZIP do front-end**:
   ```bash
   unzip Front-end.zip -d frontend
   ```

2. **Navegue até o diretório do front-end**:
   ```bash
   cd frontend
   ```

3. **Instale as dependências do projeto**:
   ```bash
   npm install
   ```
   ou, se você preferir usar o yarn:
   ```bash
   yarn install
   ```

4. **Configure a URL da API**:

   Crie um arquivo `.env` no diretório raiz do front-end com o seguinte conteúdo:
   ```
   VITE_API_URL=http://localhost:5000/api
   ```

## Execução do Projeto

### Execução do Back-end

1. **Navegue até o diretório do back-end**:
   ```bash
   cd backend
   ```

2. **Execute o projeto**:
   ```bash
   dotnet run
   ```

   O back-end estará disponível em `http://localhost:5000`.

### Execução do Front-end

1. **Navegue até o diretório do front-end**:
   ```bash
   cd frontend
   ```

2. **Execute o projeto**:
   ```bash
   npm run dev
   ```
   ou, se você preferir usar o yarn:
   ```bash
   yarn dev
   ```

   O front-end estará disponível em `http://localhost:5173`.

3. **Acesse a aplicação**:

   Abra um navegador e acesse `http://localhost:5173`.

## Solução de Problemas Comuns

### Problema: Erro de conexão com o banco de dados

**Solução**:
- Verifique se o MySQL Server está em execução
- Verifique se a string de conexão no arquivo `appsettings.json` está correta
- Verifique se o usuário e senha do MySQL estão corretos
- Verifique se o banco de dados `kira2db` existe

### Problema: Erro ao executar as migrações

**Solução**:
- Verifique se o .NET SDK está instalado corretamente
- Verifique se a ferramenta de linha de comando do Entity Framework está instalada
- Tente executar o comando `dotnet ef migrations add Initial` antes de executar `dotnet ef database update`

### Problema: Erro ao instalar as dependências do front-end

**Solução**:
- Verifique se o Node.js e o npm estão instalados corretamente
- Tente limpar o cache do npm: `npm cache clean --force`
- Tente remover a pasta `node_modules` e o arquivo `package-lock.json` e execute `npm install` novamente

### Problema: O front-end não consegue se comunicar com o back-end

**Solução**:
- Verifique se o back-end está em execução
- Verifique se a URL da API no arquivo `.env` está correta
- Verifique se o CORS está configurado corretamente no back-end
- Verifique se não há firewall ou proxy bloqueando a comunicação

---

Se você encontrar algum problema não listado aqui, por favor, entre em contato com o suporte técnico.

**Autor**: Manus AI  
**Data**: 06/06/2025

