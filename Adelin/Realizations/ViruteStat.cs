using Adelin.Abstractions;

namespace Adelin.Realizations;

public sealed class VirtueStat : IRollStat
{
    public string Key { get; init; } = null!;
    public IReadOnlyList<string> Aliases { get; init; } = [];
    public Func<Virtues, int> Selector { get; init; } = null!;
    public StatCategory Category => StatCategory.Virtue;

    public int GetValue(Charsheet sheet)
        => Selector(sheet.Virtues);
}