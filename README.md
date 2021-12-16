# AttributedInjection
Fornece injeção de dependência por atributo para projetos NetCore.

### O que você vai encontrar aqui:
  - Instalação
  - Como usar
  - Exemplo

## Instalação

Este projeto requer:
  - Instalar o NuGet conforme indicado na [WiKi](https://wiki.cappta.com.br/pt-br/Tecnologia/GitHub/Packages/Nuget)

## Como usar
### Registrar AttributedInjection:
No arquivo `Startup.cs` adicione a diretiva `using` para o pacote e no método `ConfigureServices` adicione o serviço.

```c#
using AttributedInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public class Startup
	{
		private readonly IConfiguration configuration;

		public Startup(IConfiguration configuration)
			=> this.configuration = configuration;

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAttributedInjections();
		}
	}
}
```

### Adicionar a forma de injeção ao serviço:
Adicione a diretiva `using` para o pacote na classe e configure qual será a forma de injeção de dependência no serviço através do uso dos atributos [`[Singleton]`, `[Scoped]` ou `[Transient]`](https://docs.microsoft.com/pt-br/dotnet/core/extensions/dependency-injection).

```c#
using AttributedInjection;

namespace Parselmouth.Commands.Pair {
	public interface IFormatPinKey {
		string Execute(int pin);
	}

	[Singleton]
	public class FormatPinKey : IFormatPinKey {
		public string Execute(int pin)
			=> $"PIN-{pin}";
	}
}
```

## Exemplo
<details> <summary>Veja aqui um exemplo de uso</summary>

```c#
using AttributedInjection;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Parselmouth.Commands.Pair {
	public interface IGetDeterministicHashCode {
		int Execute(string value);
	}

	[Singleton]
	public class GetDeterministicHashCode : IGetDeterministicHashCode {
		public int Execute(string value) {
			var md5 = MD5.Create();

			var utf8Value = Encoding.UTF8.GetBytes(value);
			var hash = md5.ComputeHash(utf8Value);

			var hashCode = BitConverter.ToInt32(hash, 0);

			return hashCode;
		}
	}
}
```

</details>
