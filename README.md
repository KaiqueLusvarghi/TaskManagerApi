# TaskManagerAPI

A **TaskManagerAPI** é uma aplicação Web API desenvolvida para gerenciar tarefas com funcionalidades de CRUD (Criar, Ler, Atualizar e Deletar). Ela permite o gerenciamento de tarefas com recursos de filtro, paginação e vinculação a categorias. A API foi construída utilizando **ASP.NET Core** com **Entity Framework Core** e usa **DTOs** para abstração e otimização das respostas.

## Funcionalidades

- **Cadastro de Tarefas**: Permite criar novas tarefas, associando a uma categoria existente.
- **Visualização de Tarefas**: Permite consultar tarefas com filtros (status, categoria) e paginação.
- **Edição de Tarefas**: Permite atualizar as informações de uma tarefa existente.
- **Exclusão de Tarefas**: Permite remover uma tarefa do sistema.
- **Gestão de Categorias**: Permite criar, atualizar, excluir e listar categorias para as tarefas.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework para construção da API.
- **Entity Framework Core**: ORM para persistência de dados em banco de dados.
- **SQL Server**: Banco de dados utilizado para armazenar os dados.
- **DTOs**: Data Transfer Objects para otimizar as respostas da API.

## Estrutura do Projeto

- **Models**: Contém as classes de modelo que representam as entidades da aplicação (Tarefa, Categoria, etc).
- **Controllers**: Define os endpoints da API, realizando as operações de CRUD.
- **DTOs**: Contém as classes de DTO (Data Transfer Object), usadas para abstração e transferência de dados entre a API e o cliente.
- **Data**: Contém o contexto do banco de dados (DbContext) e a configuração do banco de dados.

## Endpoints da API

### **Tarefas**

#### `GET /api/tarefas`
- Retorna todas as tarefas, com a possibilidade de aplicar filtros de status e categoria, além de suportar paginação.
  
  **Exemplo de filtro**:
  ```http
  GET /api/tarefas?pagina=1&tamanhoPagina=10&status=pendente&categoriaId=2


---
**GET /api/tarefas/{id}**:
- Retorna os detalhes de uma tarefa específica.

---
**POST /api/tarefas**
-- Cria uma nova tarefa.

**Exemplo de payload:**

 ```json

{
  "titulo": "Minha tarefa",
  "descricao": "Descrição da tarefa",
  "dataCriacao": "2025-01-01T00:00:00",
  "status": "Pendente",
  "categoriaId": 1
}
 ```
---
**PUT /api/tarefas/{id}**
Atualiza uma tarefa existente.

Exemplo de payload:

 ```json

{
  "id": 1,
  "titulo": "Minha tarefa atualizada",
  "descricao": "Descrição atualizada",
  "dataCriacao": "2025-01-01T00:00:00",
  "status": "EmAndamento",
  "categoriaId": 1
}
 ```
---

**DELETE /api/tarefas/{id}**
- Exclui uma tarefa pelo ID.

---
### **Categorias**

**GET /api/categorias**
- Retorna todas as categorias disponíveis.
---
**POST /api/categorias**
- Cria uma nova categoria.

Exemplo de payload:

```json

{
  "nome": "Nova Categoria"
}
```
---
**PUT /api/categorias/{id}**
- Atualiza uma categoria existente.
---
**DELETE /api/categorias/{id}**
- Exclui uma categoria pelo ID.
---
### **Como Rodar o Projeto**
Pré-requisitos
- .NET 6.0 ou superior
- SQL Server ou SQL Server Express

**Passos**
- Clone o repositório

Git bash

```
git clone https://github.com/seuusuario/TaskManagerAPI.git
```
---
- Navegue até a pasta do projeto

Git bash
```
cd TaskManagerAPI
```
- Restaure as dependências do projeto

Git bash
```
dotnet restore
```
- Configure a string de conexão no arquivo appsettings.json

**No arquivo appsettings.json, altere a string de conexão para o seu banco de dados SQL Server.**

json
```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TaskManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
###**Crie o banco de dados e aplique as migrações**

Git bash
```
dotnet ef database update
```
---
Inicie a aplicação

Git bash
```
dotnet run
```
---
### **API estará disponível em http://localhost:5000 por padrão.**
---
- Testando a API
Você pode testar os endpoints da API utilizando ferramentas como Postman ou Insomnia.
---
### **Exemplo de Teste com o Postman**

- GET /api/tarefas - Recupera todas as tarefas com paginação.
- POST /api/tarefas - Cria uma nova tarefa.
- PUT /api/tarefas/{id} - Atualiza uma tarefa existente.
- DELETE /api/tarefas/{id} - Deleta uma tarefa.
