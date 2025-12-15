using Adelin.Realizations;

public sealed class AttributeRegistry
{
    private readonly Dictionary<string, AttributeStat> _lookup;

    public IReadOnlyCollection<AttributeStat> All => _lookup.Values.Distinct().ToList();

    public AttributeRegistry()
    {
        var list = new[]
        {
            new AttributeStat
            {
                Key = "strength",
                Aliases = ["сила", "strength"],
                Selector = a => a.Strength
            },
            new AttributeStat
            {
                Key = "dexterity",
                Aliases = ["ловкость", "dexterity"],
                Selector = a => a.Dexterity
            },
            new AttributeStat
            {
                Key = "stamina",
                Aliases = ["выносливость", "stamina"],
                Selector = a => a.Stamina
            },
            new AttributeStat
            {
                Key = "charisma",
                Aliases = ["обаяние", "харизма", "charisma"],
                Selector = a => a.Charisma
            },
            new AttributeStat
            {
                Key = "manipulation",
                Aliases = ["манипулирование", "манипуляция", "manipulation"],
                Selector = a => a.Manipulation
            },
            new AttributeStat
            {
                Key = "appearance",
                Aliases = ["привлекательность", "внешность", "appearance"],
                Selector = a => a.Appearance
            },
            new AttributeStat
            {
                Key = "perception",
                Aliases = ["восприятие", "perception"],
                Selector = a => a.Perception
            },
            new AttributeStat
            {
                Key = "intelligence",
                Aliases = ["интеллект", "intelligence"],
                Selector = a => a.Intelligence
            },
            new AttributeStat
            {
                Key = "wits",
                Aliases = ["смекалка", "wits"],
                Selector = a => a.Wits
            }
        };


        _lookup = list
            .SelectMany(stat => stat.Aliases.Select(a => (alias: Normalize(a), stat)))
            .ToDictionary(x => x.alias, x => x.stat);
    }

    public AttributeStat? Find(string input)
    {
        var key = Normalize(input);

        if (!_lookup.TryGetValue(key, out var stat))
            return null;

        return stat;
    }

    private static string Normalize(string s)
        => s.Trim().ToLowerInvariant();
}