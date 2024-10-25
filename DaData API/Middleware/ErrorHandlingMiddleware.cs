using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace DaData_API.Middleware
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ErrorHandlingMiddleware> _logger;
		public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch(Exception ex)
			{
				_logger.LogCritical(ex, ex.Message);
				await HandleExceptionAsync(context);
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context)
		{

			var problemDetails = new ProblemDetails
			{
				Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
				Title = "Internal server error",
				Status = (int)HttpStatusCode.InternalServerError
			};

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			string json = JsonSerializer.Serialize(problemDetails);

			await context.Response.WriteAsync(json);
		}
	}
}
