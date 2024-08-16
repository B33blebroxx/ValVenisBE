using ValVenisBE;
using ValVenisBE.Controllers;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();
builder.Services.AddNpgsql<ValVenisBEDbContext>(builder.Configuration["ValVenisBEDbConnectionString"]);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5003")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
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

app.UseAuthorization();

app.MapControllers();

UserApi.Map(app);
SupportOrgApi.Map(app);
QuoteApi.Map(app);
LogoApi.Map(app);
AboutMeApi.Map(app);
MissionStatementApi.Map(app);
SupportPageApi.Map(app);
QuotePageApi.Map(app);

app.Run();
