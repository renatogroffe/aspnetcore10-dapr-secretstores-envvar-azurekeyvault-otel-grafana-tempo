$env:APITestSecretsDapr_Mensagem = "Secret vindo de uma variável de ambiente! Foi numa live do Canal .NET..."
dapr run --app-id APITestSecretsDapr --components-path ..\components dotnet run