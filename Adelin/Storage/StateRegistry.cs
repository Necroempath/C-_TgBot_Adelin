using Adelin.Realizations;

namespace Adelin.Storage;

public sealed class StateRegistry
{
    public IReadOnlyCollection<StateStat> All { get; }

    public StateRegistry()
    {
        All = new[]
        {
            new StateStat
            {
                Key = "willpower",
                Aliases = ["воля", "willpower", "wp"],
                Selector = s => s.WillpowerPool
            },
            new StateStat
            {
                Key = "humanity",
                Aliases = ["человечность","humanity"],
                Selector = s => s.Humanity
            },
            new StateStat
            {
                Key = "bloodpool",
                Aliases = ["кровь", "bloodpool", "bp"],
                Selector = s => s.Bloodpool
            }
            
        };
    }
}
