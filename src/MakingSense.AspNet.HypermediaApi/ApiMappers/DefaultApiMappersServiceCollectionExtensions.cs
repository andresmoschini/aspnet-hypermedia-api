using System;
using Microsoft.Framework.Internal;
using System.Linq;
using System.Reflection;
using MakingSense.AspNet.HypermediaApi.ApiMappers;

namespace Microsoft.Framework.DependencyInjection
{
	public static class DefaultApiMappersServiceCollectionExtensions
	{
		/// <summary>
		/// Search and register all types that implements an interface based on `IApiMapper<,>` in specified assembly
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddApiMappers([NotNull] this IServiceCollection services, Assembly mappersAssembly)
		{
			var potentialServiceTypes = mappersAssembly.GetTypes()
				.Select(x => new
				{
					type = x,
					info = x.GetTypeInfo()
				})
				.Where(x => !x.info.IsAbstract
					&& !x.info.IsInterface
					&& x.info.IsPublic
					&& x.info.Namespace != null)
				.Select(x => x.type);

			foreach (var type in potentialServiceTypes)
			{
				var serviceInterfaces = type.GetInterfaces()
					.Select(x => new
					{
						type = x,
						info = x.GetTypeInfo()
					})
					.Where(x => x.info.IsGenericType && x.info.GetGenericTypeDefinition() == typeof(IApiMapper<,>))
					.Select(x => x.type);

				foreach (var service in serviceInterfaces)
				{
					services.AddScoped(service, type);
				}
			}

			return services;
		}

#if DNX451
		/// <summary>
		/// Search and register all types that implements an interface based on `IApiMapper<,>` in caller assembly
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddApiMappers([NotNull] this IServiceCollection services)
		{
			return services.AddApiMappers(Assembly.GetCallingAssembly());
		}
#endif
	}
}
