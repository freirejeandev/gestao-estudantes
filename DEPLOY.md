# Guia de Deploy

Este documento fornece instruções para fazer o deploy da aplicação em diferentes ambientes.

## Deploy Local (Desenvolvimento)

### Back-end
```bash
cd StudentManagement.API
dotnet restore
dotnet run
```
API disponível em: http://localhost:5000

### Front-end
```bash
cd student-management-ui
npm install
npm run dev
```
UI disponível em: http://localhost:3000

## Build de Produção

### Back-end

#### Build
```bash
cd StudentManagement.API
dotnet publish -c Release -o ./publish
```

#### Executar
```bash
cd publish
dotnet StudentManagement.API.dll
```

### Front-end

#### Build
```bash
cd student-management-ui
npm run build
```

Os arquivos estarão em `dist/` prontos para deploy em qualquer servidor web estático.

## Deploy com Docker

### Dockerfile - Back-end

Crie `StudentManagement.API/Dockerfile`:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["StudentManagement.API.csproj", "./"]
RUN dotnet restore "StudentManagement.API.csproj"
COPY . .
RUN dotnet build "StudentManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentManagement.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentManagement.API.dll"]
```

#### Build e Run
```bash
cd StudentManagement.API
docker build -t student-api .
docker run -d -p 5000:80 --name student-api student-api
```

### Dockerfile - Front-end

Crie `student-management-ui/Dockerfile`:

```dockerfile
FROM node:18-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

Crie `student-management-ui/nginx.conf`:

```nginx
server {
    listen 80;
    server_name localhost;
    root /usr/share/nginx/html;
    index index.html;

    location / {
        try_files $uri $uri/ /index.html;
    }

    location /api {
        proxy_pass http://student-api:80;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}
```

#### Build e Run
```bash
cd student-management-ui
docker build -t student-ui .
docker run -d -p 3000:80 --name student-ui student-ui
```

### Docker Compose

Crie `docker-compose.yml` na raiz do projeto:

```yaml
version: '3.8'

services:
  api:
    build:
      context: ./StudentManagement.API
      dockerfile: Dockerfile
    ports:
      - "5000:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - Jwt__Key=ChaveSecretaSuperSegura123456789012345678901234567890
    networks:
      - student-network

  ui:
    build:
      context: ./student-management-ui
      dockerfile: Dockerfile
    ports:
      - "3000:80"
    depends_on:
      - api
    networks:
      - student-network

networks:
  student-network:
    driver: bridge
```

#### Executar
```bash
docker-compose up -d
```

## Deploy no Azure

### App Service (API)

```bash
# Login
az login

# Criar Resource Group
az group create --name student-management-rg --location eastus

# Criar App Service Plan
az appservice plan create --name student-api-plan --resource-group student-management-rg --sku B1 --is-linux

# Criar Web App
az webapp create --resource-group student-management-rg --plan student-api-plan --name student-api-app --runtime "DOTNET|8.0"

# Deploy
cd StudentManagement.API
dotnet publish -c Release
cd bin/Release/net8.0/publish
zip -r deploy.zip .
az webapp deployment source config-zip --resource-group student-management-rg --name student-api-app --src deploy.zip

# Configurar variáveis
az webapp config appsettings set --resource-group student-management-rg --name student-api-app --settings Jwt__Key="ChaveSecretaSuperSegura123456789012345678901234567890"
```

### Static Web Apps (UI)

```bash
# Instalar CLI
npm install -g @azure/static-web-apps-cli

# Deploy
cd student-management-ui
npm run build
az staticwebapp create --name student-ui-app --resource-group student-management-rg --source dist/ --location eastus
```

## Deploy no AWS

### Elastic Beanstalk (API)

```bash
# Instalar EB CLI
pip install awsebcli

# Inicializar
cd StudentManagement.API
eb init -p docker student-api --region us-east-1

# Criar ambiente
eb create student-api-env

# Deploy
eb deploy

# Configurar variáveis
eb setenv Jwt__Key="ChaveSecretaSuperSegura123456789012345678901234567890"
```

### S3 + CloudFront (UI)

```bash
# Build
cd student-management-ui
npm run build

# Criar bucket S3
aws s3 mb s3://student-management-ui

# Upload
aws s3 sync dist/ s3://student-management-ui

# Configurar bucket para hospedagem web
aws s3 website s3://student-management-ui --index-document index.html

# Criar distribuição CloudFront (opcional, para HTTPS e CDN)
```

## Deploy no Heroku

### API

```bash
# Login
heroku login

# Criar app
heroku create student-api-heroku

# Deploy
cd StudentManagement.API
git init
heroku git:remote -a student-api-heroku
git add .
git commit -m "Initial commit"
git push heroku main

# Configurar variáveis
heroku config:set Jwt__Key="ChaveSecretaSuperSegura123456789012345678901234567890"
```

### UI

```bash
# Criar app
heroku create student-ui-heroku

# Deploy
cd student-management-ui
# Adicionar buildpack
heroku buildpacks:set heroku/nodejs
# Modificar package.json para incluir script "start": "vite preview"
git add .
git commit -m "Deploy to Heroku"
git push heroku main
```

## Considerações de Produção

### Segurança

1. **HTTPS**: Use sempre HTTPS em produção
   ```csharp
   // Program.cs
   app.UseHttpsRedirection();
   ```

2. **Variáveis de Ambiente**: Nunca commite chaves secretas
   ```bash
   # Use .env ou Azure Key Vault, AWS Secrets Manager
   export JWT_KEY="sua-chave-segura"
   ```

3. **CORS**: Restrinja origens permitidas
   ```csharp
   policy.WithOrigins("https://seu-dominio.com")
   ```

4. **Rate Limiting**: Implemente para evitar abuso
   ```bash
   dotnet add package AspNetCoreRateLimit
   ```

### Performance

1. **Compressão**: Habilite compressão de resposta
   ```csharp
   builder.Services.AddResponseCompression();
   ```

2. **Cache**: Implemente estratégia de cache
   ```csharp
   builder.Services.AddResponseCaching();
   ```

3. **CDN**: Use CDN para assets estáticos

### Monitoramento

1. **Logging**: Configure logging estruturado
   ```bash
   dotnet add package Serilog.AspNetCore
   ```

2. **Application Insights** (Azure):
   ```bash
   dotnet add package Microsoft.ApplicationInsights.AspNetCore
   ```

3. **Health Checks**:
   ```csharp
   builder.Services.AddHealthChecks();
   app.MapHealthChecks("/health");
   ```

### Banco de Dados

Para produção, migre para banco real:

1. **Instalar pacotes**:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   ```

2. **Configurar connection string**:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=...;Database=...;User Id=...;Password=..."
     }
   }
   ```

3. **Atualizar Program.cs**:
   ```csharp
   builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
   ```

4. **Criar migrations**:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## Checklist de Deploy

### Pré-Deploy
- [ ] Todos os testes passam
- [ ] Código revisado
- [ ] Variáveis de ambiente configuradas
- [ ] Secrets não estão no código
- [ ] CORS configurado corretamente
- [ ] SSL/TLS configurado

### Pós-Deploy
- [ ] Aplicação está acessível
- [ ] Login funciona
- [ ] Endpoints da API respondem
- [ ] Logs estão sendo gerados
- [ ] Monitoramento configurado
- [ ] Backup configurado (se aplicável)
- [ ] Documentação atualizada

## Troubleshooting

### API não inicia
- Verifique logs: `dotnet run --verbosity detailed`
- Verifique portas em uso
- Verifique variáveis de ambiente

### Erro 401 no front-end
- Verifique URL da API em `api.js`
- Verifique token no localStorage
- Verifique CORS na API

### Build falha
- Limpe o cache: `dotnet clean`
- Restaure dependências: `dotnet restore`
- Para React: `rm -rf node_modules && npm install`

## Suporte

Para problemas de deploy, consulte:
- Documentação do .NET: https://docs.microsoft.com/dotnet
- Documentação React: https://react.dev
- Documentação do provedor de cloud específico
