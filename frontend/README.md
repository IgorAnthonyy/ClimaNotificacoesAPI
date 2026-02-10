# Frontend ClimaNotificacoesAPI

Frontend Angular para o sistema de gerenciamento de notificações climáticas.

## 🚀 Tecnologias

- Angular 17
- TypeScript
- Bootstrap 5
- Bootstrap Icons
- RxJS

## 📋 Pré-requisitos

- Node.js 20 ou superior
- npm

## 🔧 Instalação

```bash
# Instalar dependências
npm install
```

## 💻 Desenvolvimento

```bash
# Executar servidor de desenvolvimento
ng serve
```

A aplicação estará disponível em `http://localhost:4200`

## 🏗️ Build

```bash
# Build para produção
ng build --configuration production
```

Os arquivos de build estarão no diretório `dist/`.

## 📱 Funcionalidades

### Gerenciamento de Usuários
- Listar todos os usuários
- Criar novo usuário
- Editar usuário existente
- Excluir usuário

### Gerenciamento de Cidades
- Listar todas as cidades
- Adicionar nova cidade
- Excluir cidade
- Buscar previsão do tempo para uma cidade

### Visualização de Previsões
- Visualizar condição climática
- Temperaturas máxima e mínima
- Umidade
- Velocidade do vento

## 🌐 Configuração da API

O frontend se conecta à API backend em `http://localhost:8080`

## 🎨 UI/UX

A interface utiliza Bootstrap 5 para um design responsivo e moderno.

## 📝 Licença

MIT
