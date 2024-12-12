using Microsoft.EntityFrameworkCore;
using ReceitasLiterarias.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Prioriza a variável de ambiente DB_CONNECTION_STRING no ambiente de produção
var connectionString = builder.Configuration.GetValue<string>("DB_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    // Se a variável de ambiente não estiver definida, utiliza o appsettings.json
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

// Configura o DbContext com a string de conexão
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// Configura o Kestrel para escutar na porta 5000
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // Configura a aplicação para escutar na porta 5000
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllers();

// Aplicar as migrations ao banco de dados
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        Console.WriteLine("Aplicando migrations...");
        dbContext.Database.Migrate(); // Aplica migrations
        Console.WriteLine("Migrations aplicadas com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao aplicar migrations: {ex.Message}");
        throw;
    }
}

app.Run();
