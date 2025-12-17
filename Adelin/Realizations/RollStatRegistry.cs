using Adelin.Abstractions;

namespace Adelin.Realizations;

public sealed class RollStatRegistry
{
    private readonly Dictionary<string, IRollStat> _lookup;

    public RollStatRegistry(IEnumerable<IRollStat> stats)
    {
        _lookup = stats
            .SelectMany(stat => stat.Aliases.Select(a => (alias: Normalize(a), stat)))
            .ToDictionary(x => x.alias, x => x.stat);
    }

    public IRollStat? Find(string input)
    {
        var key = Normalize(input);
        if (!_lookup.TryGetValue(key, out var stat))
            return null;
        return stat;
    }

    public IEnumerable<IRollStat> All => _lookup.Values.Distinct();

    public IEnumerable<IRollStat> ByCategory(StatCategory category)
        => All.Where(s => s.Category == category);

    private static string Normalize(string s) => s.Trim().ToLowerInvariant();
}