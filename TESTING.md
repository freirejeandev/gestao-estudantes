# Guia de Testes da API

Este documento fornece exemplos de como testar a API usando diferentes ferramentas.

## Testando com Swagger (Recomendado para Iniciantes)

1. Inicie a API:
```bash
cd StudentManagement.API
dotnet run
```

2. Acesse: http://localhost:5000/swagger

3. Primeiro, faça login:
   - Expanda `POST /api/auth/login`
   - Clique em "Try it out"
   - Use o corpo:
   ```json
   {
     "username": "admin",
     "password": "admin123"
   }
   ```
   - Clique em "Execute"
   - Copie o token da resposta

4. Autorize o Swagger:
   - Clique no botão "Authorize" (cadeado no topo)
   - Digite: `Bearer {seu-token-aqui}`
   - Clique em "Authorize"

5. Agora você pode testar todos os endpoints protegidos!

## Testando com cURL

### Login
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"admin123"}'
```

### Listar Todos os Estudantes
```bash
curl -X GET http://localhost:5000/api/students \
  -H "Authorization: Bearer {seu-token}"
```

### Buscar Estudante por ID
```bash
curl -X GET http://localhost:5000/api/students/1 \
  -H "Authorization: Bearer {seu-token}"
```

### Criar Novo Estudante
```bash
curl -X POST http://localhost:5000/api/students \
  -H "Authorization: Bearer {seu-token}" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "idade": 10,
    "serie": 5,
    "notaMedia": 8.5,
    "endereco": "Rua Teste, 123",
    "nomePai": "José Silva",
    "nomeMae": "Maria Silva",
    "dataNascimento": "2014-03-15T00:00:00Z"
  }'
```

### Atualizar Estudante
```bash
curl -X PUT http://localhost:5000/api/students/1 \
  -H "Authorization: Bearer {seu-token}" \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "nome": "Alice Atualizada",
    "idade": 11,
    "serie": 6,
    "notaMedia": 9.0,
    "endereco": "123 Main St",
    "nomePai": "John Doe",
    "nomeMae": "Jane Doe",
    "dataNascimento": "2013-05-15T00:00:00Z"
  }'
```

### Deletar Estudante
```bash
curl -X DELETE http://localhost:5000/api/students/53 \
  -H "Authorization: Bearer {seu-token}"
```

## Testando com Postman

### Configuração

1. Crie uma nova Collection "Student Management API"

2. Adicione variáveis de ambiente:
   - `baseUrl`: http://localhost:5000/api
   - `token`: (será preenchido automaticamente)

### Endpoint: Login

- **Method**: POST
- **URL**: `{{baseUrl}}/auth/login`
- **Body** (raw JSON):
```json
{
  "username": "admin",
  "password": "admin123"
}
```
- **Tests** (para salvar o token automaticamente):
```javascript
if (pm.response.code === 200) {
    var jsonData = pm.response.json();
    pm.environment.set("token", jsonData.token);
}
```

### Endpoint: Get All Students

- **Method**: GET
- **URL**: `{{baseUrl}}/students`
- **Headers**:
  - Key: `Authorization`
  - Value: `Bearer {{token}}`

### Endpoint: Create Student

- **Method**: POST
- **URL**: `{{baseUrl}}/students`
- **Headers**:
  - Key: `Authorization`
  - Value: `Bearer {{token}}`
  - Key: `Content-Type`
  - Value: `application/json`
- **Body** (raw JSON):
```json
{
  "nome": "Teste Postman",
  "idade": 10,
  "serie": 5,
  "notaMedia": 8.0,
  "endereco": "Rua Postman, 456",
  "nomePai": "Pai Teste",
  "nomeMae": "Mãe Teste",
  "dataNascimento": "2014-01-01T00:00:00Z"
}
```

## Testando com PowerShell

### Login
```powershell
$response = Invoke-RestMethod -Uri "http://localhost:5000/api/auth/login" `
  -Method Post `
  -ContentType "application/json" `
  -Body '{"username":"admin","password":"admin123"}'

$token = $response.token
Write-Host "Token: $token"
```

### Listar Estudantes
```powershell
$headers = @{
    "Authorization" = "Bearer $token"
}

$students = Invoke-RestMethod -Uri "http://localhost:5000/api/students" `
  -Method Get `
  -Headers $headers

$students | Format-Table
```

### Criar Estudante
```powershell
$newStudent = @{
    nome = "João PowerShell"
    idade = 10
    serie = 5
    notaMedia = 8.5
    endereco = "Rua PS, 789"
    nomePai = "Pai PS"
    nomeMae = "Mãe PS"
    dataNascimento = "2014-05-15T00:00:00Z"
} | ConvertTo-Json

$result = Invoke-RestMethod -Uri "http://localhost:5000/api/students" `
  -Method Post `
  -Headers $headers `
  -ContentType "application/json" `
  -Body $newStudent

$result
```

## Cenários de Teste

### 1. Teste de Autenticação

✅ **Login com credenciais válidas**
- Username: admin, Password: admin123
- Deve retornar status 200 e um token

❌ **Login com credenciais inválidas**
- Username: wrong, Password: wrong
- Deve retornar status 401

❌ **Login sem campos obrigatórios**
- Body vazio
- Deve retornar status 400

### 2. Teste de Autorização

❌ **Acessar endpoint sem token**
- GET /api/students sem header Authorization
- Deve retornar status 401

❌ **Acessar endpoint com token inválido**
- Token modificado ou expirado
- Deve retornar status 401

### 3. Teste CRUD de Estudantes

✅ **Listar todos os estudantes**
- Deve retornar 52 estudantes inicialmente
- Status 200

✅ **Buscar estudante específico**
- GET /api/students/1
- Deve retornar Alice
- Status 200

❌ **Buscar estudante inexistente**
- GET /api/students/9999
- Deve retornar status 404

✅ **Criar novo estudante**
- POST com dados válidos
- Deve retornar status 201 e o estudante criado
- ID deve ser gerado automaticamente

❌ **Criar estudante com dados inválidos**
- Campos obrigatórios vazios
- Deve retornar status 400

✅ **Atualizar estudante existente**
- PUT /api/students/1 com dados válidos
- Deve retornar status 204

❌ **Atualizar estudante com ID incompatível**
- PUT /api/students/1 com body.id = 2
- Deve retornar status 400

❌ **Atualizar estudante inexistente**
- PUT /api/students/9999
- Deve retornar status 404

✅ **Deletar estudante**
- DELETE /api/students/1
- Deve retornar status 204
- GET /api/students/1 deve retornar 404

❌ **Deletar estudante inexistente**
- DELETE /api/students/9999
- Deve retornar status 404

## Validações Esperadas

### Campos Obrigatórios
- nome
- idade (1-100)
- serie (1-12)
- notaMedia (0-10)
- endereco
- nomePai
- nomeMae
- dataNascimento

### Formatos
- `dataNascimento`: ISO 8601 (YYYY-MM-DDTHH:mm:ssZ)
- `notaMedia`: número decimal
- `idade`, `serie`: números inteiros

## Respostas de Erro Comuns

### 400 Bad Request
```json
{
  "message": "Erro de validação",
  "errors": { ... }
}
```

### 401 Unauthorized
```json
{
  "message": "Usuário ou senha inválidos"
}
```

### 404 Not Found
```json
{
  "message": "Estudante com ID X não encontrado"
}
```

## Checklist de Testes

- [ ] Login com admin funciona
- [ ] Login com user funciona
- [ ] Login com credenciais erradas falha
- [ ] Listar estudantes retorna 52 registros
- [ ] Buscar estudante por ID funciona
- [ ] Criar novo estudante funciona
- [ ] Atualizar estudante funciona
- [ ] Deletar estudante funciona
- [ ] Acessar endpoints sem autenticação falha
- [ ] Token expira após tempo configurado
