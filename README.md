# ChatBotIaCore 🤖 - Backend de Chatbot Inteligente com IA

O **ChatBotIaCore** é uma solução de backend robusta para a criação e orquestração de **chatbots inteligentes**, desenvolvida em **.NET 8.0 (C#)**. O projeto utiliza uma **Clean Architecture** para garantir escalabilidade e manutenção, integrando-se nativamente com a **API do WhatsApp (Meta)** para mensageria e **Inteligência Artificial (OpenAI)** para processamento de linguagem natural, transcrição de áudio e execução de ferramentas (Tool Calling).

---

# 🎥 Demonstração

https://github.com/user-attachments/assets/523322d5-e1d9-4425-aee7-4f6a3bd8623f

---

## 🛠️ Instalação

Siga os passos abaixo para configurar e rodar o projeto.

### Pré-requisitos

Certifique-se de ter instalado:

* **SDK do .NET 8.0**
* **Docker & Docker Compose** (Recomendado para rodar o ambiente completo).
* **SQL Server** (Caso opte por rodar localmente sem Docker).

### 1. Configuração de Ambiente

1.  Clone o repositório e navegue até a pasta raiz.
2.  **Variáveis de Ambiente (.env):**
    * Crie um arquivo `.env` na raiz (baseado em `.env.example`).
    * Defina a senha do banco de dados: `SA_PASSWORD=SuaSenhaForte123!`.
3.  **Configurações da Aplicação (`appsettings.json`):**
    * Navegue até a pasta `chatBotIaCore.API`.
    * Renomeie ou copie `appsettings.example.json` para `appsettings.Development.json`.
    * Preencha as chaves:
        * `ConnectionStrings:Chinook`: String de conexão com o SQL Server.
        * `Meta`: Credenciais da API do WhatsApp (Token, Phone ID).
        * `IA`: Chave de API da OpenAI (ou compatível).

### 2. Executando com Docker (Recomendado)

O projeto já está configurado com orquestração de contêineres para a API e o Banco de Dados.

1.  Na raiz da solução, execute:
    ```bash
    docker-compose up -d --build
    ```
2.  A API estará disponível em: `http://localhost:8080/swagger/index.html` ou em `https://localhost:8081/swagger/index.html`.
3.  O SQL Server estará acessível na porta `1433`.

### 3. Executando Localmente (Manual)

1.  Certifique-se de ter um SQL Server rodando e atualize a `ConnectionString`.
2.  Aplique as migrações do banco de dados:
    ```bash
    cd chatBotIaCore.API
    dotnet ef database update --project ../chatBotIaCore.Infra
    ```
3.  Execute a API:
    ```bash
    dotnet run
    ```

    
### 🌎 3. Configurando o Webhook do Meta (WhatsApp)

1. Com a API **rodando**, inicie o **ngrok**:
   ```bash
   ngrok http 5058
   ```
2. Pegue a URL gerada (ex.: `https://f0a2ab243a9b.ngrok-free.app`).
3. Vá até **Meta for Developers** → Webhooks → Configure:

```
{URL_DO_NGROK}/api/v1/Meta/hook
```

Exemplo:
```
https://f0a2ab243a9b.ngrok-free.app/api/v1/Meta/hook
```

4. Na configuração do Webhook:
   - Ative **"messages"**
   - Use versão **v19.0 ou superior**

Se tudo estiver correto, o webhook será validado automaticamente e o bot ficará ativo com o fluxo padrão criado pelo SQL.

---


---

## 🚀 Uso

O sistema funciona como um backend de processamento de mensagens.

### Funcionalidades Principais

* **Webhook (`/api/v1/Meta/hook`)**: Endpoint principal para receber mensagens do WhatsApp. Ele processa texto, áudio e imagens, mantendo o contexto da conversa.
* **Swagger (`/swagger`)**: Interface para documentação e teste manual dos endpoints da API.
* **Orquestração de IA**: O sistema gerencia automaticamente o fluxo de conversa, decidindo quando responder diretamente, chamar uma ferramenta (Tool Calling) ou processar um arquivo.
* **Gerenciamento de Arquivos**: Downloads automáticos de mídias enviadas pelo usuário, com suporte a transcrição de áudio e extração de dados de imagens e documentos.

---

## 🎨 Estilo de Codificação

O projeto segue estritamente os princípios de **Clean Architecture** e **SOLID**, dividindo a solução em camadas com responsabilidades bem definidas:

### Backend (.NET)

* **Estrutura de Projetos (Solution):** A solução é segmentada para desacoplar o núcleo da aplicação de suas dependências externas.
* **Domain (`chatBotIaCore.Domain`)**: O coração do projeto. Contém as entidades (ex: `Message`, `BotConfiguration`, `Contact`) e Interfaces que definem as regras de negócio sem dependências externas.
* **Services (`chatBotIaCore.Services`)**: Contém a lógica de orquestração (ex: `ChatOrchestrationService.cs`), execução de ferramentas (`ToolExecutorService.cs`) e processamento de arquivos. Implementa os casos de uso da aplicação.
* **Infrastructure (`chatBotIaCore.Infra`)**: Implementa a persistência de dados utilizando **Entity Framework Core** e o padrão Repository (ex: `BaseRepository.cs`, `BotConfigurationRepository.cs`), além de gerenciar as migrações do banco.
* **Providers (`chatBotIaCore.Providers`)**: Camada responsável pela comunicação com APIs externas. Aqui ficam os clientes para **OpenAI** (`OpenAIProvider.cs`) e **Meta/WhatsApp** (`MessageProcessingWhatsappHandler.cs`), isolando a lógica de terceiros.
* **Storage (`chatBotIaCore.Storage`)**: Gerencia o armazenamento físico de arquivos e serviços de conversão (ex: PDF para Texto).
* **API (`chatBotIaCore.API`)**: Ponto de entrada da aplicação. Contém os `Controllers`, configuração de injeção de dependência (DI) e Webhooks.
