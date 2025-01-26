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

