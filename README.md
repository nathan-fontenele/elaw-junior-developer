# elaw Client API

Este é um projeto de API para gerenciamento de clientes, desenvolvido como parte do teste para desenvolvedor junior na elaw.

## Requisitos

- .NET 8.0 SDK
- Visual Studio 2022 ou VS Code

## Como executar o projeto

### Usando Visual Studio
1. Abra a solução `elaw-desenvolvedor-junior.sln`
2. Certifique-se de que o projeto `elaw-desenvolvedor-junior` está definido como projeto de inicialização
3. Pressione F5 ou clique em "Iniciar" para executar o projeto
4. O Swagger UI será aberto automaticamente no navegador (https://localhost:5001/swagger)

### Usando linha de comando
1. Navegue até a pasta do projeto `elaw-desenvolvedor-junior`
2. Execute os seguintes comandos:
```
dotnet restore
dotnet build
dotnet run
```
3. Acesse a API através de https://localhost:5001 ou http://localhost:5000

## Endpoints da API

A API oferece os seguintes endpoints para gerenciamento de clientes:

### GET /api/clients
Retorna todos os clientes cadastrados.

**Resposta de sucesso (200 OK):**
```json
[
  {
    "id": "guid",
    "name": "Nome do Cliente",
    "email": "email@exemplo.com",
    "phone": "123456789",
    "address": {
      "street": "Rua Exemplo",
      "number": "123",
      "city": "Cidade",
      "state": "UF",
      "zipCode": "12345-678"
    }
  }
]
```

### GET /api/clients/{id}
Retorna um cliente específico pelo ID.

**Resposta de sucesso (200 OK):**
```json
{
  "id": "guid",
  "name": "Nome do Cliente",
  "email": "email@exemplo.com",
  "phone": "123456789",
  "address": {
    "street": "Rua Exemplo",
    "number": "123",
    "city": "Cidade",
    "state": "UF",
    "zipCode": "12345-678"
  }
}
```

### POST /api/clients
Cria um novo cliente.

**Requisição:**
```json
{
  "name": "Nome do Cliente",
  "email": "email@exemplo.com",
  "phone": "123456789",
  "address": {
    "street": "Rua Exemplo",
    "number": "123",
    "city": "Cidade",
    "state": "UF",
    "zipCode": "12345-678"
  }
}
```

**Resposta de sucesso (201 Created):**
```json
{
  "message": "Created successfully",
  "data": {
    "id": "guid",
    "name": "Nome do Cliente",
    "email": "email@exemplo.com",
    "phone": "123456789",
    "address": {
      "street": "Rua Exemplo",
      "number": "123",
      "city": "Cidade",
      "state": "UF",
      "zipCode": "12345-678"
    }
  }
}
```

### PUT /api/clients/{id}
Atualiza um cliente existente.

**Requisição:**
```json
{
  "name": "Nome Atualizado",
  "email": "email.atualizado@exemplo.com",
  "phone": "987654321",
  "address": {
    "street": "Nova Rua",
    "number": "456",
    "city": "Nova Cidade",
    "state": "UF",
    "zipCode": "98765-432"
  }
}
```

**Resposta de sucesso (200 OK):**
```json
{
  "message": "User updated successfully",
  "data": {
    "id": "guid",
    "name": "Nome Atualizado",
    "email": "email.atualizado@exemplo.com",
    "phone": "987654321",
    "address": {
      "street": "Nova Rua",
      "number": "456",
      "city": "Nova Cidade",
      "state": "UF",
      "zipCode": "98765-432"
    }
  }
}
```

### DELETE /api/clients/{id}
Remove um cliente existente.

**Resposta de sucesso (200 OK):**
```json
{
  "message": "User deleted successfully",
  "data": {
    "id": "guid",
    "name": "Nome do Cliente",
    "email": "email@exemplo.com",
    "phone": "123456789",
    "address": {
      "street": "Rua Exemplo",
      "number": "123",
      "city": "Cidade",
      "state": "UF",
      "zipCode": "12345-678"
    }
  }
}
```

## Executando os testes

O projeto inclui testes de unidade para validar o funcionamento dos endpoints da API.

### Usando Visual Studio
1. Abra o Test Explorer (Teste > Test Explorer)
2. Clique em "Executar todos os testes" ou selecione testes específicos para executar

### Usando linha de comando
1. Navegue até a pasta do projeto de teste `elaw-desenvolvedor-junior-test`
2. Execute o seguinte comando:
```
dotnet test
```

## Tecnologias utilizadas

- ASP.NET Core 8.0
- Entity Framework Core (In-Memory Database)
- AutoMapper
- xUnit (para testes)
- Moq (para mocks nos testes)
- Swagger/OpenAPI 