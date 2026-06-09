# SpaceData - Gerenciador de Missões Espaciais

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-10.0-512BD4?style=flat-square)](https://learn.microsoft.com/ef/)
[![Oracle Database](https://img.shields.io/badge/Oracle-Database-F80000?style=flat-square)](https://www.oracle.com/)
[![Swagger](https://img.shields.io/badge/API-Swagger-85EA2D?style=flat-square)](https://swagger.io/)

---

## 📋 Resumo Executivo

**SpaceData** é uma API REST desenvolvida em **.NET 10.0** que gerencia agentes espaciais, missões e associações entre eles. O sistema implementa padrões de arquitetura em camadas (Controllers → Services → Data Access) com suporte completo a CRUD e integração com banco de dados Oracle.

### Características Principais:
- ✅ API REST com Swagger/OpenAPI
- ✅ Banco de dados Oracle com Entity Framework Core
- ✅ Arquitetura em camadas com separação de responsabilidades
- ✅ DTOs para validação de entrada/saída
- ✅ Mapeamento de entidades com Mappers
- ✅ Tratamento de erros estruturado
- ✅ Gerenciamento de relacionamentos N:N

---

## 🏗️ Arquitetura

### Diagrama da Arquitetura Geral

```
┌─────────────────────────────────────────────────────────────┐
│                     Cliente HTTP/Swagger                     │
└────────────────────────┬────────────────────────────────────┘
						 │
┌────────────────────────▼────────────────────────────────────┐
│              Controllers (API Endpoints)                     │
│  ┌─────────────────┐  ┌─────────────────┐  ┌────────────┐  │
│  │ AgenteController│  │ MissaoController│  │AgenteMissao│  │
│  └────────┬────────┘  └────────┬────────┘  └──────┬─────┘  │
└───────────┼──────────────────────┼──────────────────┼────────┘
			│                      │                  │
┌───────────▼──────────────────────▼──────────────────▼────────┐
│                   Services Layer                             │
│  ┌─────────────────┐  ┌─────────────────┐  ┌────────────┐   │
│  │ AgenteService   │  │ MissaoService   │  │AgenteMissao│   │
│  │                 │  │                 │  │Service     │   │
│  └────────┬────────┘  └────────┬────────┘  └──────┬─────┘   │
└───────────┼──────────────────────┼──────────────────┼────────┘
			│                      │                  │
┌───────────▼──────────────────────▼──────────────────▼────────┐
│                Mappers Layer (DTOs)                          │
│  ┌─────────────────┐  ┌─────────────────┐  ┌────────────┐   │
│  │ AgenteMapper    │  │ MissaoMapper    │  │AgenteMissao│   │
│  │                 │  │                 │  │Mapper      │   │
│  └────────┬────────┘  └────────┬────────┘  └──────┬─────┘   │
└───────────┼──────────────────────┼──────────────────┼────────┘
			│                      │                  │
┌───────────▼──────────────────────▼──────────────────▼────────┐
│            Data Access Layer (DbContext)                     │
│                  AppDbContext (EF Core)                      │
└─────────────────────────────────────────────────────────────┘
						 │
			┌────────────▼────────────┐
			│   Oracle Database       │
			│  ┌──────────────────┐   │
			│  │ T_AGENTE         │   │
			│  │ T_MISSAO         │   │
			│  │ T_AGENTE_MISSAO  │   │
			│  └──────────────────┘   │
			└────────────────────────┘
```

### Diagrama de Relacionamento de Dados

```
┌──────────────────────┐           ┌──────────────────────┐
│    T_AGENTE          │           │    T_MISSAO          │
├──────────────────────┤           ├──────────────────────┤
│ ID_AGENTE (PK) ──┐  │           │ ID_MISSAO (PK) ──┐  │
│ NOME             │  │           │ TIPO_MISSAO      │  │
│ ESPECIALIDADE    │  │           │ DATA_INICIO      │  │
│ NIVEL_SEGURANCA  │  │           │ STATUS           │  │
└────────┬─────────┘  │           └─────────┬────────┘  │
		 │            │                     │           │
		 │            │                     │           │
		 └────┬───────┴─────────────────┬───┘           │
			  │                         │               │
		 ┌────▼─────────────────────────▼──────┐        │
		 │    T_AGENTE_MISSAO (Tabela Pivot)   │        │
		 ├────────────────────────────────────┤        │
		 │ ID_AGENTE_MISSAO (PK)              │        │
		 │ ID_AGENTE (FK) ─────────────────────┼────────→
		 │ ID_MISSAO (FK) ─────────────────────┼────────→
		 │ DESCRICAO (Relatório)               │
		 └────────────────────────────────────┘
```

---

## 📦 Estrutura do Projeto

```
SpaceData/
├── Controllers/
│   ├── AgenteController.cs           # Endpoints para gerenciamento de agentes
│   ├── MissaoController.cs           # Endpoints para gerenciamento de missões
│   └── AgenteMissaoController.cs     # Endpoints para associações agente-missão
├── Services/
│   ├── AgenteService.cs              # Lógica de negócio para agentes
│   ├── MissaoService.cs              # Lógica de negócio para missões
│   └── AgenteMissaoService.cs        # Lógica de negócio para associações
├── Models/
│   ├── Agente.cs                     # Entidade de agente
│   ├── Missao.cs                     # Entidade de missão
│   └── AgenteMissao.cs               # Entidade de associação
├── DTOs/
│   ├── Request/                      # Modelos de entrada
│   │   ├── AgenteRequest.cs
│   │   ├── MissaoRequest.cs
│   │   └── AgenteMissaoRequest.cs
│   └── Response/                     # Modelos de saída
│       ├── AgenteResponse.cs
│       ├── MissaoResponse.cs
│       └── AgenteMissaoResponse.cs
├── Mappers/
│   ├── AgenteMapper.cs               # Mapeamento de Agente
│   ├── MissaoMapper.cs               # Mapeamento de Missão
│   └── AgenteMissaoMapper.cs         # Mapeamento de AgenteMissao
├── Data/
│   ├── AppDbContext.cs               # DbContext do Entity Framework Core
│   └── Migrations/                   # Histórico de migrações
├── Program.cs                        # Configuração da aplicação
├── appsettings.json                  # Configurações
└── SpaceData.csproj                  # Arquivo do projeto
```

---

## 🚀 Guia de Instalação

### Pré-requisitos
- **.NET 10.0 SDK** ou superior
- **Oracle Database** 19c ou 21c
- **Visual Studio 2022+** ou **VS Code**
- **Git**

### Passos de Instalação

#### 1. Clonar o Repositório
```bash
git clone <seu-repositorio>
cd SpaceData
```

#### 2. Configurar a Conexão com Oracle

Edite o arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
	"Oracle": "User Id=seu_usuario;Password=sua_senha;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=seu_host)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=seu_servico)));"
  },
  "Logging": {
	"LogLevel": {
	  "Default": "Information"
	}
  },
  "AllowedHosts": "*"
}
```

#### 3. Restaurar Dependências
```bash
dotnet restore
```

#### 4. Aplicar Migrações do Banco de Dados
```bash
dotnet ef database update
```

#### 5. Executar a Aplicação
```bash
dotnet run
```

A API estará disponível em: `https://localhost:5001`

---

## 📚 Documentação da API

### Acessar o Swagger

Após iniciar a aplicação, acesse:

```
https://localhost:5001/swagger/index.html
```

### Endpoints Principais

#### **Agentes**

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `POST` | `/api/agente` | Criar novo agente |
| `GET` | `/api/agente/{id}` | Buscar agente por ID |
| `GET` | `/api/agente` | Listar todos os agentes |
| `PUT` | `/api/agente/{id}` | Atualizar agente |
| `DELETE` | `/api/agente/{id}` | Deletar agente |

#### **Missões**

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `POST` | `/api/missao` | Criar nova missão |
| `GET` | `/api/missao/{id}` | Buscar missão por ID |
| `GET` | `/api/missao` | Listar todas as missões |
| `PUT` | `/api/missao/{id}` | Atualizar missão |
| `DELETE` | `/api/missao/{id}` | Deletar missão |

#### **Agente-Missão**

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `POST` | `/api/agente-missao` | Associar agente a missão |
| `GET` | `/api/agente-missao/{id}` | Buscar associação por ID |
| `GET` | `/api/agente-missao` | Listar todas as associações |
| `DELETE` | `/api/agente-missao/{id}` | Remover associação |

---

## 🧪 Testes

### Exemplos de Testes com cURL

#### 1. Criar um Agente
```bash
curl -X POST "https://localhost:5001/api/agente" \
  -H "Content-Type: application/json" \
  -d '{
	"nome": "Yuri Gagarin",
	"especialidade": "Exploração Espacial",
	"nivelSeguranca": 10
  }'
```

**Resposta Esperada (201 Created):**
```json
{
  "idAgente": "550e8400-e29b-41d4-a716-446655440000",
  "nome": "Yuri Gagarin",
  "especialidade": "Exploração Espacial",
  "nivelSeguranca": 10
}
```

#### 2. Buscar Agente por ID
```bash
curl -X GET "https://localhost:5001/api/agente/550e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json"
```

**Resposta Esperada (200 OK):**
```json
{
  "idAgente": "550e8400-e29b-41d4-a716-446655440000",
  "nome": "Yuri Gagarin",
  "especialidade": "Exploração Espacial",
  "nivelSeguranca": 10
}
```

#### 3. Listar Todos os Agentes
```bash
curl -X GET "https://localhost:5001/api/agente" \
  -H "Content-Type: application/json"
```

**Resposta Esperada (200 OK):**
```json
[
  {
	"idAgente": "550e8400-e29b-41d4-a716-446655440000",
	"nome": "Yuri Gagarin",
	"especialidade": "Exploração Espacial",
	"nivelSeguranca": 10
  },
  {
	"idAgente": "550e8400-e29b-41d4-a716-446655440001",
	"nome": "Neil Armstrong",
	"especialidade": "Comando",
	"nivelSeguranca": 9
  }
]
```

#### 4. Atualizar Agente
```bash
curl -X PUT "https://localhost:5001/api/agente/550e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json" \
  -d '{
	"nome": "Yuri Gagarin - Atualizado",
	"especialidade": "Exploração Espacial",
	"nivelSeguranca": 11
  }'
```

**Resposta Esperada (200 OK):**
```json
{
  "idAgente": "550e8400-e29b-41d4-a716-446655440000",
  "nome": "Yuri Gagarin - Atualizado",
  "especialidade": "Exploração Espacial",
  "nivelSeguranca": 11
}
```

#### 5. Deletar Agente
```bash
curl -X DELETE "https://localhost:5001/api/agente/550e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json"
```

**Resposta Esperada (204 No Content):** (sem corpo)

---

#### 6. Criar uma Missão
```bash
curl -X POST "https://localhost:5001/api/missao" \
  -H "Content-Type: application/json" \
  -d '{
	"tipoMissao": "Exploração Lunar",
	"dataInicio": "2026-01-15",
	"status": "Planejada"
  }'
```

**Resposta Esperada (201 Created):**
```json
{
  "idMissao": "660e8400-e29b-41d4-a716-446655440002",
  "tipoMissao": "Exploração Lunar",
  "dataInicio": "2026-01-15T00:00:00",
  "status": "Planejada"
}
```

#### 7. Associar Agente a Missão
```bash
curl -X POST "https://localhost:5001/api/agente-missao" \
  -H "Content-Type: application/json" \
  -d '{
	"idAgente": "550e8400-e29b-41d4-a716-446655440000",
	"idMissao": "660e8400-e29b-41d4-a716-446655440002",
	"relatorioMissao": "Agente responsável pela segurança da base lunar"
  }'
```

**Resposta Esperada (201 Created):**
```json
{
  "idAgenteMissao": "770e8400-e29b-41d4-a716-446655440003",
  "idAgente": "550e8400-e29b-41d4-a716-446655440000",
  "idMissao": "660e8400-e29b-41d4-a716-446655440002",
  "relatorioMissao": "Agente responsável pela segurança da base lunar"
}
```

#### 8. Listar Todas as Associações
```bash
curl -X GET "https://localhost:5001/api/agente-missao" \
  -H "Content-Type: application/json"
```

**Resposta Esperada (200 OK):**
```json
[
  {
	"idAgenteMissao": "770e8400-e29b-41d4-a716-446655440003",
	"idAgente": "550e8400-e29b-41d4-a716-446655440000",
	"idMissao": "660e8400-e29b-41d4-a716-446655440002",
	"relatorioMissao": "Agente responsável pela segurança da base lunar"
  }
]
```

---

### Testes com Postman

#### Importar Coleção

1. Abra o **Postman**
2. Clique em **Import** → **Link**
3. Cole a URL de uma coleção Postman (se disponível)
4. Clique em **Import**

#### Executar Testes Manually

1. **Criar Agente:**
   - Method: `POST`
   - URL: `{{base_url}}/api/agente`
   - Body (JSON): 
   ```json
   {
	 "nome": "Alan Bean",
	 "especialidade": "Piloto",
	 "nivelSeguranca": 8
   }
   ```

2. **Validar Resposta:**
   - Status: `201 Created`
   - Extrair `idAgente` da resposta
   - Usar em próximas requisições

3. **Usar Variáveis de Ambiente:**
   ```
   base_url = https://localhost:5001
   agent_id = <copiar do response anterior>
   ```

---

### Testes com PowerShell

```powershell
# 1. Criar Agente
$agente = @{
	nome = "Valentina Tereshkova"
	especialidade = "Piloto"
	nivelSeguranca = 9
} | ConvertTo-Json

$response = Invoke-WebRequest -Uri "https://localhost:5001/api/agente" `
	-Method POST `
	-ContentType "application/json" `
	-Body $agente `
	-SkipCertificateCheck

$agenteId = ($response.Content | ConvertFrom-Json).idAgente
Write-Host "Agente criado com ID: $agenteId"

# 2. Buscar Agente
Invoke-WebRequest -Uri "https://localhost:5001/api/agente/$agenteId" `
	-Method GET `
	-SkipCertificateCheck | Select-Object -ExpandProperty Content | ConvertFrom-Json

# 3. Listar Todos os Agentes
Invoke-WebRequest -Uri "https://localhost:5001/api/agente" `
	-Method GET `
	-SkipCertificateCheck | Select-Object -ExpandProperty Content | ConvertFrom-Json
```

---

## 🔧 Desenvolvimento

### Padrões de Código

#### Mapper Pattern
```csharp
// AgenteMapper.cs
public AgenteResponse AgenteToResponse(Agente agente)
{
	return new AgenteResponse
	{
		IdAgente = agente.IdAgente,
		Nome = agente.Nome,
		Especialidade = agente.Especialidade,
		NivelSeguranca = agente.NivelSeguranca
	};
}
```

#### Service Pattern
```csharp
// AgenteService.cs
public class AgenteService
{
	private readonly AppDbContext _context;
	private readonly AgenteMapper _mapper;

	public AgenteResponse CriarAgente(AgenteRequest request)
	{
		var agente = _mapper.AgenteRequestToEntity(request);
		agente.IdAgente = Guid.NewGuid().ToString();

		_context.Agentes.Add(agente);
		_context.SaveChanges();

		return _mapper.AgenteToResponse(agente);
	}
}
```

#### DTO Pattern
```csharp
// Request DTO
public class AgenteRequest
{
	[Required]
	[StringLength(150)]
	public string Nome { get; set; }

	[Required]
	[StringLength(100)]
	public string Especialidade { get; set; }

	[Required]
	[Range(1, 10)]
	public int NivelSeguranca { get; set; }
}

// Response DTO
public class AgenteResponse
{
	public string IdAgente { get; set; }
	public string Nome { get; set; }
	public string Especialidade { get; set; }
	public int NivelSeguranca { get; set; }
}
```

### Adicionar Nova Entidade

1. **Criar Model:**
   ```csharp
   // Models/NovaEntidade.cs
   [Table("T_NOVA_ENTIDADE")]
   public class NovaEntidade
   {
	   [Key]
	   [Column("ID_NOVA_ENTIDADE")]
	   public string Id { get; set; }

	   [Required]
	   [Column("NOME")]
	   public string Nome { get; set; }
   }
   ```

2. **Criar DTOs:**
   - `DTOs/Request/NovaEntidadeRequest.cs`
   - `DTOs/Response/NovaEntidadeResponse.cs`

3. **Criar Mapper:**
   - `Mappers/NovaEntidadeMapper.cs`

4. **Criar Service:**
   - `Services/NovaEntidadeService.cs`

5. **Criar Controller:**
   - `Controllers/NovaEntidadeController.cs`

6. **Registrar em Program.cs:**
   ```csharp
   builder.Services.AddScoped<NovaEntidadeService>();
   builder.Services.AddScoped<NovaEntidadeMapper>();
   ```

7. **Executar Migrations:**
   ```bash
   dotnet ef migrations add AddNovaEntidade
   dotnet ef database update
   ```

---

## 🛠️ Troubleshooting

### Erro: Conexão com Oracle Recusada
**Solução:**
- Verificar credenciais em `appsettings.json`
- Confirmar que o serviço Oracle está rodando
- Testar conexão com SQL*Plus

### Erro: Migrations Pendentes
**Solução:**
```bash
dotnet ef database update
```

### Erro: Port 5001 Já em Uso
**Solução:**
```bash
# Windows
netstat -ano | findstr :5001
taskkill /PID <PID> /F

# Linux/Mac
lsof -i :5001
kill -9 <PID>
```

### Erro: SSL Certificate Not Trusted
**Solução (apenas desenvolvimento):**
```bash
dotnet dev-certs https --trust
```

---

## 📖 Recursos Adicionais

- [Documentação do .NET 10](https://learn.microsoft.com/pt-br/dotnet/)
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)
- [ASP.NET Core Web API](https://learn.microsoft.com/pt-br/aspnet/core/web-api/)
- [Swagger/OpenAPI](https://swagger.io/)
- [Oracle Database](https://www.oracle.com/br/database/)

---

## 👥 Contribuidores

- Equipo de Desenvolvimento SpaceData

---

## 📄 Licença

Este projeto está licenciado sob a MIT License - veja o arquivo LICENSE para detalhes.

---

## 📞 Suporte

Para dúvidas ou problemas, entre em contato ou abra uma issue no repositório.

**Última atualização:** 09/06/2026
