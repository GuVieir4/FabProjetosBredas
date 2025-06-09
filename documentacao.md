# Documentação do Projeto KiraShop

## Índice

1. [Introdução](#introdução)
2. [Estrutura do Projeto](#estrutura-do-projeto)
   - [Back-end (C#)](#back-end-c)
   - [Front-end (Vue.js)](#front-end-vuejs)
3. [Funcionalidades Implementadas](#funcionalidades-implementadas)
4. [APIs Disponíveis](#apis-disponíveis)
5. [Tutorial de Instalação e Execução](#tutorial-de-instalação-e-execução)
   - [Requisitos](#requisitos)
   - [Configuração do Back-end](#configuração-do-back-end)
   - [Configuração do Front-end](#configuração-do-front-end)
   - [Execução do Projeto](#execução-do-projeto)
6. [Considerações Finais](#considerações-finais)

## Introdução

Este documento apresenta a documentação do projeto KiraShop, uma aplicação web para gerenciamento de produtos de uma loja de periféricos de computador. O projeto é composto por um back-end desenvolvido em C# com ASP.NET Core e um front-end desenvolvido em Vue.js.

O objetivo principal deste projeto é fornecer uma plataforma para gerenciamento de produtos, categorias e marcas, permitindo operações CRUD (Create, Read, Update, Delete) e a visualização de produtos por categorias e marcas.

## Estrutura do Projeto

### Back-end (C#)

O back-end do projeto foi desenvolvido utilizando C# com ASP.NET Core, seguindo o padrão MVC (Model-View-Controller). A estrutura de diretórios do back-end é a seguinte:

```
backend/
├── Controllers/         # Controladores da API
│   ├── CarrinhoController.cs
│   ├── CategoriaController.cs
│   ├── MarcaController.cs
│   ├── ProdutoController.cs
│   └── UsuarioController.cs
├── Dtos/                # Objetos de Transferência de Dados
├── Migrations/          # Migrações do Entity Framework
├── Models/              # Modelos de dados
│   ├── Carrinho.cs
│   ├── Categoria.cs
│   ├── Marca.cs
│   ├── Produto.cs
│   └── Usuario.cs
├── Properties/          # Configurações do projeto
├── Kira2DbContext.cs    # Contexto do banco de dados
├── KiraApi2.csproj      # Arquivo de projeto
├── Program.cs           # Ponto de entrada da aplicação
└── appsettings.json     # Configurações da aplicação
```

#### Modelos de Dados

Os principais modelos de dados do back-end são:

1. **Produto**: Representa um produto da loja, com propriedades como Nome, Preço, Descrição, etc.
2. **Categoria**: Representa uma categoria de produtos, como Mouses, Teclados, etc.
3. **Marca**: Representa uma marca de produtos, como Redragon, Razer, etc.
4. **Carrinho**: Representa um carrinho de compras de um usuário.
5. **Usuario**: Representa um usuário da aplicação.

#### Controllers

Os controllers são responsáveis por expor as APIs RESTful para manipulação dos modelos de dados:

1. **ProdutoController**: Gerencia operações CRUD para produtos e busca de produtos por categoria e marca.
2. **CategoriaController**: Gerencia operações CRUD para categorias.
3. **MarcaController**: Gerencia operações CRUD para marcas.
4. **CarrinhoController**: Gerencia operações relacionadas ao carrinho de compras.
5. **UsuarioController**: Gerencia operações relacionadas aos usuários.

### Front-end (Vue.js)

O front-end do projeto foi desenvolvido utilizando Vue.js, um framework JavaScript progressivo para construção de interfaces de usuário. A estrutura de diretórios do front-end é a seguinte:

```
frontend/
├── public/              # Arquivos públicos
├── src/                 # Código-fonte
│   ├── assets/          # Recursos estáticos (imagens, etc.)
│   ├── components/      # Componentes Vue
│   │   ├── Avaliacoes/
│   │   ├── CardsCrud/
│   │   ├── CardsPromocionais/
│   │   ├── Categorias/
│   │   ├── DetalhesProduto/
│   │   ├── Filtros/
│   │   ├── Footer/
│   │   ├── GraficosProdutos/
│   │   ├── Lancamentos/
│   │   ├── Navbar/
│   │   ├── ProdutosCategoria/
│   │   ├── ProdutosMarca/
│   │   ├── PromoRegister/
│   │   ├── SecaoDestaque/
│   │   ├── SecaoMarcas/
│   │   ├── Sidebar/
│   │   ├── SuperFeirao/
│   │   └── TabelaProdutos/
│   ├── router/          # Configuração de rotas
│   ├── services/        # Serviços para comunicação com a API
│   │   ├── api.js
│   │   ├── categoriaService.js
│   │   ├── index.js
│   │   ├── marcaService.js
│   │   └── produtoService.js
│   ├── views/           # Páginas da aplicação
│   │   ├── AdminDashboardView.vue
│   │   ├── CategoryPageView.vue
│   │   ├── HomepageView.vue
│   │   ├── KartView.vue
│   │   ├── MarcaPageView.vue
│   │   ├── NotFoundErrorView.vue
│   │   └── ProductDetailsView.vue
│   ├── App.vue          # Componente raiz
│   └── main.js          # Ponto de entrada da aplicação
├── .env                 # Variáveis de ambiente
├── package.json         # Dependências do projeto
└── vite.config.js       # Configuração do Vite
```

#### Componentes Principais

1. **Categorias**: Exibe as categorias de produtos disponíveis.
2. **ProdutosCategoria**: Exibe os produtos de uma categoria específica.
3. **SecaoMarcas**: Exibe as marcas disponíveis.
4. **ProdutosMarca**: Exibe os produtos de uma marca específica.
5. **TabelaProdutos**: Exibe uma tabela com todos os produtos (painel administrativo).
6. **CardsCrud**: Fornece funcionalidades CRUD para produtos (painel administrativo).

#### Views (Páginas)

1. **HomepageView**: Página inicial da aplicação.
2. **CategoryPageView**: Página que exibe produtos de uma categoria específica.
3. **MarcaPageView**: Página que exibe produtos de uma marca específica.
4. **AdminDashboardView**: Painel administrativo para gerenciamento de produtos.
5. **ProductDetailsView**: Página de detalhes de um produto específico.
6. **KartView**: Página do carrinho de compras.
7. **NotFoundErrorView**: Página de erro 404.

#### Serviços

Os serviços são responsáveis pela comunicação com a API do back-end:

1. **api.js**: Configuração base do Axios para comunicação com a API.
2. **produtoService.js**: Serviço para operações CRUD de produtos.
3. **categoriaService.js**: Serviço para operações CRUD de categorias.
4. **marcaService.js**: Serviço para operações CRUD de marcas.

## Funcionalidades Implementadas

### Back-end

1. **Gerenciamento de Produtos**:
   - Criação, leitura, atualização e exclusão de produtos
   - Busca de produtos por categoria
   - Busca de produtos por marca

2. **Gerenciamento de Categorias**:
   - Criação, leitura, atualização e exclusão de categorias

3. **Gerenciamento de Marcas**:
   - Criação, leitura, atualização e exclusão de marcas

### Front-end

1. **Página Inicial**:
   - Exibição de produtos em destaque
   - Exibição de categorias
   - Exibição de marcas

2. **Página de Categoria**:
   - Exibição de produtos de uma categoria específica
   - Filtros para refinamento da busca

3. **Página de Marca**:
   - Exibição de produtos de uma marca específica
   - Filtros para refinamento da busca

4. **Painel Administrativo**:
   - Exibição de todos os produtos em uma tabela
   - Funcionalidades CRUD para produtos
   - Gráficos de produtos

## APIs Disponíveis

### Produtos

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | /api/Produto | Retorna todos os produtos |
| GET | /api/Produto/{id} | Retorna um produto específico |
| GET | /api/Produto/categoria/{categoriaId} | Retorna produtos de uma categoria específica |
| GET | /api/Produto/marca/{marcaId} | Retorna produtos de uma marca específica |
| POST | /api/Produto | Cria um novo produto |
| PUT | /api/Produto/{id} | Atualiza um produto existente |
| DELETE | /api/Produto/{id} | Exclui um produto |

### Categorias

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | /api/Categoria | Retorna todas as categorias |
| GET | /api/Categoria/{id} | Retorna uma categoria específica |
| POST | /api/Categoria | Cria uma nova categoria |
| PUT | /api/Categoria/{id} | Atualiza uma categoria existente |
| DELETE | /api/Categoria/{id} | Exclui uma categoria |

### Marcas

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | /api/Marca | Retorna todas as marcas |
| GET | /api/Marca/{id} | Retorna uma marca específica |
| POST | /api/Marca | Cria uma nova marca |
| PUT | /api/Marca/{id} | Atualiza uma marca existente |
| DELETE | /api/Marca/{id} | Exclui uma marca |

## Tutorial de Instalação e Execução

### Requisitos

Para executar o projeto, você precisará ter instalado em seu computador:

1. **.NET SDK 8.0** ou superior
2. **MySQL Server 8.0** ou superior
3. **Node.js 16** ou superior
4. **npm** ou **yarn**

### Configuração do Back-end

1. **Configuração do Banco de Dados**:

   Abra o arquivo `appsettings.json` no diretório raiz do back-end e configure a string de conexão com o banco de dados:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "server=localhost;user id=root;password=mudar;database=kira2db"
     }
   }
   ```

   Substitua `root` e `mudar` pelo seu usuário e senha do MySQL, respectivamente.

2. **Criação do Banco de Dados**:

   Abra um terminal no diretório raiz do back-end e execute os seguintes comandos:

   ```bash
   dotnet ef database update
   ```

   Este comando irá criar o banco de dados e aplicar todas as migrações.

3. **Compilação e Execução**:

   Ainda no terminal, execute os seguintes comandos:

   ```bash
   dotnet build
   dotnet run
   ```

   O back-end estará disponível em `http://localhost:5000`.

### Configuração do Front-end

1. **Instalação de Dependências**:

   Abra um terminal no diretório raiz do front-end e execute o seguinte comando:

   ```bash
   npm install
   ```

   ou, se você preferir usar o yarn:

   ```bash
   yarn install
   ```

2. **Configuração da API**:

   Crie um arquivo `.env` no diretório raiz do front-end com o seguinte conteúdo:

   ```
   VITE_API_URL=http://localhost:5000/api
   ```

   Substitua a URL pela URL do seu back-end, se necessário.

3. **Compilação e Execução**:

   Ainda no terminal, execute o seguinte comando:

   ```bash
   npm run dev
   ```

   ou, se você preferir usar o yarn:

   ```bash
   yarn dev
   ```

   O front-end estará disponível em `http://localhost:5173`.

### Execução do Projeto

1. **Acesso à Aplicação**:

   Abra um navegador e acesse `http://localhost:5173` para acessar a aplicação.

2. **Acesso ao Painel Administrativo**:

   Para acessar o painel administrativo, acesse `http://localhost:5173/adminDashboard`.

## Considerações Finais

Este projeto foi desenvolvido com o objetivo de fornecer uma plataforma para gerenciamento de produtos de uma loja de periféricos de computador. As funcionalidades implementadas permitem a criação, leitura, atualização e exclusão de produtos, categorias e marcas, além da visualização de produtos por categorias e marcas.

O projeto pode ser expandido com novas funcionalidades, como autenticação de usuários, carrinho de compras funcional, processamento de pagamentos, entre outras.

---

**Autor**: Manus AI  
**Data**: 06/06/2025

