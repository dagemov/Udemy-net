using WebApiAutores;

var builder = WebApplication.CreateBuilder(args);

//stratup
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);



// Configure the HTTP request pipeline.
app.Run();
