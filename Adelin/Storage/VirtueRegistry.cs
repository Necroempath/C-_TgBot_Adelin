using Adelin.Realizations;

namespace Adelin.Storage;

public sealed class VirtueRegistry
{
    public IReadOnlyCollection<VirtueStat> All { get; }

    public VirtueRegistry()
    {
        All = new[]
        {
            new VirtueStat
            {
                Key = "conscience",
                Aliases = ["совесть","conscience"],
                Selector = v => v.Conscience
            },
            new VirtueStat
            {
                Key = "selfcontrol",
                Aliases = ["самоконтроль","self-control","selfcontrol"],
                Selector = v => v.SelfControl
            },
            new VirtueStat
            {
                Key = "courage",
                Aliases = ["смелость","courage"],
                Selector = v => v.Courage
            }
        };
    }
}
