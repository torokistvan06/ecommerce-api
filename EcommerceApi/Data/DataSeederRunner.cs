using EcommerceApi.Attributes;

namespace EcommerceApi.Data;

public class DataSeederRunner
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, List<Type>> _graph = new();

    public DataSeederRunner(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var seeders = _serviceProvider
            .GetServices<ISeeder>()
            .ToList();

        var seederTypes = seeders.Select(s => s.GetType()).ToList();

        foreach (var seederType in seederTypes)
        {
            if (!_graph.ContainsKey(seederType))
                _graph[seederType] = new List<Type>();

            var dependsOn = seederType.GetCustomAttributes(typeof(DependsOnAttribute), true)
                .Cast<DependsOnAttribute>()
                .Select(attr => attr.DependedSeeder);

            foreach (var dep in dependsOn)
            {
                if (!_graph.ContainsKey(dep))
                    _graph[dep] = [];

                _graph[seederType].Add(dep);
            }
        }

        var sortedTypes = TopologicalSort(_graph);

        foreach (var type in sortedTypes)
        {
            var instance = seeders.First(s => s.GetType() == type);
            await instance.SeedAsync(cancellationToken: cancellationToken);
        }
    }

    private static List<Type> TopologicalSort(Dictionary<Type, List<Type>> graph)
    {
        var sorted = new List<Type>();
        var visited = new HashSet<Type>();
        var temp = new HashSet<Type>();

        void Visit(Type type)
        {
            if (temp.Contains(type))
                throw new Exception($"Cyclic dependency detected on seeder: {type.Name}");

            if (!visited.Contains(type))
            {
                temp.Add(type);
                foreach (var dep in graph[type])
                    Visit(dep);
                temp.Remove(type);
                visited.Add(type);
                sorted.Add(type);
            }
        }

        foreach (var node in graph.Keys)
            Visit(node);

        return sorted;
    }
}
