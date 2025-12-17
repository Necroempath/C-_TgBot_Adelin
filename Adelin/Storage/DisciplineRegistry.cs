using Adelin.Realizations;

public sealed class DisciplineRegistry
{
    public IReadOnlyCollection<DisciplineStat> All { get; }

    public DisciplineRegistry()
    {
        All = new[]
        {
            new DisciplineStat
            {
                Key = "dominate",
                Aliases = ["доминирование","dominate"]
            },
            new DisciplineStat
            {
                Key = "thaumaturgy",
                Aliases = ["тауматургия","thaumaturgy"]
            },
            new DisciplineStat
            {
                Key = "auspex",
                Aliases = ["прорицание","auspex"]
            }
        };
    }
}