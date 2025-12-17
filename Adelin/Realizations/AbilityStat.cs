using Adelin.Abstractions;

namespace Adelin.Realizations;

public sealed class AbilityStat : IRollStat
{
    public string Key { get; init; } = null!;
    public IReadOnlyList<string> Aliases { get; init; } = [];
    public Func<Abilities, int> Selector { get; init; } = null!;
    public StatCategory Category => StatCategory.Ability;

    public int GetValue(Charsheet sheet) => Selector(sheet.Abilities);
}