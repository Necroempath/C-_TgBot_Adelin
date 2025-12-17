using Adelin.Abstractions;

namespace Adelin.Realizations;

public sealed class StateStat : IRollStat
{
    public string Key { get; init; } = null!;
    public IReadOnlyList<string> Aliases { get; init; } = [];
    public Func<State, int> Selector { get; init; } = null!;
    public StatCategory Category => StatCategory.State;

    public int GetValue(Charsheet sheet)
        => Selector(sheet.State);
}