# 🎉 ClimaNotificacoesAPI - Projeto Completo

## 📋 Visão Geral

Sistema completo de gerenciamento de notificações climáticas com:
- ✅ **Backend**: API REST em .NET 8 com Clean Architecture
- ✅ **Frontend**: SPA em Angular 17 com Bootstrap 5
- ✅ **Docker**: Orquestração completa com Docker Compose
- ✅ **Testes**: 38 testes unitários com 100% de sucesso
- ✅ **Documentação**: Guias completos de uso e desenvolvimento

## 🚀 Início Rápido (1 minuto)

```bash
# 1. Clone o repositório
git clone https://github.com/IgorAnthonyy/ClimaNotificacoesAPI.git
cd ClimaNotificacoesAPI

# 2. Configure as variáveis de ambiente
cp .env.example .env
# Edite .env com suas credenciais (OpenWeatherMap API Key obrigatória)

# 3. Execute tudo com Docker
docker-compose up

# 4. Acesse a aplicação
# Frontend: http://localhost:4200
# API: http://localhost:8080
# Swagger: http://localhost:8080/swagger
```

## 📦 Arquitetura do Sistema

```
┌─────────────────────────────────────────────────────────────┐
│                     DOCKER COMPOSE                          │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│  ┌──────────────┐    ┌──────────────┐    ┌─────────────┐  │
│  │   Frontend   │───→│  Backend API │───→│ SQL Server  │  │
│  │  Angular 17  │    │   .NET 8     │    │   2022      │  │
│  │  + Nginx     │    │ Clean Arch   │    │ + Volume    │  │
│  │  Port 4200   │    │  Port 8080   │    │ Port 1433   │  │
│  └──────────────┘    └──────────────┘    └─────────────┘  │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

## 🛠️ Stack Tecnológica

### Backend
- **.NET 8** - Framework
- **Entity Framework Core** - ORM
- **SQL Server 2022** - Banco de dados
- **JWT** - Autenticação
- **Mapster** - Object mapping
- **MailKit** - Envio de emails
- **OpenWeatherMap API** - Dados climáticos
- **xUnit + Moq + FluentAssertions** - Testes

### Frontend
- **Angular 17** - Framework SPA
- **TypeScript** - Linguagem
- **Bootstrap 5** - UI Framework
- **Bootstrap Icons** - Ícones
- **RxJS** - Programação reativa
- **Nginx** - Servidor web

### DevOps
- **Docker** - Containerização
- **Docker Compose** - Orquestração
- **Git** - Controle de versão

## 📱 Funcionalidades

### Backend API

#### Usuários
- ✅ Criar usuário
- ✅ Listar usuários
- ✅ Buscar por ID
- ✅ Atualizar usuário
- ✅ Excluir usuário
- ✅ Login com JWT
- ✅ Listar cidades do usuário

#### Cidades
- ✅ Adicionar cidade
- ✅ Listar cidades
- ✅ Buscar por ID
- ✅ Excluir cidade
- ✅ Listar previsões da cidade

#### Previsões do Tempo
- ✅ Buscar previsão atual
- ✅ Atualizar previsão
- ✅ Job automático (a cada 3 minutos)
- ✅ Envio de alertas por email

### Frontend Web

#### Dashboard
- ✅ Home page com cards
- ✅ Navegação rápida
- ✅ Design responsivo

#### Gerenciamento de Usuários
- ✅ Tabela de usuários
- ✅ Formulário de criação
- ✅ Edição inline
- ✅ Exclusão com confirmação
- ✅ Validação de campos

#### Gerenciamento de Cidades
- ✅ Lista de cidades
- ✅ Adicionar cidade
- ✅ Associar a usuário
- ✅ Buscar previsão
- ✅ Exibir dados climáticos
- ✅ Excluir cidade

## 📊 Estatísticas do Projeto

### Código
- **Linguagens**: C#, TypeScript, HTML, CSS
- **Linhas de Código**: ~15,000+
- **Arquivos**: 100+
- **Componentes Angular**: 4
- **Controllers .NET**: 3
- **Services**: 9
- **Repositories**: 3

### Testes
- **Total**: 38 testes unitários
- **Sucesso**: 100%
- **Cobertura**: Services, Helpers, Repositories

### Docker
- **Containers**: 3 (SQL, API, Frontend)
- **Networks**: 1 (clima-net)
- **Volumes**: 1 (sqlserver-data)

## 📁 Estrutura de Diretórios

```
ClimaNotificacoesAPI/
├── ClimaNotificacoesAPI.API/          # API Controllers
├── ClimaNotificacoesAPI.Application/   # Services, DTOs, Jobs
├── ClimaNotificacoesAPI.Domain/        # Entities, Interfaces
├── ClimaNotificacoesAPI.Infrastructure/# Repositories, Data
├── ClimaNotificacoesAPI.Tests/         # Unit Tests
├── frontend/                           # Angular Application
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/            # UI Components
│   │   │   ├── services/              # API Services
│   │   │   ├── models/                # TypeScript Models
│   │   │   └── app.routes.ts          # Routing
│   │   ├── environments/              # Configuration
│   │   └── styles.scss                # Global Styles
│   ├── Dockerfile                     # Frontend Docker
│   └── nginx.conf                     # Nginx Config
├── docker-compose.yml                 # Docker Orchestration
├── .env                               # Environment Variables
├── README.md                          # Main Documentation
├── QUICKSTART.md                      # Quick Start Guide
├── TESTING.md                         # Testing Guide
├── FRONTEND_GUIDE.md                  # Frontend User Guide
└── DELIVERABLES.md                    # Project Summary
```

## 🔐 Segurança

### Implementado
- ✅ JWT Authentication
- ✅ Senha hash (BCrypt)
- ✅ CORS configurado
- ✅ Validação de dados (Data Annotations)
- ✅ Secrets em variáveis de ambiente
- ✅ SQL Injection prevention (EF Core)
- ✅ HTTPS redirect

### Configurações de Segurança
```bash
# Variáveis obrigatórias no .env
PASSWORD_SQL=SenhaForte!123
JWT_SECRET_KEY=chave-minima-32-caracteres
EMAIL_USERNAME=seu@email.com
EMAIL_PASSWORD=suaSenhaDeApp
OPENWEATHER_API_KEY=sua-api-key
```

## 🧪 Testes Unitários

### Cobertura (38 testes)
```
✅ WeatherConditionHelperTests (7 testes)
✅ UsuarioServiceTests (8 testes)
✅ CidadeServiceTests (8 testes)
✅ TokenServiceTests (4 testes)
✅ GenericRepositoryTests (7 testes)
✅ Controllers Tests (4 testes)
```

### Executar Testes
```bash
# Todos os testes
dotnet test

# Com detalhes
dotnet test --logger "console;verbosity=detailed"

# Teste específico
dotnet test --filter "FullyQualifiedName~UsuarioServiceTests"
```

## 📖 Documentação

### Documentos Disponíveis
1. **README.md** - Documentação principal do projeto
2. **QUICKSTART.md** - Guia de início rápido (5 minutos)
3. **TESTING.md** - Guia completo de testes unitários
4. **FRONTEND_GUIDE.md** - Manual de uso do frontend
5. **DELIVERABLES.md** - Sumário de entregas
6. **frontend/README.md** - Documentação técnica do Angular

### Swagger API
- Disponível em: http://localhost:8080/swagger
- Documentação interativa de todos os endpoints
- Teste direto da API

## 🐛 Troubleshooting

### Frontend não carrega
```bash
# Verificar logs
docker-compose logs frontend

# Reiniciar
docker-compose restart frontend

# Rebuild
docker-compose up --build frontend
```

### API não responde
```bash
# Verificar logs
docker-compose logs clima-api

# Testar diretamente
curl http://localhost:8080/Usuario

# Acessar Swagger
open http://localhost:8080/swagger
```

### Banco de dados
```bash
# Verificar SQL Server
docker-compose logs sqlserver

# Resetar banco
docker-compose down -v
docker-compose up
```

### Erro de CORS
- Verifique se frontend está em http://localhost:4200
- Reinicie a API: `docker-compose restart clima-api`

## 🚀 Deploy em Produção

### Variáveis de Ambiente
```bash
# Produção - use valores seguros!
PASSWORD_SQL=SenhaProducaoMuitoForte!@#
JWT_SECRET_KEY=chave-aleatoria-64-caracteres-minimo
EMAIL_USERNAME=producao@empresa.com
EMAIL_PASSWORD=SenhaDeAppSegura
OPENWEATHER_API_KEY=prod-api-key
```

### Build Otimizado
```bash
# Backend
dotnet publish -c Release

# Frontend
cd frontend
ng build --configuration production

# Docker
docker-compose -f docker-compose.prod.yml up -d
```

## 📈 Melhorias Futuras

### Backend
- [ ] Implementar paginação nas listas
- [ ] Adicionar filtros de busca
- [ ] Cache com Redis
- [ ] Rate limiting
- [ ] Logging estruturado (Serilog)
- [ ] Application Insights

### Frontend
- [ ] Autenticação JWT completa
- [ ] Guards de rota
- [ ] Interceptors HTTP
- [ ] Notificações toast
- [ ] Gráficos de temperatura
- [ ] PWA (Progressive Web App)
- [ ] Internacionalização (i18n)

### DevOps
- [ ] CI/CD com GitHub Actions
- [ ] Kubernetes deployment
- [ ] Monitoramento (Prometheus/Grafana)
- [ ] Backup automatizado
- [ ] HTTPS com Let's Encrypt

## 🎓 Aprendizados

Este projeto demonstra:
- ✅ Clean Architecture em .NET
- ✅ Repository Pattern
- ✅ Dependency Injection
- ✅ Unit Testing com xUnit e Moq
- ✅ Angular Standalone Components
- ✅ Reactive Programming com RxJS
- ✅ Docker Multi-stage builds
- ✅ Docker Compose orquestração
- ✅ CORS configuration
- ✅ JWT Authentication
- ✅ RESTful API design

## 👥 Contribuindo

### Setup de Desenvolvimento
```bash
# Backend
dotnet restore
dotnet build
dotnet run --project ClimaNotificacoesAPI.API

# Frontend
cd frontend
npm install
ng serve

# Testes
dotnet test
```

### Padrões de Código
- C#: seguir convenções .NET
- TypeScript: seguir Angular Style Guide
- Commits: mensagens descritivas em português
- PRs: com descrição detalhada

## 📞 Suporte

### Recursos
- **Swagger**: http://localhost:8080/swagger
- **Logs**: `docker-compose logs [serviço]`
- **Documentação**: Veja arquivos .md na raiz

### Problemas Comuns
- Porta ocupada: mude a porta no docker-compose.yml
- API lenta: verifique conexão com OpenWeatherMap
- Frontend sem dados: verifique se API está rodando

## 📝 Licença

MIT License - Veja LICENSE file para detalhes

## ✨ Agradecimentos

Projeto desenvolvido como sistema completo de gerenciamento de notificações climáticas, demonstrando boas práticas de desenvolvimento full-stack com .NET e Angular.

---

**Desenvolvido com ❤️ usando .NET 8, Angular 17, e Docker**

**Status do Projeto**: ✅ Completo e Funcional

**Última Atualização**: Fevereiro 2026
