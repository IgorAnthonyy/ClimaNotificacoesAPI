# 📱 Guia de Uso - Frontend ClimaNotificações

## 🚀 Acesso Rápido

### Com Docker (Mais Fácil)
```bash
# Na raiz do projeto
docker-compose up

# Acesse: http://localhost:4200
```

### Desenvolvimento Local
```bash
# Entre no diretório frontend
cd frontend

# Instale dependências (apenas primeira vez)
npm install

# Execute o servidor de desenvolvimento
ng serve

# Acesse: http://localhost:4200
```

**Importante**: A API backend deve estar rodando em http://localhost:8080

## 🎯 Funcionalidades da Interface

### 1️⃣ Página Inicial (Home)
- Dashboard com acesso rápido às funcionalidades
- Cards com links diretos para:
  - Gerenciamento de Usuários
  - Gerenciamento de Cidades
  - Visualização de Previsões

### 2️⃣ Gerenciamento de Usuários

#### Listar Usuários
1. Clique em "Usuários" no menu
2. Veja a lista de todos os usuários cadastrados
3. Cada usuário mostra: ID, Nome, E-mail, Telefone

#### Criar Novo Usuário
1. Na tela de Usuários, clique em "Novo Usuário"
2. Preencha o formulário:
   - **Nome**: Nome completo do usuário
   - **E-mail**: Endereço de e-mail válido
   - **Senha**: Senha para login
   - **Telefone**: Número de telefone
3. Clique em "Salvar"
4. Confirme a mensagem de sucesso

#### Editar Usuário
1. Na lista de usuários, clique em "Editar" no usuário desejado
2. Modifique os campos necessários
3. **Senha**: Deixe em branco para manter a senha atual
4. Clique em "Salvar"

#### Excluir Usuário
1. Na lista de usuários, clique em "Excluir"
2. Confirme a exclusão no diálogo
3. O usuário será removido do sistema

### 3️⃣ Gerenciamento de Cidades

#### Listar Cidades
1. Clique em "Cidades" no menu
2. Veja todas as cidades monitoradas
3. Cada cidade mostra: ID, Nome, Usuário responsável

#### Adicionar Nova Cidade
1. Na tela de Cidades, clique em "Nova Cidade"
2. Preencha o formulário:
   - **Nome da Cidade**: Nome da cidade (ex: São Paulo, Rio de Janeiro)
   - **Usuário**: Selecione o usuário responsável
3. Clique em "Salvar"
4. A cidade será adicionada ao monitoramento

#### Buscar Previsão do Tempo
1. Na lista de cidades, clique em "Previsão" na cidade desejada
2. Aguarde o carregamento
3. Visualize os dados climáticos:
   - **Condição**: Descrição do tempo (ex: céu limpo, nublado)
   - **Temperatura Máxima e Mínima**: Em graus Celsius
   - **Umidade**: Porcentagem de umidade do ar
   - **Velocidade do Vento**: Em km/h
4. Clique em "Fechar" para ocultar a previsão

#### Excluir Cidade
1. Na lista de cidades, clique em "Excluir"
2. Confirme a exclusão
3. A cidade será removida do monitoramento

## 🎨 Interface do Usuário

### Navegação
- **Barra Superior (Navbar)**: 
  - Azul com ícone do sistema
  - Links para Home, Usuários e Cidades
  - Sempre visível no topo

### Cards e Formulários
- **Cards Brancos**: Organizam o conteúdo
- **Botões Coloridos**:
  - 🔵 Azul: Ações primárias (Criar, Editar)
  - 🟢 Verde: Sucesso, Adicionar
  - 🟡 Amarelo: Editar
  - 🔴 Vermelho: Excluir
  - 🔵 Info: Previsão do Tempo

### Tabelas
- **Responsivas**: Adaptam-se ao tamanho da tela
- **Hover**: Destaque ao passar o mouse
- **Ações**: Botões de ação em cada linha

## ⚠️ Mensagens e Alertas

### Mensagens de Sucesso
- ✅ "Usuário criado com sucesso!"
- ✅ "Cidade adicionada com sucesso!"
- ✅ "Usuário atualizado com sucesso!"

### Mensagens de Erro
- ❌ "Erro ao carregar dados"
- ❌ "Erro ao criar usuário"
- ❌ "Erro ao buscar previsão"

### Loading
- Spinner de carregamento enquanto busca dados
- Indica que uma operação está em andamento

## 🔧 Solução de Problemas

### Frontend não carrega
1. Verifique se o Docker está rodando: `docker ps`
2. Verifique se a porta 4200 está livre
3. Tente: `docker-compose restart frontend`

### API não responde
1. Verifique se o backend está rodando
2. Acesse http://localhost:8080/swagger
3. Verifique os logs: `docker-compose logs clima-api`

### Erro ao buscar previsão
1. Verifique se a API Key do OpenWeatherMap está configurada
2. Verifique se o nome da cidade está correto
3. Alguns nomes de cidades podem precisar ser em inglês

### Erro de CORS
1. Verifique se CORS está habilitado no backend
2. Reinicie o backend: `docker-compose restart clima-api`

## 💡 Dicas de Uso

### Primeiro Acesso
1. ✅ Crie pelo menos um usuário
2. ✅ Adicione uma cidade para esse usuário
3. ✅ Busque a previsão do tempo

### Nomes de Cidades
- Use nomes conhecidos: São Paulo, Rio de Janeiro, Brasília
- Para cidades menores, tente o formato: "Cidade, Estado"
- Algumas cidades internacionais podem funcionar melhor em inglês

### Melhores Práticas
- 📧 Use e-mails reais para receber notificações
- 📱 Adicione um telefone válido
- 🔑 Use senhas fortes
- 🌍 Monitore cidades relevantes para você

## 📊 Recursos Visuais

### Ícones Utilizados
- 🏠 Home
- 👥 Usuários
- 📍 Cidades
- ☁️ Previsão do Tempo
- ✏️ Editar
- 🗑️ Excluir
- ➕ Adicionar

### Cores do Sistema
- **Azul**: Ações principais
- **Verde**: Sucesso, cidades
- **Amarelo**: Avisos, edição
- **Vermelho**: Exclusões, erros
- **Cinza**: Cancelar, secundário

## 🎓 Fluxo Completo de Uso

1. **Acesse** http://localhost:4200
2. **Crie um usuário**
   - Vá em Usuários → Novo Usuário
   - Preencha os dados
   - Salve
3. **Adicione uma cidade**
   - Vá em Cidades → Nova Cidade
   - Selecione o usuário criado
   - Digite o nome da cidade
   - Salve
4. **Consulte a previsão**
   - Na lista de cidades, clique em "Previsão"
   - Visualize os dados climáticos
5. **Gerencie conforme necessário**
   - Edite usuários
   - Adicione mais cidades
   - Exclua itens não necessários

## 📞 Suporte

Para mais informações:
- Consulte a documentação da API: http://localhost:8080/swagger
- Veja o README principal do projeto
- Verifique os logs do Docker: `docker-compose logs`

---

**Desenvolvido com Angular 17 + Bootstrap 5**
