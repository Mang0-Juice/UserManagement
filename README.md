# 🚀 UserManagement API — ASP.NET Core 9.0

![.NET 9](https://img.shields.io/badge/.NET%209.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white)

API para gerenciamento de usuários com **ASP.NET Core 9.0**, **JWT**, **AutoMapper** e **SQLite**. Estrutura limpa, rotas objetivas e pronta para rodar localmente ou em container.

---

## ✨ Funcionalidades
- CRUD de usuários
- Autenticação via JWT (login + rotas protegidas)
- Mapeamento DTO ↔ Entidade com AutoMapper
- Swagger / OpenAPI habilitado

---

## 📦 Tecnologias & Pacotes
- ASP.NET Core 9.0
- Entity Framework Core 9.0.7 (Sqlite + Design)
- AutoMapper 15.0.1
- JWT Bearer 9.0.8
- Swashbuckle.AspNetCore 9.0.3

---

## 📂 Estrutura
/Controllers → Endpoints HTTP  
/Data → AppDbContext  
/Dtos → Objetos de transferência  
/Mapping → Perfis AutoMapper  
/Models → Entidades  
/Repositories → Acesso a dados  
/Services → Regras de negócio  
/Utils → Utilitários (JWT)  
Program.cs, appsettings.json, Dockerfile

---

## ⚙️ Pré-requisitos
- .NET 9.0 SDK
- Docker (opcional)

---
## 🔧 Configuração (exemplo `appsettings.Development.json`)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=./data/usermanagement.db"
  },
  "Jwt": {
    "Key": "um-segredo-muito-forte-aqui",
    "Issuer": "UserManagement",
    "Audience": "UserManagementUsers",
    "ExpiresInMinutes": 60
  },
  "Logging": { 
    "LogLevel": { 
      "Default": "Information" 
    } 
  }
}
```

Observação: `Jwt:Key` deve ser fornecida por segredo local (User Secrets) ou variável de ambiente em produção.

---

## ▶️ Execução local
```bash
 $dotnet restore  
 $dotnet ef migrations add InitialCreate  
 $dotnet ef database update  
 $dotnet run
```

Swagger: https://localhost:5001/swagger (ou conforme sua configuração)

---

## 🐳 Docker
Build:  

**Build**:
```bash
docker build -t usermanagement .
Run (persistindo SQLite e variáveis mínimas):

docker run -d --name usermanagement \
  -p 8080:8080 \
  -v $(pwd)/data:/app/data \
  -e ConnectionStrings__DefaultConnection="Data Source=/app/data/usermanagement.db" \
  -e JWT__Key="um-segredo-muito-forte-aqui" \
  usermanagement
```

Swagger: http://localhost:8080/swagger

---

## 🔑 Variáveis de ambiente
ConnectionStrings__DefaultConnection  
ASPNETCORE_ENVIRONMENT (Development|Production)  
JWT__Key  
JWT__Issuer (opcional)  
JWT__Audience (opcional)  
JWT__ExpiresInMinutes (opcional)

---

## 🔎 Endpoints principais
POST /api/auth/login → autenticar (email + senha)  
POST /api/user/register → registrar usuário  
GET /api/user/{id} → obter por id (autenticado)  
PUT /api/user/{id} → atualizar (autenticado)  
DELETE /api/user/{id} → remover (autenticado)

Modelos e exemplos completos disponíveis no Swagger.

---

## 📜 Licença
MIT
