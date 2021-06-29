using Microsoft.Extensions.DependencyInjection;

namespace AttributedInjection {
	public class ScopedAttribute : BaseInjectionAttribute {
		public override ServiceLifetime ServiceLifetime { get; } = ServiceLifetime.Scoped;
	}
}
