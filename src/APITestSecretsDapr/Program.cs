using Dapr.Client;
using APITestSecretsDapr.Tracing;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Grafana.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDaprClient();

var resourceBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName: OpenTelemetryExtensions.ServiceName,
        serviceVersion: OpenTelemetryExtensions.ServiceVersion);
builder.Services.AddOpenTelemetry()
    .WithTracing((traceBuilder) =>
    {
        traceBuilder
            .AddSource(OpenTelemetryExtensions.ServiceName)
            .SetResourceBuilder(resourceBuilder)
            .AddAspNetCoreInstrumentation()
            .AddGrpcClientInstrumentation()
            .UseGrafana();
    });

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

// Middleware necessário na integração com Dapr
app.UseCloudEvents();

app.MapGet("/secret-from-envvar", async (DaprClient daprClient) =>
{
    const string key = "Mensagem";
    var secret = await daprClient.GetSecretAsync("localenvsecretstore", key);
    app.Logger.LogInformation($"Secret retornado pelo Dapr (Env Var): key = {key}, value = {secret[key]}");
    return secret;
})
.WithName("GetSecretFromEnvVar");


app.MapGet("/secret-from-azurekeyvault", async (DaprClient daprClient) =>
{
    const string key = "Mensagem-Dapr-Test";
    var secret = await daprClient.GetSecretAsync("azurekeyvaultsecretstore", key);
    app.Logger.LogInformation($"Secret retornado pelo Dapr (Azure Key Vault): key = {key}, value = {secret[key]}");
    return secret;
})
.WithName("GetSecretFromAzureKeyVault");

app.Run();