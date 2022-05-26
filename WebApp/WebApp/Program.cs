using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Data;
using WebApp.Hubs;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<WebChatContext>();

builder.Services.AddSignalR();
// Add services to the container.

builder.Services.AddControllers();
// services
builder.Services.AddTransient<MessageService>();
builder.Services.AddTransient<ContactService>();
builder.Services.AddTransient<UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow All",
        builder =>
        {
            builder
            .SetIsOriginAllowed((o)=>true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow All");
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/Chat");

app.Run();