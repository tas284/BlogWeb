# BlogWeb 📝

**BlogWeb** é uma aplicação web de blog desenvolvida com ASP.NET Core. Este projeto foi criado com o objetivo de aprendizado e prática dos principais conceitos de desenvolvimento web com C#, incluindo o uso de Entity Framework Core, Razor Pages e boas práticas de arquitetura.

## 🚀 Tecnologias Utilizadas

- **ASP.NET Core**
- **Entity Framework Core**
- **C#**
- **SQL Server**
- **Razor Pages**

## 📁 Estrutura do Projeto

- `Controllers/` — Controladores que lidam com as requisições HTTP.
- `Data/` — Contexto do banco de dados e inicialização.
- `Extensions/` — Métodos de extensão para facilitar operações comuns.
- `Migrations/` — Migrações do Entity Framework.
- `Models/` — Modelos que representam as entidades do sistema.
- `Services/` — Serviços com lógica de negócio.
- `ViewModels/` — Modelos utilizados nas views.

## 🔧 Como Executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/tas284/BlogWeb.git
   ```

2. Navegue até o diretório:
   ```bash
   cd BlogWeb
   ```

3. Restaure as dependências:
   ```bash
   dotnet restore
   ```

4. Atualize o banco de dados:
   ```bash
   dotnet ef database update
   ```

5. Execute a aplicação:
   ```bash
   dotnet run
   ```

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
