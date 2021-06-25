using Microsoft.Extensions.DependencyInjection;
using System;

namespace AttributedInjection {
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public abstract class BaseInjectionAttribute : Attribute {
		public Type? ServiceType { get; set; }

		public abstract ServiceLifetime ServiceLifetime { get; }
	}
}