# Teste Técnico – Web API com .NET Core, EF In-Memory e DDD

## 🧩 Descrição do projeto

É uma Web API em ASP.NET Core para gerenciar um cadastro de clientes e seus respectivos endereços, estruturada conforme princípios do Domain-Driven Design (DDD) e utilizando Entity Framework Core com banco de dados em memória.

---

## 📚 Requisitos Funcionais

A API expõe os seguintes endpoints:

- **GET `/clientes`** – Listar todos os clientes
- **GET `/clientes/{id}`** – Obter um cliente pelo ID
- **POST `/clientes`** – Criar um novo cliente
- **PUT `/clientes/{id}`** – Atualizar um cliente existente
- **DELETE `/clientes/{id}`** – Remover um cliente

---

## 🧱 Requisitos Técnicos

- ASP.NET Core Web API (**.NET 8 ou superior**)
- **Entity Framework Core** (In-Memory Database)
- Estrutura DDD com separação em:
  - **Domain** (Entidades, Value Objects, Interfaces de Repositório)
  - **Application** (Serviços de aplicação, DTOs)
  - **Infrastructure** (Repositórios e banco)
  - **API** (Controllers, configuração)
- **Validações** (campos obrigatórios, e-mail único, etc.)
- **AutoMapper** para mapeamento entre DTOs e Entidades
- Testes unitários com **xUnit**
- Documentação com **Swagger**
- **Versionamento da API**

---

## ⚙️ Como rodar o projeto

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/JoaoXavierDEV/Elaw.git
   cd src/Elaw
   ```

2. **Restaure as dependências:**
   ```bash
   dotnet restore
   ```

3. **Rode a aplicação:**
   ```bash
   dotnet run --project XPTO.Presentation.API/XPTO.Presentation.API.csproj
   ```

4. **Acesse a API:**

   Por padrão, a API estará disponível em:  
   `https://localhost:7274` ou `http://localhost:5268`

5. **Acesse a documentação Swagger:**

   Normalmente disponível em:  
   `https://localhost:7274/swagger`  
   ou  
   `http://localhost:5268/swagger`

---

## 📁 Estrutura de Pastas 

```
src/
  Elaw.Domain/         # Entidades, Value Objects, Interfaces de Repositório
  Elaw.Application/    # Serviços de aplicação, DTOs
  Elaw.Infrastructure/ # Repositórios, contexto EF
  Elaw.API/            # Controllers, configuração de API
tests/
  Elaw.Tests/          # Testes unitários
```

---

## 📝 Observações

- O banco de dados é **em memória** — ao reiniciar o projeto, os dados serão perdidos.
- Para testes unitários, utilize o comando:
  ```bash
  dotnet test
  ```
- Caso deseje testar outros bancos, altere a configuração do `DbContext` na camada Infrastructure.

---

## 👨‍💻 Autor

[JoaoXavierDEV](https://github.com/JoaoXavierDEV)

---