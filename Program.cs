using BlogWeb.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration().ConfigureJwt();
builder.Services.ConfigureControllers().ConfigureServices();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();