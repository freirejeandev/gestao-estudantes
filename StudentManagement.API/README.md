# Student Management API

API REST para gerenciamento de estudantes com autenticação JWT desenvolvida em .NET 8.

## Características

- ✅ Entity Framework Core com banco de dados em memória
- ✅ Autenticação JWT
- ✅ Documentação Swagger/OpenAPI
- ✅ CRUD completo de estudantes
- ✅ Seed data automático
- ✅ CORS configurado

## Executar o Projeto

```bash
dotnet restore
dotnet run
```

Acesse a documentação: http://localhost:5000/swagger

## Endpoints

### POST /api/auth/login
Autentica um usuário e retorna um token JWT.

**Request:**
```json
{
  "username": "admin",
  "password": "admin123"
}
```

**Response:**
```json
{
  "token": "eyJhbGc...",
  "username": "admin",
  "expiresAt": "2026-02-14T12:00:00Z"
}
```

### GET /api/students
Retorna todos os estudantes (requer autenticação).

**Headers:**
```
Authorization: Bearer {token}
```

### GET /api/students/{id}
Retorna um estudante específico (requer autenticação).

### POST /api/students
Cria um novo estudante (requer autenticação).

**Request:**
```json
{
  "nome": "João Silva",
  "idade": 10,
  "serie": 5,
  "notaMedia": 8.5,
  "endereco": "Rua Exemplo, 123",
  "nomePai": "José Silva",
  "nomeMae": "Maria Silva",
  "dataNascimento": "2014-03-15T00:00:00Z"
}
```

### PUT /api/students/{id}
Atualiza um estudante existente (requer autenticação).

### DELETE /api/students/{id}
Remove um estudante (requer autenticação).

## Estrutura de Dados

### Student
```csharp
{
  "id": int,
  "nome": string,
  "idade": int,
  "serie": int,
  "notaMedia": double,
  "endereco": string,
  "nomePai": string,
  "nomeMae": string,
  "dataNascimento": DateTime
}
```

### User
```csharp
{
  "id": int,
  "username": string,
  "password": string
}
```

## Usuários Padrão

- **admin** / admin123
- **user** / user123

## Configuração JWT

Edite `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "sua-chave-secreta-aqui",
    "Issuer": "StudentManagementAPI",
    "Audience": "StudentManagementClient",
    "ExpiresInMinutes": 120
  }
}
```

## Pacotes NuGet

- Microsoft.AspNetCore.Authentication.JwtBearer 8.0.0
- Microsoft.EntityFrameworkCore.InMemory 8.0.0
- Swashbuckle.AspNetCore 6.5.0
- System.IdentityModel.Tokens.Jwt 7.0.0
