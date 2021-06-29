using AttributeSdk;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AttributedInjection {
	public static class IServiceCollectionExtensions {
		private static IServiceCollection IdempotentAdd(this IServiceCollection services, ServiceDescriptor serviceDescriptor) {
			if(services.Any(sd =>
				sd.ServiceType == serviceDescriptor.ServiceType &&
				sd.ImplementationInstance == serviceDescriptor.ImplementationInstance &&
				sd.ImplementationType == serviceDescriptor.ImplementationType &&
				sd.ImplementationFactory == serviceDescriptor.ImplementationFactory
				) == false
			) {
				services.Add(serviceDescriptor);
			}
			return services;
		}

		public static IServiceCollection AddAttributedInjections(this IServiceCollection services) {
			foreach(var (type, injectAttributes) in AppDomain.CurrentDomain.EnumerateTypesWithAttributes<BaseInjectionAttribute>()) {
				foreach(var injectAttribute in injectAttributes) {
					var serviceType = injectAttribute.ServiceType;
					if(serviceType is null) {
						var assumedType = type.GetInterfaces().FirstOrDefault() ?? type;
						if(assumedType.IsGenericType) { assumedType = assumedType.GetGenericTypeDefinition(); }
						serviceType = assumedType;
					}
					services.IdempotentAdd(new ServiceDescriptor(serviceType, type, injectAttribute.ServiceLifetime));
				}
			}
			return services;
		}

		public static IServiceCollection AddAttributedInjectionsFromAssemblyContaining<T>(this IServiceCollection services) {
			foreach(var (type, injectAttributes) in typeof(T).Assembly.EnumerateTypesWithAttributes<BaseInjectionAttribute>()) {
				foreach(var injectAttribute in injectAttributes) {
					var serviceType = injectAttribute.ServiceType;
					if(serviceType is null) {
						var assumedType = type.GetInterfaces().FirstOrDefault() ?? type;
						if(assumedType.IsGenericType) { assumedType = assumedType.GetGenericTypeDefinition(); }
						serviceType = assumedType;
					}
					services.IdempotentAdd(new ServiceDescriptor(serviceType, type, injectAttribute.ServiceLifetime));
				}
			}
			return services;
		}
	}
}
