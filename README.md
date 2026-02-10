# ClimaNotificacoesAPI

API para gerenciamento de notificações climáticas, desenvolvida em .NET seguindo os princípios de Clean Architecture.

## 📋 Sobre o Projeto

Este projeto fornece uma API REST para consulta de previsão do tempo e envio de notificações por e-mail quando condições climáticas específicas são detectadas.

## 🏗️ Arquitetura

O projeto segue Clean Architecture com separação em camadas:

- **ClimaNotificacoesAPI.API**: Camada de apresentação (Controllers, Middlewares)
- **ClimaNotificacoesAPI.Application**: Camada de aplicação (Services, DTOs, Jobs)
- **ClimaNotificacoesAPI.Domain**: Camada de domínio (Entities, Interfaces)
- **ClimaNotificacoesAPI.Infrastructure**: Camada de infraestrutura (Repositories, Data Context, Auth)

## 🚀 Tecnologias Utilizadas

- .NET 8.0
- Entity Framework Core
- SQL Server
- JWT Authentication
- Mapster (Object Mapping)
- MailKit (Email)
- OpenWeatherMap API
- Docker & Docker Compose

## 🐳 Como Executar com Docker (Recomendado)

### Pré-requisitos

- Docker
- Docker Compose

### Passos

1. **Clone o repositório**
   ```bash
   git clone https://github.com/IgorAnthonyy/ClimaNotificacoesAPI.git
   cd ClimaNotificacoesAPI
   ```

2. **Configure as variáveis de ambiente**
   
   Copie o arquivo `.env.example` para `.env`:
   ```bash
   cp .env.example .env
   ```

   Edite o arquivo `.env` e configure as seguintes variáveis:
   ```bash
   # Senha forte para o SQL Server (obrigatório)
   PASSWORD_SQL=YourStr0ng!Password#2024

   # Credenciais de email para envio de alertas (opcional, mas recomendado)
   EMAIL_USERNAME=seu@email.com
   EMAIL_PASSWORD=suaSenhaDeEmail

   # Chave JWT (use uma chave forte em produção)
   JWT_SECRET_KEY=sua-chave-super-secreta-minima-32-caracteres

   # API Key do OpenWeatherMap (obtenha em https://openweathermap.org/api)
   OPENWEATHER_API_KEY=sua-api-key-aqui
   ```

3. **Execute o projeto**
   ```bash
   docker-compose up
   ```

   A aplicação estará disponível em:
   - **API**: http://localhost:8080
   - **Swagger**: http://localhost:8080/swagger

4. **Para parar a aplicação**
   ```bash
   docker-compose down
   ```

5. **Para limpar volumes e recomeçar**
   ```bash
   docker-compose down -v
   docker-compose up --build
   ```

## ⚙️ Configuração Manual (Sem Docker)

### Variáveis de Ambiente

Configure as seguintes variáveis no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "sua-connection-string-aqui"
  },
  "JwtSettings": {
    "Key": "sua-chave-secreta-aqui",
    "Issuer": "seu-issuer",
    "Audience": "seu-audience",
    "ExpireMinutes": 60
  },
  "EmailSettings": {
    "Username": "seu-email@exemplo.com",
    "Password": "sua-senha",
    "SmtpServer": "smtp-mail.outlook.com",
    "SmtpPort": 587
  },
  "OpenWeatherMap": {
    "ApiKey": "sua-api-key-aqui"
  }
}
```

### Como Executar Localmente (sem Docker)

```bash
dotnet restore
dotnet build
dotnet run --project ClimaNotificacoesAPI.API
```

## 📚 Endpoints Principais

### Usuários
- `GET /Usuario` - Lista todos os usuários
- `GET /Usuario/{id}` - Busca usuário por ID
- `POST /Usuario` - Cria novo usuário
- `PUT /Usuario/{id}` - Atualiza usuário
- `DELETE /Usuario/{id}` - Remove usuário
- `POST /Usuario/login` - Autentica usuário
- `GET /Usuario/{id}/cidades` - Lista cidades do usuário

### Cidades
- `GET /Cidade` - Lista todas as cidades
- `GET /Cidade/{id}` - Busca cidade por ID
- `POST /Cidade` - Adiciona cidade
- `DELETE /Cidade/{id}` - Remove cidade
- `GET /Cidade/{id}/previsoes` - Lista previsões da cidade

### Previsão do Tempo
- `POST /PrevisaoTempo/{id}` - Busca e atualiza previsão

## 🔄 Melhorias Recentes (Refatoração)

### Segurança
- ✅ Removido hardcoded secrets do código
- ✅ Configurações agora carregadas de appsettings.json
- ✅ Adicionada validação de configurações

### Qualidade de Código
- ✅ Implementado padrão Generic Repository
- ✅ Removido código duplicado (~100 linhas)
- ✅ Substituído loops foreach por LINQ
- ✅ Removido null checks redundantes
- ✅ Criado IWeatherService para melhor testabilidade
- ✅ Padronização de nomenclatura (inglês)

### Arquitetura
- ✅ Adicionado WeatherConditionHelper para condições climáticas
- ✅ Criadas exceptions customizadas
- ✅ Validação de DTOs com Data Annotations
- ✅ Melhor uso de injeção de dependência

## 🧪 Testes Unitários

O projeto possui **38 testes unitários** com 100% de taxa de sucesso cobrindo:

- ✅ **Services**: UsuarioService, CidadeService, TokenService (20 testes)
- ✅ **Helpers**: WeatherConditionHelper (7 testes)
- ✅ **Repositories**: GenericRepository (7 testes)
- ✅ **Controllers**: Validação de endpoints (4 testes)

### Executar Testes

```bash
# Executar todos os testes
dotnet test

# Executar com detalhes
dotnet test --logger "console;verbosity=detailed"

# Executar testes específicos
dotnet test --filter "FullyQualifiedName~Services"
```

Para mais detalhes sobre os testes, consulte [TESTING.md](TESTING.md).

### Tecnologias de Teste

- xUnit - Framework de teste
- Moq - Mocking
- FluentAssertions - Assertions
- AutoFixture - Geração de dados
- EF Core InMemory - Banco de dados em memória

## 📝 Licença

Este projeto está sob licença MIT.
