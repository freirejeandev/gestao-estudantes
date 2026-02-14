# Sistema de Gerenciamento de Estudantes

Sistema completo para gerenciamento de estudantes com autenticação JWT, desenvolvido com .NET 8 (back-end) e React (front-end).

## � Documentação Completa

Para entender melhor as decisões arquiteturais e de design deste projeto, consulte:

- **[📐 DESIGN_DECISIONS.md](./DESIGN_DECISIONS.md)** - Decisões detalhadas de arquitetura e design com justificativas e trade-offs
- **[🏗️ ARCHITECTURE.md](./ARCHITECTURE.md)** - Visão geral da arquitetura, padrões de código e considerações de segurança
- **[🚀 DEPLOY.md](./DEPLOY.md)** - Guia de deploy e configuração para produção
- **[🧪 TESTING.md](./TESTING.md)** - Estratégias de teste e cobertura
- **[📁 PROJECT_STRUCTURE.md](./PROJECT_STRUCTURE.md)** - Estrutura detalhada do projeto

## �📋 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (versão 18 ou superior)
- npm ou yarn

## 🚀 Como Executar o Projeto

### Back-end (API)

1. Navegue até a pasta da API:
```bash
cd StudentManagement.API
```

2. Restaure as dependências:
```bash
dotnet restore
```

3. Execute a aplicação:
```bash
dotnet run
```

A API estará disponível em: `http://localhost:5000`

A documentação Swagger estará disponível em: `http://localhost:5000/swagger`

### Front-end (React)

1. Navegue até a pasta do front-end:
```bash
cd student-management-ui
```

2. Instale as dependências:
```bash
npm install
```

3. Execute a aplicação:
```bash
npm run dev
```

A aplicação estará disponível em: `http://localhost:3000`

## 🔐 Credenciais de Acesso

O sistema vem com dois usuários padrão para teste:

- **Admin**: 
  - Usuário: `admin`
  - Senha: `admin123`

- **Usuário Comum**:
  - Usuário: `user`
  - Senha: `user123`

## 📚 Estrutura do Projeto

### Back-end (StudentManagement.API)

```
StudentManagement.API/
├── Controllers/
│   ├── AuthController.cs      # Endpoint de autenticação
│   └── StudentsController.cs  # Endpoints CRUD de estudantes
├── Data/
│   ├── AppDbContext.cs        # Contexto do Entity Framework
│   └── DbInitializer.cs       # Seed de dados inicial
├── DTOs/
│   ├── LoginDto.cs            # DTO para requisição de login
│   └── LoginResponseDto.cs    # DTO para resposta de login
├── Models/
│   ├── Student.cs             # Modelo de estudante
│   └── User.cs                # Modelo de usuário
├── Program.cs                 # Configuração da aplicação
└── appsettings.json           # Configurações (JWT, etc)
```

### Front-end (student-management-ui)

```
student-management-ui/
├── src/
│   ├── components/
│   │   ├── ProtectedRoute.jsx # Componente de rota protegida
│   │   └── StudentForm.jsx    # Formulário de estudante
│   ├── context/
│   │   └── AuthContext.jsx    # Context de autenticação
│   ├── pages/
│   │   ├── Login.jsx          # Página de login
│   │   └── StudentList.jsx    # Página de listagem de estudantes
│   ├── services/
│   │   └── api.js             # Configuração do Axios e serviços
│   ├── App.jsx                # Componente principal
│   └── main.jsx               # Ponto de entrada
└── package.json
```

## 🛠️ Tecnologias Utilizadas

### Back-end

- **.NET 8**: Framework principal
- **Entity Framework Core**: ORM e banco de dados em memória
- **JWT Bearer Authentication**: Autenticação via tokens JWT
- **Swagger/OpenAPI**: Documentação interativa da API

### Front-end

- **React 18**: Biblioteca JavaScript para construção de interfaces
- **Material-UI (MUI)**: Biblioteca de componentes UI
- **Axios**: Cliente HTTP para comunicação com a API
- **React Router**: Roteamento
- **Vite**: Build tool e dev server

## 📡 Endpoints da API

### Autenticação

- `POST /api/auth/login` - Autentica usuário e retorna token JWT

### Estudantes (Requer autenticação)

- `GET /api/students` - Lista todos os estudantes
- `GET /api/students/{id}` - Busca um estudante por ID
- `POST /api/students` - Cria um novo estudante
- `PUT /api/students/{id}` - Atualiza um estudante existente
- `DELETE /api/students/{id}` - Remove um estudante

## 🎯 Funcionalidades

### Sistema de Autenticação
- Login com usuário e senha
- Token JWT com tempo de expiração configurável
- Proteção de rotas no front-end e back-end
- Logout com limpeza de sessão

### Gerenciamento de Estudantes
- **Listagem**: Visualização de todos os estudantes em uma tabela responsiva
- **Criação**: Formulário completo para adicionar novos estudantes
- **Edição**: Atualização de informações de estudantes existentes
- **Exclusão**: Remoção de estudantes com confirmação

### Dados dos Estudantes
- Nome
- Idade
- Série
- Nota Média
- Endereço
- Nome do Pai
- Nome da Mãe
- Data de Nascimento

### UI/UX
- Interface moderna e responsiva usando Material-UI
- Mensagens de sucesso e erro para feedback do usuário
- Confirmação de exclusão para evitar erros
- Loading states durante requisições
- Formulários com validação

## 🗃️ Dados Iniciais

O sistema vem pré-populado com 52 estudantes de exemplo conforme o CSV fornecido.

## 🔧 Configurações Importantes

### JWT (appsettings.json)

```json
{
  "Jwt": {
    "Key": "ChaveSecretaSuperSegura123456789012345678901234567890",
    "Issuer": "StudentManagementAPI",
    "Audience": "StudentManagementClient",
    "ExpiresInMinutes": 120
  }
}
```

### CORS

A API está configurada para aceitar requisições das seguintes origens:
- `http://localhost:3000` (Vite)
- `http://localhost:5173` (Vite alternativo)

## 🏗️ Decisões de Arquitetura

Este projeto foi desenvolvido com atenção especial às decisões de arquitetura e design. Algumas decisões principais:

### Backend
- **Arquitetura em Camadas**: Separação clara entre Controllers, Models, Data e DTOs
- **Entity Framework Core In-Memory**: Simples setup, ideal para demonstração
- **JWT Stateless Authentication**: Escalável e padrão da indústria
- **Async/Await**: Todas operações I/O são assíncronas para melhor performance
- **API RESTful**: Seguindo princípios REST com recursos bem definidos

### Frontend
- **Single Page Application (SPA)**: React para experiência fluida
- **Material Design (MUI)**: UI moderna, responsiva e acessível
- **Context API**: Gerenciamento de estado simples e efetivo
- **Axios com Interceptors**: Token JWT automático em todas requisições
- **Protected Routes**: Segurança no frontend com rotas protegidas

### Justificativas e Trade-offs

Para entender o **porquê** de cada decisão, os **trade-offs** considerados e **alternativas avaliadas**, consulte:

👉 **[DESIGN_DECISIONS.md](./DESIGN_DECISIONS.md)** - Documento detalhado com todas as decisões arquiteturais

Tópicos abordados:
- ✅ Decisões Arquiteturais (Cliente-Servidor, API REST, JWT, etc)
- 🎨 Decisões de Design (SPA, Material-UI, Modais, Feedback Visual)
- 💻 Decisões Técnicas (EF Core, Async/Await, Axios, Vite)
- ⚖️ Trade-offs e Justificativas (comparações detalhadas)
- 📚 Lições Aprendidas (Do's e Don'ts)

## 📝 Observações

- O banco de dados é em memória, portanto os dados são perdidos quando a aplicação é reiniciada.
- As senhas são armazenadas em texto plano apenas para fins de demonstração. Em produção, use hashing adequado (bcrypt, etc).
- O token JWT tem expiração configurável (padrão: 2 horas).

## 🧪 Testando a Aplicação

1. Inicie o back-end primeiro
2. Inicie o front-end
3. Acesse `http://localhost:3000`
4. Faça login com as credenciais fornecidas
5. Teste as operações CRUD de estudantes

