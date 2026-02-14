# Estrutura Completa do Projeto

```
1_INTRO/
│
├── StudentManagement.API/              # Back-end .NET 8
│   ├── Controllers/
│   │   ├── AuthController.cs          # Autenticação JWT
│   │   └── StudentsController.cs      # CRUD de estudantes
│   │
│   ├── Data/
│   │   ├── AppDbContext.cs            # Entity Framework DbContext
│   │   └── DbInitializer.cs           # Seed de dados
│   │
│   ├── DTOs/
│   │   ├── LoginDto.cs                # Request de login
│   │   └── LoginResponseDto.cs        # Response de login
│   │
│   ├── Models/
│   │   ├── Student.cs                 # Entidade Estudante
│   │   └── User.cs                    # Entidade Usuário
│   │
│   ├── Properties/
│   │   └── launchSettings.json        # Configurações de launch
│   │
│   ├── Program.cs                     # Entry point e configuração
│   ├── appsettings.json               # Configurações da aplicação
│   ├── appsettings.Development.json   # Configurações de dev
│   ├── StudentManagement.API.csproj   # Arquivo do projeto
│   └── README.md                      # Documentação da API
│
├── student-management-ui/              # Front-end React
│   ├── src/
│   │   ├── components/
│   │   │   ├── ProtectedRoute.jsx     # HOC de proteção de rotas
│   │   │   └── StudentForm.jsx        # Formulário de estudante
│   │   │
│   │   ├── context/
│   │   │   └── AuthContext.jsx        # Context de autenticação
│   │   │
│   │   ├── pages/
│   │   │   ├── Login.jsx              # Página de login
│   │   │   └── StudentList.jsx        # Página de lista
│   │   │
│   │   ├── services/
│   │   │   └── api.js                 # Axios config e serviços
│   │   │
│   │   ├── App.jsx                    # Componente raiz
│   │   └── main.jsx                   # Entry point
│   │
│   ├── index.html                     # HTML principal
│   ├── package.json                   # Dependências NPM
│   ├── vite.config.js                 # Configuração Vite
│   └── README.md                      # Documentação do UI
│
├── img/                                # Pasta de imagens (original)
│
├── index.html                          # Arquivo original
│
├── students_data.csv                   # Dados originais CSV
│
├── .gitignore                          # Arquivos ignorados pelo Git
├── README.md                           # Documentação principal
├── ARCHITECTURE.md                     # Decisões de arquitetura
├── TESTING.md                          # Guia de testes
└── DEPLOY.md                           # Guia de deploy

```

## Fluxo de Dados

```
┌─────────────────────────────────────────────────────────────────┐
│                         USUÁRIO                                 │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
                    ┌──────────────────┐
                    │   Browser        │
                    │   localhost:3000 │
                    └──────────────────┘
                              │
        ┌─────────────────────┼─────────────────────┐
        │                     │                     │
        ▼                     ▼                     ▼
   ┌─────────┐         ┌──────────┐         ┌──────────┐
   │ Login   │         │ Student  │         │ Student  │
   │ Page    │         │ List     │         │ Form     │
   └─────────┘         └──────────┘         └──────────┘
        │                     │                     │
        └─────────────────────┼─────────────────────┘
                              │
                        ┌─────▼──────┐
                        │ AuthContext│
                        │ (Context   │
                        │  API)      │
                        └─────┬──────┘
                              │
                        ┌─────▼──────┐
                        │ api.js     │
                        │ (Axios +   │
                        │ interceptor)│
                        └─────┬──────┘
                              │
                    HTTP/JSON │ JWT Bearer Token
                              │
                              ▼
                    ┌──────────────────┐
                    │   WebAPI         │
                    │   localhost:5000 │
                    └──────────────────┘
                              │
        ┌─────────────────────┼─────────────────────┐
        │                     │                     │
        ▼                     ▼                     ▼
   ┌──────────┐       ┌──────────────┐      ┌──────────┐
   │ Auth     │       │ Students     │      │ Swagger  │
   │Controller│       │ Controller   │      │ UI       │
   └──────────┘       └──────────────┘      └──────────┘
        │                     │
        │     ┌───────────────┘
        │     │
        ▼     ▼
   ┌─────────────────┐
   │  AppDbContext   │
   │  (EF Core)      │
   └─────────────────┘
           │
           ▼
   ┌─────────────────┐
   │  In-Memory DB   │
   │  ┌──────────┐   │
   │  │ Students │   │
   │  │ Users    │   │
   │  └──────────┘   │
   └─────────────────┘
```

## Fluxo de Autenticação

```
1. Login
   ┌──────┐           ┌─────┐           ┌────────────┐
   │ User │──login──→ │ UI  │──POST────→│ /auth/login│
   └──────┘           └─────┘           └────────────┘
                         ↑                     │
                         │                     ▼
                         │             ┌──────────────┐
                         │             │ Validate user│
                         │             │ in database  │
                         │             └──────────────┘
                         │                     │
                         │                     ▼
                         │             ┌──────────────┐
                         │             │ Generate JWT │
                         │             │ Token        │
                         │             └──────────────┘
                         │                     │
                         └─────token───────────┘
                                 │
                                 ▼
                         ┌──────────────┐
                         │ localStorage │
                         └──────────────┘

2. Request com Autenticação
   ┌─────┐                              ┌─────────────┐
   │ UI  │──GET /api/students ─────────→│ Interceptor │
   └─────┘                              └─────────────┘
                                               │
                                               ▼
                                  ┌────────────────────────┐
                                  │ Add Authorization:     │
                                  │ Bearer {token}         │
                                  └────────────────────────┘
                                               │
                                               ▼
                                       ┌──────────────┐
                                       │ API validates│
                                       │ JWT token    │
                                       └──────────────┘
                                               │
                      ┌────────────────────────┼────────────────────┐
                      │                        │                    │
                      ▼                        ▼                    ▼
              ┌──────────────┐       ┌──────────────┐     ┌──────────────┐
              │ Token Valid  │       │ Token Invalid│     │ Token Expired│
              │ Return Data  │       │ Return 401   │     │ Return 401   │
              └──────────────┘       └──────────────┘     └──────────────┘
```

## Tecnologias por Camada

### Back-end (.NET 8)
```
┌─────────────────────────────────────────┐
│ Presentation Layer                      │
│ • ASP.NET Core Controllers              │
│ • Swagger/OpenAPI                       │
│ • JWT Authentication Middleware         │
└─────────────────────────────────────────┘
                  │
┌─────────────────────────────────────────┐
│ Business Logic Layer                    │
│ • DTOs (Data Transfer Objects)          │
│ • Model Validation                      │
└─────────────────────────────────────────┘
                  │
┌─────────────────────────────────────────┐
│ Data Access Layer                       │
│ • Entity Framework Core                 │
│ • DbContext                             │
│ • Repository Pattern (via EF)           │
└─────────────────────────────────────────┘
                  │
┌─────────────────────────────────────────┐
│ Database                                │
│ • In-Memory Database (Development)      │
│ • SQL Server (Production - futuro)      │
└─────────────────────────────────────────┘
```

### Front-end (React)
```
┌─────────────────────────────────────────┐
│ View Layer                              │
│ • React Components (JSX)                │
│ • Material-UI Components                │
│ • CSS-in-JS (Emotion)                   │
└─────────────────────────────────────────┘
                  │
┌─────────────────────────────────────────┐
│ State Management                        │
│ • Context API                           │
│ • useState / useEffect Hooks            │
└─────────────────────────────────────────┘
                  │
┌─────────────────────────────────────────┐
│ Routing                                 │
│ • React Router v6                       │
│ • Protected Routes                      │
└─────────────────────────────────────────┘
                  │
┌─────────────────────────────────────────┐
│ Data Layer                              │
│ • Axios HTTP Client                     │
│ • Request/Response Interceptors         │
│ • API Service Layer                     │
└─────────────────────────────────────────┘
```

## Endpoints da API

```
Authentication
├── POST   /api/auth/login       # Autenticar e obter token

Students (Requer autenticação)
├── GET    /api/students          # Listar todos
├── GET    /api/students/{id}     # Buscar por ID
├── POST   /api/students          # Criar novo
├── PUT    /api/students/{id}     # Atualizar existente
└── DELETE /api/students/{id}     # Deletar

Documentation
└── GET    /swagger               # Documentação interativa
```

## Páginas do Front-end

```
Public Routes
└── /login                        # Página de login

Protected Routes (Requer autenticação)
└── /students                     # Lista de estudantes
    ├── [Modal] Adicionar estudante
    ├── [Modal] Editar estudante
    └── [Dialog] Confirmar exclusão
```

## Resumo de Arquivos por Tipo

### Back-end
- **Controllers**: 2 arquivos (Auth, Students)
- **Models**: 2 arquivos (Student, User)
- **DTOs**: 2 arquivos (LoginDto, LoginResponseDto)
- **Data**: 2 arquivos (AppDbContext, DbInitializer)
- **Configuration**: 3 arquivos (.csproj, appsettings, launchSettings)
- **Total**: ~11 arquivos principais

### Front-end
- **Pages**: 2 componentes (Login, StudentList)
- **Components**: 2 componentes (ProtectedRoute, StudentForm)
- **Context**: 1 arquivo (AuthContext)
- **Services**: 1 arquivo (api.js)
- **Core**: 2 arquivos (App.jsx, main.jsx)
- **Configuration**: 3 arquivos (package.json, vite.config, index.html)
- **Total**: ~11 arquivos principais

### Documentação
- **README.md**: Documentação geral
- **ARCHITECTURE.md**: Decisões de arquitetura
- **TESTING.md**: Guia de testes
- **DEPLOY.md**: Guia de deploy
- **API/README.md**: Documentação da API
- **UI/README.md**: Documentação do front-end
- **Total**: 6 arquivos de documentação

## Comandos Rápidos

### Desenvolvimento
```bash
# Back-end
cd StudentManagement.API && dotnet run

# Front-end (novo terminal)
cd student-management-ui && npm run dev
```

### Build
```bash
# Back-end
cd StudentManagement.API && dotnet publish -c Release

# Front-end
cd student-management-ui && npm run build
```

### Docker
```bash
# Executar tudo com Docker Compose
docker-compose up -d
```

## Próximos Passos Sugeridos

1. ✅ Implementar paginação na listagem
2. ✅ Adicionar filtros e busca
3. ✅ Implementar testes unitários
4. ✅ Adicionar logging estruturado
5. ✅ Migrar para banco de dados persistente
6. ✅ Implementar refresh tokens
7. ✅ Adicionar hashing de senhas
8. ✅ Implementar rate limiting
9. ✅ Adicionar CI/CD pipeline
10. ✅ Deploy em cloud (Azure/AWS)
