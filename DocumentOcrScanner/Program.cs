using DocumentOcrScanner.Data.Infra;
using DocumentOcrScanner.Services;
using Google.Cloud.Firestore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

FirebaseSettings firebaseSettings = new FirebaseSettings();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDocumentInfoReaderService, DocumentInfoReaderService>();

builder.Services.AddSingleton(_ => new FirestoreProvider(
    new FirestoreDbBuilder
    {
        ProjectId = firebaseSettings.ProjectId,
        JsonCredentials = JsonSerializer.Serialize(firebaseSettings)
    }.Build()
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
