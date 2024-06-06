// using MongoDB.Bson;
// using MongoDB.Driver;
using TodoApi2.Data;
using TodoApi2.Serializers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<BsonStringNumericSerializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// var connectionString = "mongodb://localhost:27017";

// var client = new MongoClient(connectionString);
// var collection = client.GetDatabase("portal").GetCollection<BsonDocument>("users");
// var filter = Builders<BsonDocument>.Filter.Eq("Id", 1);
// // var document = collection.Find(filter).First();
// var documents = collection.Find(new BsonDocument()).ToList();
// foreach (var document in documents)
// {
//     Console.WriteLine(document);
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");

app.Run();
