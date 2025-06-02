# 🎮 FIAP Cloud Games - Fase 1

API RESTful desenvolvida em .NET 8 para cadastro e gerenciamento de usuários e seus jogos adquiridos. Este projeto faz parte do Tech Challenge da FIAP e serve como base para futuras funcionalidades como matchmaking e gestão de servidores.

---

## 📌 Funcionalidades

- ✅ Cadastro de usuários com validação de e-mail e senha forte
- ✅ Autenticação via JWT com dois níveis de acesso: `User` e `Admin`
- ✅ Cadastro e gerenciamento de jogos (Admin)
- ✅ Adição e remoção de jogos na biblioteca do usuário autenticado
- ✅ Middleware para tratamento de erros e logs estruturados
- ✅ Documentação de API com Swagger
- ✅ Testes unitários com xUnit, Moq e FluentAssertions

---

## 🚀 Como executar o projeto

### 1. Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server LocalDB ou outro SQL compatível]
- [Visual Studio 2022+ ou VS Code]

### 2. Configuração

No terminal, navegue até a pasta do projeto e execute:

```bash
dotnet ef database update
dotnet run --project Tech.Api
```

A API será iniciada em: `https://localhost:5001`

---

## 🔐 Autenticação

Após cadastrar um usuário via endpoint `/api/Users`, gere o token com:

```
GET /api/Users/token?email=usuario@email.com&password=Senha@123
```

Use o token no Swagger clicando em **Authorize**.

---

## 🧪 Testes

Os testes estão no projeto `Tech.Tests`.

Para executá-los:

```bash
dotnet test
```

---

## 📘 Documentação da API

Acesse:

```
https://localhost:5001/swagger/index.html
```

---

## 🛠️ Stack utilizada

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- Serilog + ILogger
- xUnit + Moq + FluentAssertions

---

## 👥 Equipe

| Nome                | Discord              |
|---------------------|----------------------|
| Andre Saporito      | @andresaporito       |

---

## 📎 Links

- 📁 Repositório: [URL do GitHub]
- 🎥 Vídeo da Apresentação: [YouTube ou Google Drive]
- 🧩 Documentação DDD (Event Storming): [Link do Miro]
- 📄 Relatório: [PDF ou TXT]

---

## 📄 Licença

Projeto acadêmico - FIAP Tech Challenge 2024.
