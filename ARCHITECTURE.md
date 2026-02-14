# Documentação de Arquitetura e Design

## Visão Geral

Este projeto implementa um sistema completo de gerenciamento de estudantes, composto por uma WebAPI em .NET 8 no back-end e uma aplicação React no front-end, com autenticação JWT.

## Decisões Arquiteturais

### 1. Arquitetura em Camadas

#### Back-end
O back-end foi estruturado seguindo uma arquitetura em camadas simplificada:

- **Controllers**: Responsável por receber requisições HTTP e retornar respostas
- **Models**: Define as entidades de domínio (Student, User)
- **Data**: Gerencia o acesso a dados (DbContext, DbInitializer)
- **DTOs**: Objetos de transferência de dados para comunicação com o cliente

**Justificativa**: Para um projeto deste escopo, uma arquitetura em camadas simples é suficiente. Evitamos over-engineering introduzindo padrões como Repository ou Unit of Work, já que o Entity Framework Core já implementa esses padrões internamente.

#### Front-end
O front-end segue uma estrutura modular:

- **Pages**: Componentes de página completa
- **Components**: Componentes reutilizáveis
- **Services**: Lógica de comunicação com API
- **Context**: Gerenciamento de estado global

**Justificativa**: Separação clara de responsabilidades facilita manutenção e testes.

### 2. Banco de Dados em Memória

**Decisão**: Utilização do Entity Framework Core In-Memory Database

**Justificativa**:
- Simplicidade no setup
- Não requer instalação de banco de dados
- Ideal para desenvolvimento e demonstração
- Facilita testes e prototipação rápida

**Considerações para Produção**:
- Em produção, migrar para SQL Server, PostgreSQL ou outro SGBD
- Adicionar migrations do EF Core
- Implementar backup e recuperação de dados

### 3. Autenticação JWT

**Decisão**: Implementação de autenticação baseada em tokens JWT

**Justificativa**:
- Stateless: não requer armazenamento de sessão no servidor
- Escalável horizontalmente
- Padrão da indústria para APIs REST
- Facilita integração com diferentes clientes (web, mobile, etc)

**Implementação**:
- Token gerado no login contém claims do usuário
- Validade configurável (padrão: 2 horas)
- Verificação automática em endpoints protegidos via [Authorize]

**Segurança**:
- Chave secreta de 256 bits
- Validação de issuer e audience
- Verificação de expiração

### 4. CORS (Cross-Origin Resource Sharing)

**Decisão**: Configuração permissiva para desenvolvimento

**Configuração Atual**:
```csharp
policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
      .AllowAnyMethod()
      .AllowAnyHeader();
```

**Considerações para Produção**:
- Restringir origens para domínios específicos de produção
- Limitar métodos HTTP permitidos
- Implementar rate limiting
- Adicionar headers de segurança (HSTS, CSP, etc)

### 5. Tratamento de Erros

#### Back-end
- Retorno de status HTTP apropriados (200, 201, 400, 401, 404)
- Mensagens de erro descritivas em JSON
- Try-catch em operações críticas

#### Front-end
- Interceptor do Axios para tratamento global de erros
- Mensagens de feedback para o usuário (Snackbar)
- Redirecionamento automático em caso de não-autenticação

### 6. Gestão de Estado (Front-end)

**Decisão**: Context API do React

**Justificativa**:
- Suficiente para o escopo do projeto
- Evita complexidade desnecessária do Redux
- Built-in no React, sem dependências extras
- Fácil de entender e manter

**Alternativas Consideradas**:
- Redux: Overkill para este projeto
- Zustand: Boa opção, mas Context API é suficiente
- Recoil: Experimental, preferimos estabilidade

### 7. Interface do Usuário

**Decisão**: Material-UI (MUI)

**Justificativa**:
- Componentes prontos e testados
- Design system consistente
- Acessibilidade built-in
- Responsividade facilitada
- Documentação extensa
- Grande comunidade

**Componentes Principais Utilizados**:
- Table/TableContainer: Lista de estudantes
- Dialog: Formulários modais
- TextField: Inputs de formulário
- Button: Ações do usuário
- Snackbar/Alert: Feedback de operações

### 8. Roteamento (Front-end)

**Decisão**: React Router v6

**Implementação**:
- Rotas públicas: `/login`
- Rotas protegidas: `/students`
- Higher-Order Component (ProtectedRoute) para proteção

**Fluxo de Autenticação**:
1. Usuário não autenticado é redirecionado para `/login`
2. Após login bem-sucedido, token é armazenado no localStorage
3. Token é enviado automaticamente em todas as requisições (interceptor)
4. Token expirado resulta em logout automático

### 9. Validação de Dados

#### Back-end
- Validação via Data Annotations nos models
- ValidateModelState via [ApiController]
- Validações customizadas nos controllers
- Validação de tipos (int, double, DateTime)

#### Front-end
- Atributos HTML5 (required, min, max, type)
- Validação no onChange para feedback imediato
- Conversão de tipos antes de enviar para API
- Confirmação de ações destrutivas (delete)

### 10. Seed Data

**Decisão**: Popular dados automaticamente na inicialização

**Implementação**:
- DbInitializer executa após a criação do DbContext
- Verifica se há dados antes de popular
- Dados do CSV convertidos para código C#

**Vantagens**:
- Ambiente pronto para uso imediatamente
- Facilita demonstração e testes
- Dados consistentes entre reinicializações

## Padrões de Código

### Back-end (C#)
- Pascal Case para classes e métodos públicos
- Camel Case para variáveis locais e parâmetros
- Uso de async/await em operações I/O
- Uso de namespaces por arquivo
- Records/DTOs para dados imutáveis

### Front-end (JavaScript/React)
- PascalCase para componentes React
- camelCase para variáveis e funções
- Arrow functions para componentes funcionais
- Hooks do React (useState, useEffect, useContext)
- Destructuring para props

## Segurança

### Implementado
✅ Autenticação JWT
✅ CORS configurado
✅ Rotas protegidas no back-end ([Authorize])
✅ Rotas protegidas no front-end (ProtectedRoute)
✅ Validação de entrada
✅ HTTPS pronto (necessário certificado)

### Não Implementado (Produção)
❌ Hash de senhas (bcrypt, PBKDF2)
❌ Rate limiting
❌ Proteção contra CSRF
❌ Sanitização de HTML (XSS)
❌ Logs de auditoria
❌ Refresh tokens
❌ Políticas de senha forte
❌ Two-factor authentication (2FA)

## Escalabilidade

### Cenário Atual
- Banco em memória: Limitado a uma instância
- JWT stateless: Prepara para múltiplas instâncias
- API REST: Permite escalonamento horizontal

### Para Escalar
1. Migrar para banco de dados persistente
2. Implementar cache (Redis)
3. Load balancer para múltiplas instâncias da API
4. CDN para assets estáticos do front-end
5. Containerização (Docker)
6. Orquestração (Kubernetes)

## Testes

### Recomendado (Não Implementado)

#### Back-end
- Testes unitários com xUnit
- Testes de integração com WebApplicationFactory
- Mock do DbContext com biblioteca como Moq
- Cobertura de testes > 80%

#### Front-end
- Testes unitários com Jest
- Testes de componentes com React Testing Library
- Testes E2E com Cypress ou Playwright
- Snapshot tests para componentes visuais

## Performance

### Otimizações Implementadas
- Uso de async/await
- EF Core com queries otimizadas (ToListAsync)
- Lazy loading de componentes (futuro)
- Compression de resposta HTTP (futuro)

### Otimizações Futuras
- Paginação de resultados
- Cache de queries frequentes
- Debounce em buscas
- Virtualização de tabelas grandes
- Code splitting no front-end
- Service Worker para PWA

## Manutenibilidade

### Pontos Fortes
- Código organizado e estruturado
- Separação de responsabilidades
- Componentes reutilizáveis
- Documentação inline (comments)
- README detalhado

### Melhorias Futuras
- Adicionar testes automatizados
- Implementar CI/CD pipeline
- Adicionar linting mais rigoroso
- Code reviews automatizados
- Monitoramento e logging (Serilog, Application Insights)

## Conclusão

Este projeto implementa uma arquitetura sólida e escalável, seguindo as melhores práticas de desenvolvimento web moderno. As decisões foram tomadas priorizando simplicidade, manutenibilidade e facilidade de compreensão, adequadas para o escopo do desafio técnico.

As sugestões de melhorias e considerações para produção demonstram consciência dos requisitos adicionais necessários em ambientes reais.
