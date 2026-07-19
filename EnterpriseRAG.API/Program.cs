
using EnterpriseRAG.Application.Chat.Interfaces;
using EnterpriseRAG.Application.Chat.Services;
using EnterpriseRAG.Application.Conversations.Interfaces;
using EnterpriseRAG.Application.Conversations.Services;
using EnterpriseRAG.Application.Qdrant;
using EnterpriseRAG.Application.Retrieval.Interfaces;
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
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IPromptBuilder,PromptBuilder>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IConversationService, ConversationService>();

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
