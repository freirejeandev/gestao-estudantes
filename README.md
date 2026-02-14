# Sistema de Gerenciamento de Estudantes

Sistema completo para gerenciamento de estudantes com autenticação JWT, desenvolvido com .NET 8 (back-end) e React (front-end).

## 📋 Pré-requisitos

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

### Back-end

1. **Entity Framework Core In-Memory Database**: Escolhido para simplificar o setup e permitir testes rápidos sem necessidade de um banco de dados externo.

2. **JWT Authentication**: Implementado para fornecer uma autenticação stateless e escalável.

3. **Repository Pattern através do DbContext**: O EF Core já implementa o padrão Repository, então não há necessidade de uma camada adicional neste projeto.

4. **DTOs**: Utilizados para separar os modelos de domínio das requisições/respostas da API.

### Front-end

1. **Context API**: Escolhida para gerenciamento de estado de autenticação, sendo mais simples que Redux para este escopo.

2. **Axios com Interceptors**: Facilita a adição automática de tokens JWT e tratamento de erros de autenticação.

3. **Material-UI**: Proporciona componentes prontos e consistentes, acelerando o desenvolvimento.

4. **Vite**: Build tool moderna e rápida, substituindo o Create React App.

5. **Protected Routes**: Implementadas para garantir que apenas usuários autenticados acessem as funcionalidades principais.

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

## 📄 Licença

Este projeto foi desenvolvido como parte de um desafio técnico.
