using EnterpriseRAG.Infrastructure.Configuration;
using EnterpriseRAG.Infrastructure;

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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
