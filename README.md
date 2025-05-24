# 🎲 DiceHaven API

API desenvolvida em .NET para gerenciamento de fichas, personagens, campanhas de RPG, com autenticação e chat integrado.

## Tecnologias Utilizadas

- **.NET 9**
- **Entity Framework Core**
- **SQLite** (banco de dados local)
- **Swagger (Swashbuckle)**
- **JWT Authentication**
- **ASP.NET Core Web API**
- **SignalR**

---

## Funcionalidades

- Documentação automática com **Swagger UI**
- Sistema de **Login/Autenticação via JWT**
- Gerenciamento de **Campanhas**
- Personalização completa do modelo de modelo de **Ficha**
- Gerenciamento de **Fichas** e **Personagens**
- Lista de contatos
- Chat em tempo real
- Rolagem de dados com exmpressões matemáticas.

---

## ⚙️ Como Executar

### Pré-requisitos

- [.NET SDK 9 ou superior](https://dotnet.microsoft.com/download)
- Visual Studio, VS Code ou CLI
- Variaveis de ambiente configurados como no exemplo do projeto.
  
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

