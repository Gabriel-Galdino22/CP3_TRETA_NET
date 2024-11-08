using CP3.IoC;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Configuração da injeção de dependências
Bootstrap.Start(builder.Services, builder.Configuration);

// Adicionando controllers e serviços adicionais
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Obter a string de conexão para log
var connectionString = builder.Configuration.GetConnectionString("Oracle");
Console.WriteLine($"[INFO] String de conexão utilizada: {connectionString}");

var app = builder.Build();

// Teste de conexão com o banco de dados
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CP3.Data.AppData.ApplicationContext>();
    try
    {
        if (context.Database.CanConnect())
        {
            Console.WriteLine("[INFO] Conexão com o banco de dados estabelecida com sucesso.");
        }
        else
        {
            Console.WriteLine("[ERROR] Falha ao conectar ao banco de dados.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[ERROR] Erro ao conectar ao banco de dados: {ex.Message}");
    }
}

// Configuração do pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
