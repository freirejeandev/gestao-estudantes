# Student Management UI

Interface web moderna para gerenciamento de estudantes desenvolvida em React com Material-UI.

## Características

- ✅ React 18 com Vite
- ✅ Material-UI para componentes
- ✅ Autenticação JWT
- ✅ Rotas protegidas
- ✅ CRUD completo de estudantes
- ✅ Interface responsiva
- ✅ Mensagens de feedback

## Executar o Projeto

```bash
npm install
npm run dev
```

Acesse: http://localhost:3000

## Estrutura do Projeto

```
src/
├── components/
│   ├── ProtectedRoute.jsx    # HOC para rotas protegidas
│   └── StudentForm.jsx       # Formulário de estudante
├── context/
│   └── AuthContext.jsx       # Gerenciamento de autenticação
├── pages/
│   ├── Login.jsx             # Página de login
│   └── StudentList.jsx       # Lista de estudantes
├── services/
│   └── api.js                # Cliente HTTP e serviços
├── App.jsx                   # Rotas da aplicação
└── main.jsx                  # Entry point
```

## Funcionalidades

### Autenticação
- Login com usuário e senha
- Armazenamento seguro do token JWT
- Logout com limpeza de sessão
- Redirecionamento automático em caso de token expirado

### Gerenciamento de Estudantes
- Visualização em tabela responsiva
- Adicionar novo estudante
- Editar estudante existente
- Excluir com confirmação
- Filtros e ordenação (futuro)

## Componentes Principais

### AuthContext
Gerencia o estado de autenticação em toda a aplicação.

```jsx
const { user, login, logout, loading } = useAuth();
```

### ProtectedRoute
Protege rotas que requerem autenticação.

```jsx
<Route 
  path="/students" 
  element={
    <ProtectedRoute>
      <StudentList />
    </ProtectedRoute>
  } 
/>
```

### StudentForm
Formulário reutilizável para criar e editar estudantes.

```jsx
<StudentForm
  open={open}
  student={selectedStudent}
  onClose={handleClose}
  onSuccess={handleSuccess}
  onError={handleError}
/>
```

## Serviços API

### authService
```javascript
// Login
const response = await authService.login(username, password);

// Logout
authService.logout();

// Verificar autenticação
const isAuth = authService.isAuthenticated();

// Obter usuário
const username = authService.getUsername();
```

### studentService
```javascript
// Listar todos
const students = await studentService.getAllStudents();

// Buscar por ID
const student = await studentService.getStudentById(id);

// Criar
const newStudent = await studentService.createStudent(data);

// Atualizar
await studentService.updateStudent(id, data);

// Deletar
await studentService.deleteStudent(id);
```

## Configuração da API

A URL da API pode ser alterada em `src/services/api.js`:

```javascript
const API_BASE_URL = 'http://localhost:5000/api';
```

## Credenciais de Teste

- **Usuário**: admin | **Senha**: admin123
- **Usuário**: user | **Senha**: user123

## Scripts Disponíveis

- `npm run dev` - Inicia o servidor de desenvolvimento
- `npm run build` - Cria build de produção
- `npm run preview` - Preview do build de produção
- `npm run lint` - Executa o linter

## Tecnologias

- **React 18** - Biblioteca JavaScript
- **Vite** - Build tool
- **Material-UI** - Componentes UI
- **Axios** - Cliente HTTP
- **React Router** - Roteamento

## Melhorias Futuras

- [ ] Paginação na lista de estudantes
- [ ] Filtros e busca avançada
- [ ] Ordenação de colunas
- [ ] Exportação para CSV/Excel
- [ ] Dark mode
- [ ] Testes unitários e E2E
- [ ] PWA (Progressive Web App)
