# SpaceData — Gerenciador de Missões Espaciais

# LINK PARA APRESENTAÇÃO - https://youtu.be/EL7jyyPLSmg
# LINK PARA VÍDEO PITCH - https://youtu.be/8bi7kcp1GFY?si=QPeHqC9qH0kXjAyY

API REST desenvolvida em **.NET 10.0** para gerenciamento de agentes espaciais, missões e seus vínculos. O sistema adota arquitetura em camadas com Repository Pattern, operações assíncronas e integração com banco de dados Oracle via Entity Framework Core.

## Arquitetura

### Fluxo de Camadas

```
┌─────────────────────────────────────────────────┐
│              Cliente HTTP / Swagger              │
└────────────────────┬────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────┐
│                 Controllers                      │
│   AgenteController  │  MissaoController          │
│              AgenteMissaoController              │
└────────────────────┬────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────┐
│                  Services                        │
│   AgenteService  │  MissaoService                │
│              AgenteMissaoService                 │
└────────────────────┬────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────┐
│               Repositories                       │
│  IAgenteRepository  │  IMissaoRepository         │
│           IAgenteMissaoRepository                │
└────────────────────┬────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────┐
│            Data — AppDbContext (EF Core)         │
└────────────────────┬────────────────────────────┘
                     │
          ┌──────────▼──────────┐
          │   Oracle Database   │
          │  ┌───────────────┐  │
          │  │  T_AGENTE     │  │
          │  │  T_MISSAO     │  │
          │  │T_AGENTE_MISSAO│  │
          │  └───────────────┘  │
          └─────────────────────┘
```

### Diagrama de Entidades

```
┌─────────────────────┐           ┌─────────────────────┐
│      T_AGENTE       │           │      T_MISSAO        │
├─────────────────────┤           ├─────────────────────┤
│ ID_AGENTE (PK)      │           │ ID_MISSAO (PK)       │
│ NM_AGENTE           │           │ NM_MISSAO            │
│ DT_NASCIMENTO       │           │ DT_INICIO            │
│ ST_AGENTE           │           │ DURACAO_ESTIMADA     │
│ ESPECIALIDADE       │           │ DESCRICAO            │
└──────────┬──────────┘           │ ST_MISSAO            │
           │                      └──────────┬───────────┘
           │                                 │
           └──────────────┬──────────────────┘
                          │
          ┌───────────────▼──────────────────┐
          │        T_AGENTE_MISSAO            │
          ├──────────────────────────────────┤
          │ ID_AGENTE_MISSAO (PK)            │
          │ ID_AGENTE (FK) → T_AGENTE        │
          │ ID_MISSAO (FK) → T_MISSAO        │
          │ DESCRICAO (Relatório)            │
          └──────────────────────────────────┘
```

### Padrões Aplicados

| Padrão | Descrição |
|---|---|
| Repository Pattern | Abstração do acesso a dados via interfaces |
| Service Layer | Regras de negócio isoladas dos controllers |
| DTO Pattern | Separação entre modelos de entrada (Request) e saída (Response) |
| Mapper estático | Conversão entre entidades e DTOs sem dependência de injeção |
| Async/Await | Todas as operações de I/O são não-bloqueantes |

---

## Estrutura do Projeto

```
SpaceData/
├── Controllers/
│   └── Controllers.cs          # AgenteController, MissaoController, AgenteMissaoController
├── Services/
│   └── Services.cs             # AgenteService, MissaoService, AgenteMissaoService
├── Repositories/
│   └── Repositories.cs         # Interfaces + implementações
├── Mappers/
│   └── Mappers.cs              # AgenteMapper, MissaoMapper, AgenteMissaoMapper (estáticos)
├── Models/
│   ├── Agente.cs
│   ├── Missao.cs
│   └── AgenteMissao.cs
├── DTOs/
│   ├── Request/
│   │   ├── AgenteRequest.cs
│   │   ├── MissaoRequest.cs
│   │   └── AgenteMissaoRequest.cs
│   └── Response/
│       ├── AgenteResponse.cs
│       ├── MissaoResponse.cs
│       └── AgenteMissaoResponse.cs
├── Data/
│   └── AppDbContext.cs
├── Migrations/
├── Properties/
│   └── launchSettings.json
├── Program.cs
├── appsettings.json
└── SpaceData.csproj
```

---

## Instalação e Execução

### Pré-requisitos

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10)
- Acesso ao Oracle Database (FIAP ou local)
- Visual Studio 2022+ ou VS Code
- Git

### Passo a passo

```bash
# 1. Clonar o repositório
git clone <url-do-repositorio>
cd SpaceData

# 2. Restaurar dependências
dotnet restore

# 3. Configurar a conexão com o banco em appsettings.json
# (ver seção abaixo)

# 4. Aplicar migrations
dotnet ef database update

# 5. Executar
dotnet run
```

### Configuração do banco (`appsettings.json`)

```json
{
  "ConnectionStrings": {
    "Oracle": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL"
  }
}
```

---

## Migrations

### Via CLI

```bash
# Reverter banco
dotnet ef database update 0

# Remover migration
dotnet ef migrations remove

# Criar nova migration
dotnet ef migrations add InitialCreate

# Aplicar ao banco
dotnet ef database update
```

### Via Package Manager Console (PMC — Visual Studio)

```powershell
# Reverter banco
Update-Database 0

# Remover migration
Remove-Migration

# Criar nova migration
Add-Migration InitialCreate

# Aplicar ao banco
Update-Database
```

> No PMC, confirme que o **Default project** no dropdown aponta para o projeto **SpaceData** antes de executar os comandos.

---

## Acesso ao Swagger

Após iniciar a aplicação, acesse:

```
https://localhost:5001/swagger/index.html
http://localhost:5050/swagger/index.html
```

O Swagger lista todos os endpoints com schemas de entrada/saída, códigos de resposta esperados e permite executar requisições diretamente pelo navegador.

---

## Endpoints

### Agentes — `/api/agente`

| Método | Rota | Descrição | Status |
|---|---|---|---|
| `POST` | `/api/agente` | Cadastrar agente | 201 / 400 |
| `GET` | `/api/agente` | Listar todos | 200 |
| `GET` | `/api/agente/{id}` | Buscar por ID | 200 / 404 |
| `PUT` | `/api/agente/{id}` | Atualizar | 200 / 400 / 404 |
| `DELETE` | `/api/agente/{id}` | Deletar | 204 / 404 |

### Missões — `/api/missao`

| Método | Rota | Descrição | Status |
|---|---|---|---|
| `POST` | `/api/missao` | Cadastrar missão | 201 / 400 |
| `GET` | `/api/missao` | Listar todas | 200 |
| `GET` | `/api/missao/{id}` | Buscar por ID | 200 / 404 |
| `PUT` | `/api/missao/{id}` | Atualizar | 200 / 400 / 404 |
| `DELETE` | `/api/missao/{id}` | Deletar | 204 / 404 |

### Vínculos — `/api/agentemissao`

| Método | Rota | Descrição | Status |
|---|---|---|---|
| `POST` | `/api/agentemissao` | Vincular agente à missão | 201 / 400 / 404 |
| `GET` | `/api/agentemissao` | Listar todos | 200 |
| `GET` | `/api/agentemissao/{id}` | Buscar por ID | 200 / 404 |
| `PUT` | `/api/agentemissao/{id}` | Atualizar vínculo | 200 / 400 / 404 |
| `DELETE` | `/api/agentemissao/{id}` | Remover vínculo | 204 / 404 |

---

## Exemplos de Testes

> A ordem recomendada é: **Agente → Missão → AgenteMissão**, pois o vínculo depende dos dois anteriores.

---

### 1. Cadastrar Agente

**Request**
```http
POST /api/agente
Content-Type: application/json

{
  "nome": "Yuri Gagarin",
  "dtNascimento": "1934-03-09",
  "status": "1",
  "especialidade": "Piloto de Combate"
}
```

**Response — 201 Created**
```json
{
  "idAgente": "a3f1c2d4-e5b6-7890-abcd-ef1234567890",
  "nome": "Yuri Gagarin",
  "dtNascimento": "1934-03-09",
  "status": "1",
  "especialidade": "Piloto de Combate"
}
```

---

### 2. Listar Todos os Agentes

**Request**
```http
GET /api/agente
```

**Response — 200 OK**
```json
[
  {
    "idAgente": "a3f1c2d4-e5b6-7890-abcd-ef1234567890",
    "nome": "Yuri Gagarin",
    "dtNascimento": "1934-03-09",
    "status": "1",
    "especialidade": "Piloto de Combate"
  }
]
```

---

### 3. Buscar Agente por ID

**Request**
```http
GET /api/agente/a3f1c2d4-e5b6-7890-abcd-ef1234567890
```

**Response — 200 OK**
```json
{
  "idAgente": "a3f1c2d4-e5b6-7890-abcd-ef1234567890",
  "nome": "Yuri Gagarin",
  "dtNascimento": "1934-03-09",
  "status": "1",
  "especialidade": "Piloto de Combate"
}
```

**Response — 404 Not Found**
```json
{
  "message": "Agente não encontrado com ID: a3f1c2d4-e5b6-7890-abcd-ef1234567890"
}
```

---

### 4. Atualizar Agente

**Request**
```http
PUT /api/agente/a3f1c2d4-e5b6-7890-abcd-ef1234567890
Content-Type: application/json

{
  "nome": "Yuri Gagarin",
  "dtNascimento": "1934-03-09",
  "status": "3",
  "especialidade": "Exploração Orbital"
}
```

**Response — 200 OK**
```json
{
  "idAgente": "a3f1c2d4-e5b6-7890-abcd-ef1234567890",
  "nome": "Yuri Gagarin",
  "dtNascimento": "1934-03-09",
  "status": "3",
  "especialidade": "Exploração Orbital"
}
```

---

### 5. Cadastrar Missão

**Request**
```http
POST /api/missao
Content-Type: application/json

{
  "nomeMissao": "Vostok 1",
  "dtInicio": "2026-04-12",
  "duracaoEstimada": 1,
  "descricao": "Primeira missão orbital tripulada da história, com objetivo de orbitar a Terra e retornar com segurança.",
  "status": "1"
}
```

**Response — 201 Created**
```json
{
  "idMissao": "b4e2d3f5-a6c7-8901-bcde-f01234567891",
  "nomeMissao": "Vostok 1",
  "dtInicio": "2026-04-12",
  "duracaoEstimada": 1,
  "descricao": "Primeira missão orbital tripulada da história, com objetivo de orbitar a Terra e retornar com segurança.",
  "status": "1"
}
```

---

### 6. Listar Todas as Missões

**Request**
```http
GET /api/missao
```

**Response — 200 OK**
```json
[
  {
    "idMissao": "b4e2d3f5-a6c7-8901-bcde-f01234567891",
    "nomeMissao": "Vostok 1",
    "dtInicio": "2026-04-12",
    "duracaoEstimada": 1,
    "descricao": "Primeira missão orbital tripulada da história, com objetivo de orbitar a Terra e retornar com segurança.",
    "status": "1"
  }
]
```

---

### 7. Vincular Agente à Missão

> Requer que o agente e a missão já existam no sistema.

**Request**
```http
POST /api/agentemissao
Content-Type: application/json

{
  "idAgente": "a3f1c2d4-e5b6-7890-abcd-ef1234567890",
  "idMissao": "b4e2d3f5-a6c7-8901-bcde-f01234567891",
  "relatorioMissao": "Agente designado como piloto principal da cápsula Vostok."
}
```

**Response — 201 Created**
```json
{
  "idAgenteMissao": "c5f3e4a6-b7d8-9012-cdef-012345678902",
  "nomeAgente": "Yuri Gagarin",
  "nomeMissao": "Vostok 1",
  "relatorioMissao": "Agente designado como piloto principal da cápsula Vostok."
}
```

**Response — 404 Not Found** (agente ou missão inexistente)
```json
{
  "message": "Agente não encontrado com ID: a3f1c2d4-e5b6-7890-abcd-ef1234567890"
}
```

---

### 8. Listar Todos os Vínculos

**Request**
```http
GET /api/agentemissao
```

**Response — 200 OK**
```json
[
  {
    "idAgenteMissao": "c5f3e4a6-b7d8-9012-cdef-012345678902",
    "nomeAgente": "Yuri Gagarin",
    "nomeMissao": "Vostok 1",
    "relatorioMissao": "Agente designado como piloto principal da cápsula Vostok."
  }
]
```

---

### 9. Remover Vínculo

**Request**
```http
DELETE /api/agentemissao/c5f3e4a6-b7d8-9012-cdef-012345678902
```

**Response — 204 No Content** *(sem corpo)*

---

### 10. Deletar Agente

**Request**
```http
DELETE /api/agente/a3f1c2d4-e5b6-7890-abcd-ef1234567890
```

**Response — 204 No Content** *(sem corpo)*

### Valores válidos para enums

**StatusAgente**
```
Ativo | Inativo | EmMissao | Aposentado
```

**StatusMissao**
```
Planejada | EmAndamento | Concluida | Cancelada | Suspensa
```
---

## Tecnologias

| Tecnologia | Versão |
|---|---|
| .NET / ASP.NET Core | 10.0 |
| Entity Framework Core | 10.0 |
| Oracle.EntityFrameworkCore | 10.23.26000 |
| Swashbuckle (Swagger) | 10.0.0 |
