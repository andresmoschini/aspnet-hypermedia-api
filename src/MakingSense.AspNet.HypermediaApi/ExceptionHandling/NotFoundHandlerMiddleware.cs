﻿using MakingSense.AspNet.HypermediaApi.Problems;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using System;
using System.Threading.Tasks;

namespace MakingSense.AspNet.HypermediaApi.ExceptionHandling
{
	/// <summary>
	/// This middleware capture all request an throws a NotFound exception.
	/// </summary>
	public class NotFoundHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public NotFoundHandlerMiddleware(RequestDelegate next, ILogger<NotFoundHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public Task Invoke(HttpContext context)
		{
			throw new ApiException(new RouteNotFoundProblem());
		}
	}
}
