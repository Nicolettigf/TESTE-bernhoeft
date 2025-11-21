🧰 Tecnologias Utilizadas

	Backend (.NET 10 / C#)

	ASP.NET Core 10 (API REST)

	Entity Framework Core

	SQLite como banco local (padrão do projeto)

	FluentValidation para validação dos DTOs

	Injeção de dependência nativa do .NET

	Repository Pattern aplicado

	Service Layer contendo toda a regra de negócio

	Soft Delete na entidade Aviso

	DTOs específicos para Create / Update / Get

	Queryable exposto no repositório para consultas flexíveis

	Redis cache:
	O uso de Redis chegou a ser implementado e permanece comentado no código, não por necessidade técnica, 
	mas como estudo e demonstração de conhecimento, já que ele não fazia sentido real dentro do escopo deste projeto.


🎨 Frontend (Vue 3 / Vite)

	Vue 3 + Composition API

	Vite como bundler

	Vue Router (Home + AvisoView)

	Axios para integração com a API

	Componentização simples e direta

	Regras do desafio aplicadas no formulário:

	Título habilitado somente no CREATE

	Título bloqueado no UPDATE (Mensagem obrigatória nos dois fluxos)

	Telas implementadas:

	Listagem de avisos ativos

	Criação de aviso

	Edição (somente mensagem)

	Remoção (soft delete)

🏗 Arquitetura / Organização do Projeto

Estruturado em camadas:

📦 Entities

	Modelos (Aviso)

	DTOs (AvisoDto, GetAvisoDto)

📦 Repository

	AvisoRepository

	Métodos: Query(), AddAsync(), UpdateAsync()

📦 Service (Business Layer)

	Regras do desafio aplicadas

	Validações via FluentValidation

	Update permite alterar apenas a mensagem

	Soft Delete aplicado

📦 API/Web

	Endpoints REST completos

	CRUD implementado conforme regras

📦 Shared

	Interfaces para Service e Repository

	Facilita testes e futuras implementações

🧪 Testes Automatizados

	xUnit

	WebApplicationFactory (Microsoft.AspNetCore.Mvc.Testing)

	EF Core InMemory para testes isolados

	Fixtures para isolar ambiente de teste

	Testes cobrindo:

		GET por ID

		GET de avisos ativos

		POST (criação)

		PUT (alteração de mensagem)

		DELETE (soft delete)

🐳 Docker 

	O projeto inclui:

	Dockerfile Do backend

	Dockerfile do frontend (Vue 3)

	docker-compose.yml na raiz do projeto

	Ambos foram adicionados para fins de versionamento e estudo, não sendo obrigatoriamente utilizados para rodar a aplicação.

🔁 CI/CD (GitHub Actions)

O projeto inclui um pipeline básico configurado em:

.github/workflows/

O pipeline contempla:

	Build da API (.NET 10)

	Build da camada de testes

	Execução dos testes automatizados

	Validação de pull requests

	Pipeline preparado para extensões futuras (deploy, container publish etc.)