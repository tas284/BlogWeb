using BlogWeb.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration().ConfigureJwt();
builder.Services.ConfigureControllers().ConfigureServices();

var app = builder.Build();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCompression();
app.MapControllers();
app.Run();