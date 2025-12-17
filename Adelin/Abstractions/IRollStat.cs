namespace Adelin.Abstractions;

public enum StatCategory
{
    Attribute,
    Ability,
    Discipline,
    Virtue,
    State
}

public interface IRollStat
{
    string Key { get; }
    IReadOnlyList<string> Aliases { get; }
    int GetValue(Charsheet sheet);
    StatCategory Category { get; }
}
