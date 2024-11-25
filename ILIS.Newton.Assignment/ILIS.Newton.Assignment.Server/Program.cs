using ILIS.Newton.Assignment.API.Middlewares;
using ILIS.Newton.Assignment.Infrastructure;
using ILIS.Newton.Assignment.Application;
using ILIS.Newton.Assignment.API.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod(); 
    });
});


builder.Services.AddControllers().AddNewtonsoftJson(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.DocumentFilter<JsonPatchDocumentFilter>());

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAll");

app.UseDefaultFiles();
app.UseStaticFiles();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
