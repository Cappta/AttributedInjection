using AttributeSdk;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AttributedInjection {
	public static class IServiceCollectionExtensions {
		public static IServiceCollection AddAttributedInjections(this IServiceCollection services) {
			foreach(var (type, injectAttributes) in AppDomain.CurrentDomain.EnumerateTypesWithAttributes<BaseInjectionAttribute>()) {
				foreach(var injectAttribute in injectAttributes) {
					var serviceType = injectAttribute.ServiceType;
					if(serviceType is null) {
						var assumedType = type.GetInterfaces().FirstOrDefault() ?? type;
						if(assumedType.IsGenericType) { assumedType = assumedType.GetGenericTypeDefinition(); }
						serviceType = assumedType;
					}
					switch(injectAttribute) {
						case TranscientAttribute _:
							services.AddTransient(serviceType, type);
							break;
						case ScopedAttribute _:
							services.AddScoped(serviceType, type);
							break;
						case SingletonAttribute _:
							services.AddSingleton(serviceType, type);
							break;
						default:
							throw new NotImplementedException($"{injectAttribute.GetType()} has not been implemented");
					}
				}
			}
			return services;
		}
	}
}