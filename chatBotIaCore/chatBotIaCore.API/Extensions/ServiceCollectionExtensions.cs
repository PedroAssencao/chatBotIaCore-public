using System.Reflection;

namespace chatBotIaCore.API.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registra automaticamente implementações concretas de tipos genéricos abertos
    /// (ex: RepositoryBase<,,,> ou ServiceBase<,,,,>),
    /// incluindo classes que herdam desses tipos, sem exigir registro manual.
    /// </summary>
    public static IServiceCollection AddClosedGenerics(
        this IServiceCollection services,
        Type openGenericType,
        ServiceLifetime lifetime,
        params Assembly[] assemblies)
    {
        var implementations = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                !t.IsGenericTypeDefinition &&
                (ImplementsOpenGenericInterface(t, openGenericType) ||
                 InheritsFromOpenGenericBase(t, openGenericType)))
            .ToList();

        foreach (var impl in implementations)
        {
            // 🔹 Interfaces genéricas compatíveis (ex: IRepositoryBase<,,,>)
            var genericIfaces = impl.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            i.GetGenericTypeDefinition() == openGenericType);

            if (genericIfaces.Any())
            {
                foreach (var iface in genericIfaces)
                    services.Add(new ServiceDescriptor(iface, impl, lifetime));
            }
            else
            {
                // 🔹 Se não houver interface, registra a própria classe concreta
                services.Add(new ServiceDescriptor(impl, impl, lifetime));
            }

            // 🔹 Interfaces não genéricas adicionais (ex: IMedicoService)
            var otherIfaces = impl.GetInterfaces()
                .Where(i => !i.IsGenericType);
            foreach (var iface in otherIfaces)
                services.Add(new ServiceDescriptor(iface, impl, lifetime));
        }

        return services;
    }

    // ✅ Verifica se implementa uma interface genérica aberta específica
    private static bool ImplementsOpenGenericInterface(Type type, Type openGenericType)
        => type.GetInterfaces().Any(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition() == openGenericType);

    // ✅ Verifica se herda de uma classe base genérica aberta específica
    private static bool InheritsFromOpenGenericBase(Type type, Type openGenericType)
    {
        var baseType = type.BaseType;
        while (baseType != null)
        {
            if (baseType.IsGenericType &&
                baseType.GetGenericTypeDefinition() == openGenericType)
                return true;

            baseType = baseType.BaseType;
        }
        return false;
    }
}