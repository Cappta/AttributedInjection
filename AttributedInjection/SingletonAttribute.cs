using Microsoft.Extensions.DependencyInjection;

namespace AttributedInjection {
	public class SingletonAttribute : BaseInjectionAttribute {
		public override ServiceLifetime ServiceLifetime { get; } = ServiceLifetime.Singleton;
	}
}
