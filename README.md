# aspnetcore10-dapr-secretstores-envvar-azurekeyvault-otel-grafana-tempo
Exemplo de implementação do uso de Secret Stores com Dapr em uma API REST criada com ASP.NET Core + .NET 10. Inclui uso de traces coletados com OpenTelemetry + Grafana Tempo, com a criação de um ambiente de testes via Docker Compose.

Secrets management overview - Dapr Docs: **https://docs.dapr.io/developing-applications/building-blocks/secrets/secrets-overview/**

## Testes

Teste utilizando o secret store do Dapr que se baseia em Azure Key Vault:

![Requisição - Azure Key Vault state store](img/azkeyvault-01.png)

Trace no Grafana Tempo envolvendo o uso do secret store do Dapr que se baseia em Azure Key Vault (via chamada gRPC):

![Trace - Azure Key Vault state store](img/azkeyvault-02.png)

Teste utilizando o secret store do Dapr que se baseia em variáveis de ambiente:

![Requisição - Env var state store](img/envvar-01.png)

Trace no Grafana Tempo envolvendo o uso do secret store do Dapr que se baseia em variáveis de ambiente (via chamada gRPC):

![Trace - Env var state store](img/envvar-02.png)