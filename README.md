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
- Docker

## ⚙️ Configuração

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

### Como Executar

#### Com Docker

```bash
docker-compose up
```

#### Localmente

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

## 📝 Licença

Este projeto está sob licença MIT.
