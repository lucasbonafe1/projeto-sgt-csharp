# Sistema de Gerenciamento de Tarefas - API

A API oferece um conjunto completo de operações CRUD para gerenciar tarefas e usuários.

#### Relação entre Tarefas e Usuários

Cada tarefa está associada a um usuário específico através de uma relação de chave estrangeira.
Isso garante que somente o usuário que criou uma tarefa possa visualizá-la, atualizá-la ou excluí-la, e fortalece a segurança e a organização dos dados, garantindo que as operações de CRUD sejam realizadas de forma isolada e segura para cada conta de usuário.

## Tecnologias Utilizadas

 -  Domain-Driven Design (DDD)
 -  RabbitMQ
 -  Docker
 -  JWT Bearer
 -  PostgreSQL
 -  Dapper
 -  XUnit
 - .NET 8

## Estrutura do Projeto

    ├── SGT.API/                      # Projeto principal da API
    │   ├── Controllers                # Controladores que lidam com as requisições HTTP, definindo os endpoints da API. 
    │   ├── Program.cs                # Arquivo principal de configuração e inicialização da API
    │   ├── appsettings.json          # Configurações da aplicação
    ├── SGT.Core/                     # Projeto que é usado de maneira global para referenciar em outros projetos.
    │   ├── Exceptions                # Exceções de BadRequest e NotFound 
    ├── Application/              # Camada de aplicação contendo serviços e lógica de negócios
    │   ├── DTOs/                     # Objetos de Transferência de Dados (Data Transfer Objects)
    │   ├── Interfaces/               # Interfaces para repositórios e serviços
    │   ├── Services/               # Lógica do projeto
    ├── Domain/              # Camada que representa as regras de negócio e os modelos do domínio. 
    │   ├── Entities/                 # Entidades que representam os modelos de dados
    │   ├── Enum/                # Onde armazena o StatusEnum 
    │   ├── Repositories/             # Interface dos repositórios
    ├── Infrastructure/              # Camada de infraestrutura que lida com a comunicação com bancos de dados e outros serviços externos. 
    │   ├── Database/                 # Config do Dapper
    │   ├── Messaging/                # Configuração do RabbitMQ (Config, Producers, Consumers)
    │   ├── Middlewares/                # Pasta da GlobalExceptionHandler
    │   ├── Repositories/             # Implementações dos repositórios de dados
    │   ├── Security/             # Configuração do JWT
    ├── SGT.Tests/                    # Projeto de testes automatizados XUnit
    │   ├── ControllerTests/          # Testes para os Controllers da API
    │   ├── MessagingTests/           # Testes para a camada de Messaging
    
