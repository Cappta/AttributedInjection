using Microsoft.Extensions.DependencyInjection;

namespace AttributedInjection;

public class TranscientAttribute : BaseInjectionAttribute {
	public override ServiceLifetime ServiceLifetime { get; } = ServiceLifetime.Transient;
}
