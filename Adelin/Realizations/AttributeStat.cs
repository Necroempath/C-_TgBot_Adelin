using Adelin.Abstractions;

namespace Adelin.Realizations;

public sealed class AttributeStat : IRollStat
{
    public string Key { get; init; } = null!;
    public IReadOnlyList<string> Aliases { get; init; } = [];
    public Func<Attributes, int> Selector { get; init; } = null!;
    public StatCategory Category => StatCategory.Attribute;

    public int GetValue(Charsheet sheet) => Selector(sheet.Attributes);
}


