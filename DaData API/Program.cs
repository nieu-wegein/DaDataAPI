using DaData_API.Middleware;
using DaData_API.Profiles;
using DaData_API.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services.AddControllers();
	builder.Services.AddHttpClient("Dadata");
	builder.Services.AddCors(options =>
	{
		options.AddPolicy("localhost", builder =>
		{
			builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
				   .WithMethods("GET", "POST")
				   .AllowAnyHeader();
		});
	});
	builder.Services.Configure<WebClientSettings>(
		builder.Configuration.GetSection("WebClientSettings"));
	builder.Services.AddTransient<IWebClient, WebClient>();
	builder.Services.AddAutoMapper(typeof(AdressInfoProfile));
	builder.Host.UseSerilog((context, config) =>
		config.ReadFrom.Configuration(context.Configuration));
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

}


var app = builder.Build();
{
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}
	app.UseHttpsRedirection();
	app.UseCors("localhost");
	app.UseAuthorization();
	app.UseSerilogRequestLogging(conf => conf.MessageTemplate = "{RequestMethod} {RequestPath} {StatusCode}");
	app.UseMiddleware<ErrorHandlingMiddleware>();
	app.MapControllers();
	app.Run();
}
