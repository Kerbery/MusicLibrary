using Common.MassTransit;
using Common.MongoDB;
using Microsoft.IdentityModel.Tokens;
using PlaylistService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMongo()
    .AddMongoRepository<Playlist>("playlists")
    .AddMongoRepository<PlaylistItem>("playlistItems")
    .AddMongoRepository<Track>("tracks")
    .AddMasstransitRabbitMq();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://identityservice:80";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //Dangerous territory
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
