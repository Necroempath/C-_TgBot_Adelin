using Adelin.Abstractions;

namespace Adelin.Realizations;

public sealed class DisciplineStat : IRollStat
{
    public string Key { get; init; } = null!;
    public IReadOnlyList<string> Aliases { get; init; } = [];
    public StatCategory Category => StatCategory.Discipline;

    public int GetValue(Charsheet sheet)
    {
        var d = sheet.Disciplines
            .FirstOrDefault(x => x.Name.Equals(Key, StringComparison.OrdinalIgnoreCase));

        return d?.Value ?? 0;
    }
}

