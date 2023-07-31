using Football_Fantasy.Urls;

//Definedation
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
var app = builder.Build();






app.MapGet("/", async context =>
{
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "HtmlFiles/index.html");
    var fileStream = File.OpenRead(filePath);
    await fileStream.CopyToAsync(context.Response.Body);
});
app.MapGet("/dashboard",async context =>
{
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "HtmlFiles/Dashboard.html");
    var fileStream = File.OpenRead(filePath);
    await fileStream.CopyToAsync(context.Response.Body);
});
app.MapGet("/news",async context =>
{
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "HtmlFiles/News.html");
    var fileStream = File.OpenRead(filePath);
    await fileStream.CopyToAsync(context.Response.Body);
});

SignUpApi.GetApis(app);
OtpApis.GetOtpApis(app);
LoginApis.GetApis(app);
CreateTeamApis.GetApi(app);



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run("http://localhost:3001");

