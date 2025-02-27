using MySimpleChat.Server;
using Microsoft.AspNetCore.SignalR.Client;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<CosmosDbService, CosmosDbService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7134", "https://localhost:5001", "http://localhost:5000") // Client-URL anpassen
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    //endpoints.MapRazorPages();
    endpoints.MapHub<ChatHub>("/chathub");
});

//builder.Services.AddSingleton(sp =>
//    new HubConnectionBuilder()        
//        .WithUrl("https://localhost:7134/"));


app.Run();
