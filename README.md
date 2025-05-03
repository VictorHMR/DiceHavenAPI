# 🎲 DiceHaven API

API desenvolvida em .NET para gerenciamento de fichas, personagens, campanhas de RPG, com autenticação e chat integrado.

## 🧰 Tecnologias Utilizadas

- **.NET 9**
- **Entity Framework Core**
- **SQLite** (banco de dados local)
- **Swagger (Swashbuckle)**
- **JWT Authentication**
- **ASP.NET Core Web API**

---

## 🚀 Funcionalidades

- 📘 CRUD completo de **Fichas**
- 🧙 Cadastro e gerenciamento de **Personagens**
- 🏰 Criação e administração de **Campanhas**
- 🔐 Sistema de **Login/Autenticação via JWT**
- 📄 Documentação automática com **Swagger UI**

---

## ⚙️ Como Executar

### Pré-requisitos

- [.NET SDK 9 ou superior](https://dotnet.microsoft.com/download)
- Visual Studio, VS Code ou CLI

### Passos:

```bash
git clone https://github.com/VictorHMR/DiceHavenAPI.git
cd DiceGavenAPI
dotnet restore
dotnet ef database update
dotnet run
```

---

## 📑 Endpoints

Acesse a documentação completa dos endpoints no Swagger:

> http://localhost:5000/swagger  
> ou  
> http://localhost:port/swagger

---

## 🔒 Autenticação

A autenticação é feita via **JWT**. Ao realizar login, a API retorna um token que deve ser enviado em chamadas autenticadas usando o cabeçalho:

```http
Authorization: Bearer {seu_token}
```

## 🛡️ Segurança

- Autenticação com JWT
- Senhas armazenadas com hash
- Controle de acesso por usuário

---

