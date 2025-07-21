# 💻 Documentação Completa do Projeto KiraShop 🛍️

Este documento serve como um guia abrangente para o projeto KiraShop, uma aplicação web desenvolvida para o gerenciamento de produtos em uma loja de periféricos de computador. O projeto é dividido em duas partes principais: um back-end construído com C# e ASP.NET Core, e um front-end interativo desenvolvido com Vue.js.

## 🌟 Visão Geral do Projeto

O KiraShop foi concebido para oferecer uma plataforma robusta e intuitiva para a administração de produtos, categorias e marcas. Ele suporta operações CRUD (Create, Read, Update, Delete) completas e permite a visualização organizada de produtos por suas respectivas categorias e marcas.

## 🏗️ Estrutura do Projeto

O projeto é modular, com diretórios dedicados para o back-end e o front-end, facilitando o desenvolvimento e a manutenção.

### ⚙️ Back-end (C# com ASP.NET Core)

O back-end é a espinha dorsal do KiraShop, responsável pela lógica de negócios, persistência de dados e exposição de APIs. Ele segue o padrão arquitetural MVC (Model-View-Controller).

```
backend/
├── Controllers/         # Controladores da API que gerenciam as requisições HTTP
│   ├── CarrinhoController.cs
│   ├── CategoriaController.cs
│   ├── MarcaController.cs
│   ├── ProdutoController.cs
│   └── UsuarioController.cs
├── Dtos/                # Objetos de Transferência de Dados (Data Transfer Objects)
├── Migrations/          # Migrações do Entity Framework para gerenciamento do esquema do banco de dados
├── Models/              # Modelos de dados que representam as entidades do banco de dados
│   ├── Carrinho.cs
│   ├── Categoria.cs
│   ├── Marca.cs
│   ├── Produto.cs
│   └── Usuario.cs
├── Properties/          # Configurações específicas do projeto
├── Kira2DbContext.cs    # Contexto do banco de dados, configurado com Entity Framework Core
├── KiraApi2.csproj      # Arquivo de projeto C#
├── Program.cs           # Ponto de entrada da aplicação back-end
└── appsettings.json     # Configurações da aplicação, incluindo strings de conexão com o banco de dados
```

#### 📊 Modelos de Dados Chave

- **Produto**: Detalhes de cada item disponível na loja (Nome, Preço, Descrição, etc.).
- **Categoria**: Agrupamento de produtos (ex: Mouses, Teclados).
- **Marca**: Fabricantes dos produtos (ex: Redragon, Razer).
- **Carrinho**: Itens selecionados por um usuário para compra.
- **Usuario**: Informações dos usuários da aplicação.

#### 🌐 Controladores (Controllers)

Os controladores expõem as APIs RESTful, permitindo a interação com os modelos de dados:

- **ProdutoController**: Gerencia produtos, incluindo busca por categoria e marca.
- **CategoriaController**: Gerencia as categorias de produtos.
- **MarcaController**: Gerencia as marcas de produtos.
- **CarrinhoController**: Lida com operações relacionadas ao carrinho de compras.
- **UsuarioController**: Gerencia informações e autenticação de usuários.

### 🖥️ Front-end (Vue.js)

O front-end oferece a interface de usuário, construída com Vue.js para uma experiência dinâmica e responsiva.

```
frontend/
├── public/              # Arquivos estáticos e públicos
├── src/                 # Código-fonte da aplicação Vue.js
│   ├── assets/          # Recursos como imagens e ícones
│   ├── components/      # Componentes Vue reutilizáveis
│   │   ├── Avaliacoes/
│   │   ├── Categorias/
│   │   ├── Filtros/
│   │   ├── Footer/
│   │   ├── Navbar/
│   │   └── TabelaProdutos/
│   ├── router/          # Configuração das rotas da aplicação
│   ├── services/        # Módulos para comunicação com a API do back-end
│   │   ├── api.js
│   │   ├── categoriaService.js
│   │   ├── index.js
│   │   ├── marcaService.js
│   │   └── produtoService.js
│   ├── views/           # Páginas principais da aplicação
│   │   ├── AdminDashboardView.vue
│   │   ├── CategoryPageView.vue
│   │   ├── HomepageView.vue
│   │   ├── KartView.vue
│   │   ├── MarcaPageView.vue
│   │   ├── NotFoundErrorView.vue
│   │   └── ProductDetailsView.vue
│   ├── App.vue          # Componente raiz da aplicação Vue
│   └── main.js          # Ponto de entrada da aplicação Vue
├── .env                 # Variáveis de ambiente para o front-end
├── package.json         # Dependências do projeto e scripts npm
└── vite.config.js       # Configuração do Vite
```

#### 🧩 Componentes e Páginas Principais

- **Componentes**: `Categorias`, `ProdutosCategoria`, `SecaoMarcas`, `ProdutosMarca`, `TabelaProdutos`, `CardsCrud` são alguns exemplos que gerenciam a exibição e interação com os dados.
- **Páginas (Views)**: `HomepageView` (página inicial), `CategoryPageView`, `MarcaPageView`, `AdminDashboardView` (painel administrativo), `ProductDetailsView`, `KartView` (carrinho de compras) e `NotFoundErrorView`.

#### 📡 Serviços de Comunicação com a API

Os serviços (`api.js`, `produtoService.js`, `categoriaService.js`, `marcaService.js`) são responsáveis por orquestrar as requisições HTTP entre o front-end e o back-end.

## ✨ Funcionalidades Implementadas

### 🔙 Back-end

- **Gerenciamento de Produtos**: CRUD completo, busca por categoria e marca.
- **Gerenciamento de Categorias**: CRUD completo.
- **Gerenciamento de Marcas**: CRUD completo.

### ➡️ Front-end

- **Página Inicial**: Exibição de produtos em destaque, categorias e marcas.
- **Páginas de Categoria e Marca**: Exibição de produtos filtrados e opções de refinamento.
- **Painel Administrativo**: Tabela de produtos, funcionalidades CRUD e gráficos.

## 🔗 Endpoints Disponíveis

Os seguintes Endpoints são definidos pelo back-end:

### 📦 Produtos

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/Produto` | Retorna todos os produtos |
| GET | `/api/Produto/{id}` | Retorna um produto específico |
| GET | `/api/Produto/categoria/{categoriaId}` | Retorna produtos de uma categoria específica |
| GET | `/api/Produto/marca/{marcaId}` | Retorna produtos de uma marca específica |
| POST | `/api/Produto` | Cria um novo produto |
| PUT | `/api/Produto/{id}` | Atualiza um produto existente |
| DELETE | `/api/Produto/{id}` | Exclui um produto |

### 🏷️ Categorias

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/Categoria` | Retorna todas as categorias |
| GET | `/api/Categoria/{id}` | Retorna uma categoria específica |
| POST | `/api/Categoria` | Cria uma nova categoria |
| PUT | `/api/Categoria/{id}` | Atualiza uma categoria existente |
| DELETE | `/api/Categoria/{id}` | Exclui uma categoria |

### 🏢 Marcas

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/Marca` | Retorna todas as marcas |
| GET | `/api/Marca/{id}` | Retorna uma marca específica |
| POST | `/api/Marca` | Cria uma nova marca |
| PUT | `/api/Marca/{id}` | Atualiza uma marca existente |
| DELETE | `/api/Marca/{id}` | Exclui uma marca |

## 🚀 Tutorial de Instalação e Execução

Para configurar e executar o projeto KiraShop, siga os passos abaixo:

### ✅ Requisitos

Certifique-se de ter os seguintes softwares instalados:

1.  **[.NET SDK 8.0](https://dotnet.microsoft.com/download)** ou superior
2.  **[MySQL Server 8.0](https://dev.mysql.com/downloads/mysql/)** ou superior
3.  **[Node.js 16](https://nodejs.org/en/download/)** ou superior (inclui npm)
4.  **npm** (gerenciador de pacotes do Node.js) ou **yarn**

### 🛠️ Configuração do Back-end

1.  **Navegue até o diretório `backend`**:
    ```bash
    cd backend
    ```

2.  **Configure o Banco de Dados**: Abra `appsettings.json` e atualize a `DefaultConnection` com suas credenciais do MySQL:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "server=localhost;user id=root;password=SuaSenha;database=kira2db"
      }
    }
    ```
    Substitua `SuaSenha` pela senha do seu usuário `root` do MySQL.

3.  **Crie o Banco de Dados e Aplique Migrações**: No terminal, execute:
    ```bash
    dotnet ef database update
    ```
    (Se `dotnet ef` não for reconhecido, instale-o globalmente: `dotnet tool install --global dotnet-ef`)

4.  **Compile e Execute o Back-end**:
    ```bash
    dotnet build
    dotnet run
    ```
    O back-end estará acessível em `http://localhost:5000`.

### 🎨 Configuração do Front-end

1.  **Navegue até o diretório `frontend`**:
    ```bash
    cd frontend
    ```

2.  **Instale as Dependências**:
    ```bash
    npm install
    # ou yarn install
    ```

3.  **Configure a URL da API**: Crie um arquivo `.env` na raiz do diretório `frontend` com o seguinte conteúdo:
    ```
    VITE_API_URL=http://localhost:5000/api
    ```
    Ajuste a URL se o seu back-end estiver em um endereço diferente.

4.  **Compile e Execute o Front-end**:
    ```bash
    npm run dev
    # ou yarn dev
    ```
    O front-end estará acessível em `http://localhost:5173`.

### ➡️ Acesso à Aplicação

-   **Aplicação Principal**: Abra seu navegador e acesse `http://localhost:5173`.
-   **Painel Administrativo**: Acesse `http://localhost:5173/adminDashboard`.

## 🤝 Autores

Este projeto foi desenvolvido com a colaboração dos seguintes autores:

-   [Daniel Costa](https://github.com/userdanieldev)
-   [Felipe Garcia](https://github.com/GarciaFelipe-1973)
-   [Gustavo Henrique](https://github.com/GuVieir4)
-   [Joaquim Fernando](https://github.com/JoaquimMoreira808)
-   [José Vitor](https://github.com/ZezoDebug)
