# 🧪 Testes Unitários - ClimaNotificacoesAPI

Este documento descreve a estrutura e execução dos testes unitários do projeto.

## 📊 Estatísticas dos Testes

- **Total de Testes**: 38
- **Taxa de Sucesso**: 100% ✅
- **Framework**: xUnit
- **Mocking**: Moq
- **Assertions**: FluentAssertions
- **Data Generation**: AutoFixture

## 🏗️ Estrutura dos Testes

```
ClimaNotificacoesAPI.Tests/
├── Helpers/
│   └── WeatherConditionHelperTests.cs (7 testes)
├── Services/
│   ├── UsuarioServiceTests.cs (8 testes)
│   ├── CidadeServiceTests.cs (8 testes)
│   └── TokenServiceTests.cs (4 testes)
└── Repositories/
    └── GenericRepositoryTests.cs (7 testes)
```

## 🧩 Cobertura de Testes

### Helpers (7 testes)
- ✅ WeatherConditionHelper
  - Validação de condições que requerem alerta
  - Validação case-insensitive
  - Condições que não requerem alerta

### Services (20 testes)

#### UsuarioService (8 testes)
- ✅ GetByIdAsync - Retorna usuário existente
- ✅ GetByIdAsync - Lança exceção quando não encontrado
- ✅ CreateAsync - Cria usuário com email único
- ✅ CreateAsync - Lança exceção para email duplicado
- ✅ UpdateAsync - Atualiza usuário existente
- ✅ DeleteAsync - Remove usuário
- ✅ GetAllAsync - Retorna todos os usuários
- ✅ GetByEmailAsync - Busca por email

#### CidadeService (8 testes)
- ✅ GetByIdAsync - Retorna cidade existente
- ✅ GetByIdAsync - Lança exceção quando não encontrada
- ✅ CreateAsync - Cria cidade única para usuário
- ✅ CreateAsync - Lança exceção para cidade duplicada
- ✅ DeleteAsync - Remove cidade
- ✅ GetAllAsync - Retorna todas as cidades
- ✅ GetPrevisaoTempoByCidadeAsync - Retorna previsões

#### TokenService (4 testes)
- ✅ GenerateToken - Gera token JWT válido
- ✅ GenerateToken - Inclui claims corretos
- ✅ GenerateToken - Define expiração correta
- ✅ GenerateToken - Gera tokens diferentes para usuários diferentes

### Repositories (7 testes)

#### GenericRepository (7 testes)
- ✅ AddAsync - Adiciona entidade
- ✅ GetByIdAsync - Retorna entidade existente
- ✅ GetByIdAsync - Retorna null quando não existe
- ✅ GetAllAsync - Retorna todas as entidades
- ✅ UpdateAsync - Atualiza entidade
- ✅ DeleteAsync - Remove entidade existente
- ✅ DeleteAsync - Não faz nada quando entidade não existe

## 🚀 Como Executar os Testes

### Executar Todos os Testes

```bash
dotnet test
```

### Executar com Verbosidade Detalhada

```bash
dotnet test --logger "console;verbosity=detailed"
```

### Executar Testes Específicos

```bash
# Executar apenas testes de serviços
dotnet test --filter "FullyQualifiedName~Services"

# Executar apenas testes de UsuarioService
dotnet test --filter "FullyQualifiedName~UsuarioServiceTests"

# Executar um teste específico
dotnet test --filter "FullyQualifiedName~GetByIdAsync_ShouldReturnUsuario"
```

### Executar com Cobertura de Código

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## 📦 Dependências de Teste

As seguintes bibliotecas são utilizadas:

- **xUnit** (2.4.2+) - Framework de teste
- **Moq** (4.20+) - Mocking de dependências
- **FluentAssertions** (8.8+) - Assertions fluentes e legíveis
- **AutoFixture** (4.18+) - Geração automática de dados de teste
- **Microsoft.EntityFrameworkCore.InMemory** (10.0+) - Banco de dados em memória para testes

## 🎯 Padrões Utilizados

### AAA Pattern (Arrange-Act-Assert)

Todos os testes seguem o padrão AAA:

```csharp
[Fact]
public async Task NomeDoTeste_DeveRetornarResultadoEsperado_QuandoCondicao()
{
    // Arrange - Preparação dos dados e mocks
    var usuario = new Usuario { Id = 1, Nome = "Teste" };
    
    // Act - Execução do método sendo testado
    var result = await _service.GetByIdAsync(1);
    
    // Assert - Verificação dos resultados
    result.Should().NotBeNull();
    result.Id.Should().Be(1);
}
```

### Nomenclatura de Testes

```
MetodoSendoTestado_DeveComportamentoEsperado_QuandoCondicao
```

Exemplos:
- `GetByIdAsync_ShouldReturnUsuario_WhenUsuarioExists`
- `CreateAsync_ShouldThrowException_WhenEmailAlreadyExists`

## 🔍 Debugging de Testes

### Visual Studio Code

1. Instale a extensão ".NET Core Test Explorer"
2. Clique no ícone de teste na barra lateral
3. Execute ou debug testes individuais

### Linha de Comando

```bash
# Executar em modo watch (reexecuta ao salvar)
dotnet watch test
```

## 📝 Boas Práticas Implementadas

✅ **Isolamento**: Cada teste é independente e não depende de outros
✅ **Mocking**: Dependências externas são mockadas com Moq
✅ **Dados de Teste**: AutoFixture gera dados de teste de forma consistente
✅ **Assertions Claras**: FluentAssertions torna as verificações legíveis
✅ **Nomenclatura**: Nomes descritivos seguindo convenção estabelecida
✅ **Performance**: Uso de InMemoryDatabase para testes de repositórios
✅ **Cobertura**: Testes cobrem casos de sucesso e exceções

## 🚧 Adicionar Novos Testes

Para adicionar novos testes:

1. Crie um arquivo `*Tests.cs` na pasta apropriada
2. Siga o padrão AAA
3. Use as mesmas bibliotecas (xUnit, Moq, FluentAssertions)
4. Execute `dotnet test` para validar

Exemplo de novo teste:

```csharp
using Xunit;
using Moq;
using FluentAssertions;

namespace ClimaNotificacoesAPI.Tests.Services;

public class NovoServiceTests
{
    [Fact]
    public async Task MetodoNovo_DeveRetornarSucesso_QuandoDadosValidos()
    {
        // Arrange
        var mockRepo = new Mock<IRepository>();
        var service = new NovoService(mockRepo.Object);
        
        // Act
        var result = await service.MetodoNovo();
        
        // Assert
        result.Should().NotBeNull();
    }
}
```

## 📊 Relatórios de Teste

Para gerar relatórios HTML de cobertura de código, instale a ferramenta ReportGenerator:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool

# Executar testes com cobertura
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Gerar relatório HTML
reportgenerator -reports:coverage.opencover.xml -targetdir:coverage-report
```

Abra `coverage-report/index.html` no navegador para visualizar.

## 🎓 Aprendizado

Estes testes demonstram:
- Como testar serviços com dependências
- Como mockar repositórios e serviços
- Como testar geração de JWT tokens
- Como usar InMemoryDatabase para testes de repositórios
- Como validar exceções personalizadas
- Como organizar testes de forma escalável

## 📞 Suporte

Para dúvidas sobre os testes:
1. Consulte a documentação do xUnit: https://xunit.net/
2. Consulte a documentação do Moq: https://github.com/moq/moq4
3. Consulte a documentação do FluentAssertions: https://fluentassertions.com/
