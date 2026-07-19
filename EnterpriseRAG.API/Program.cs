
using EnterpriseRAG.Application.Chat.Interfaces;
using EnterpriseRAG.Application.Chat.Services;
using EnterpriseRAG.Application.Qdrant;
using EnterpriseRAG.Application.Retrieval.Interfaces;
using EnterpriseRAG.Application.Retrieval.Servoces;
using EnterpriseRAG.Infrastructure;
using EnterpriseRAG.Infrastructure.Configuration;
using EnterpriseRAG.Infrastructure.LLM;

var builder = WebApplication.CreateBuilder(args);

//ap.net core 
builder.Services.AddControllers(); //api
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// registring/connecting  option classes and sections
builder.Services.Configure<MiniLMOptions>(
    builder.Configuration.GetSection(MiniLMOptions.SectionName));
builder.Services.Configure<QdrantOptions>(
    builder.Configuration.GetSection(QdrantOptions.SectionName));
builder.Services.Configure<OllamaOptions>(
    builder.Configuration.GetSection(OllamaOptions.SectionName));
builder.Services.Configure<ChunkOptions>(builder.Configuration.GetSection(ChunkOptions.SectionName));


//registering all infra services. 
builder.Services.AddInfrastructure();
builder.Services.AddScoped<IRetrievalService, RetrievalService>();
builder.Services.AddHttpClient<ILLMService, OllamaService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Ollama:BaseUrl"]);
    client.Timeout = TimeSpan.FromMinutes(10);
});
builder.Services.AddScoped<PromptBuilder>();
builder.Services.AddScoped<IChatService, ChatService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider.GetRequiredService<IQdrantService>();
    await service.CreateCollectionAsync();
}

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
