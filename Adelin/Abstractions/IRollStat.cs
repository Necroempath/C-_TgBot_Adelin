namespace Adelin.Abstractions;

public interface IRollStat
{
    string Key { get; }
    IReadOnlyList<string> Aliases { get; } // все допустимые имена
    int GetValue(Charsheet sheet);
}
