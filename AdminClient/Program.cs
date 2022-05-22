var builder = WebApplication.CreateBuilder(args);


builder.ConfigureAdminClientServices();
var app = builder.Build();

app.ConfigurePipeline();



app.Run();

