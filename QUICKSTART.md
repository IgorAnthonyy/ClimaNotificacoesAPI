# 🚀 Guia de Início Rápido - ClimaNotificacoesAPI

Este guia irá ajudá-lo a executar a aplicação em menos de 5 minutos usando Docker.

## Pré-requisitos

- Docker instalado ([Download aqui](https://www.docker.com/get-started))
- Docker Compose instalado (geralmente vem com Docker Desktop)

## Passos para Executar

### 1. Clone o Repositório

```bash
git clone https://github.com/IgorAnthonyy/ClimaNotificacoesAPI.git
cd ClimaNotificacoesAPI
```

### 2. Configure as Variáveis de Ambiente

Copie o arquivo de exemplo:
```bash
cp .env.example .env
```

**Configuração Mínima Obrigatória:**

Edite o arquivo `.env` e altere pelo menos estas variáveis:

```bash
# Senha do SQL Server (OBRIGATÓRIO - use uma senha forte)
PASSWORD_SQL=MinhaSenh@Forte123!

# API Key do OpenWeatherMap (Obtenha grátis em https://openweathermap.org/api)
OPENWEATHER_API_KEY=sua-chave-api-aqui
```

**Configuração Completa (Opcional mas Recomendada):**

Para habilitar notificações por email, configure também:

```bash
# Email para envio de alertas
EMAIL_USERNAME=seu.email@gmail.com
EMAIL_PASSWORD=sua-senha-de-app

# Chave JWT (use uma chave forte e única)
JWT_SECRET_KEY=minha-chave-jwt-super-secreta-minima-32-caracteres-aqui
```

> **Dica Gmail**: Para usar Gmail, você precisa criar uma "Senha de App" nas configurações de segurança da sua conta Google.

### 3. Execute a Aplicação

```bash
docker-compose up
```

Aguarde alguns segundos enquanto:
- ✅ SQL Server é iniciado
- ✅ Banco de dados é criado automaticamente
- ✅ API é compilada e iniciada

### 4. Acesse a Aplicação

Abra seu navegador em:

- **Swagger UI (Documentação Interativa)**: http://localhost:8080/swagger
- **API**: http://localhost:8080

### 5. Teste a API

1. Acesse http://localhost:8080/swagger
2. Experimente os endpoints disponíveis
3. Comece criando um usuário com `POST /Usuario`
4. Faça login com `POST /Usuario/login`
5. Use o token JWT retornado para acessar endpoints protegidos

## Comandos Úteis

### Parar a aplicação
```bash
docker-compose down
```

### Reiniciar do zero (limpa o banco de dados)
```bash
docker-compose down -v
docker-compose up
```

### Ver logs em tempo real
```bash
docker-compose logs -f clima-api
```

### Reconstruir a imagem após mudanças no código
```bash
docker-compose up --build
```

## Solução de Problemas

### A API não inicia
- Verifique se as portas 8080 e 1433 não estão em uso
- Verifique os logs: `docker-compose logs`

### Erro de conexão com banco de dados
- Aguarde alguns segundos, o SQL Server pode demorar para iniciar
- Verifique se a senha do SQL Server atende aos requisitos de complexidade

### Erro na API do OpenWeatherMap
- Verifique se sua API key está correta no arquivo `.env`
- Confirme que sua API key está ativa em https://openweathermap.org

## Próximos Passos

1. Explore a documentação Swagger
2. Configure suas credenciais de email para receber alertas
3. Adicione cidades para monitoramento
4. Personalize as condições de alerta conforme necessário

## Suporte

Para mais informações, consulte o [README.md](README.md) completo.
