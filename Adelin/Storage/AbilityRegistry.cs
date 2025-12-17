using Adelin.Realizations;

namespace Adelin.Storage;

public sealed class AbilityRegistry
{
    public IReadOnlyCollection<AbilityStat> All { get; }

    public AbilityRegistry()
    {
        All =
        [
            new AbilityStat { Key = "alertness", Aliases = new[] {"бдительность", "alertness"}, Selector = a => a.Alertness },
    new AbilityStat { Key = "athletics", Aliases = new[] {"атлетика", "athletics"}, Selector = a => a.Athletics },
    new AbilityStat { Key = "brawl", Aliases = new[] {"драка", "brawl"}, Selector = a => a.Brawl },
    new AbilityStat { Key = "empathy", Aliases = new[] {"эмпатия", "empathy"}, Selector = a => a.Empathy },
    new AbilityStat { Key = "expression", Aliases = new[] {"красноречие", "выражение", "expression"}, Selector = a => a.Expression },
    new AbilityStat { Key = "intimidation", Aliases = new[] {"запугивание", "intimidation"}, Selector = a => a.Intimidation },
    new AbilityStat { Key = "leadership", Aliases = new[] {"лидерство", "leadership"}, Selector = a => a.Leadership },
    new AbilityStat { Key = "streetwise", Aliases = new[] {"уличное чутье", "streetwise"}, Selector = a => a.Streetwise },
    new AbilityStat { Key = "subterfuge", Aliases = new[] {"обман", "subterfuge"}, Selector = a => a.Subterfuge },
    new AbilityStat { Key = "animalken", Aliases = new[] {"животные", "animalken"}, Selector = a => a.Animalken },
    new AbilityStat { Key = "crafts", Aliases = new[] {"ремесло", "crafts"}, Selector = a => a.Crafts },
    new AbilityStat { Key = "drive", Aliases = new[] {"вождение", "drive"}, Selector = a => a.Drive },
    new AbilityStat { Key = "etiquette", Aliases = new[] {"этикет", "etiquette"}, Selector = a => a.Etiquette },
    new AbilityStat { Key = "firearms", Aliases = new[] {"стрельба", "firearms"}, Selector = a => a.Firearms },
    new AbilityStat { Key = "melee", Aliases = new[] {"фехтование", "melee"}, Selector = a => a.Melee },
    new AbilityStat { Key = "performance", Aliases = new[] {"исполнение", "performance"}, Selector = a => a.Performance },
    new AbilityStat { Key = "stealth", Aliases = new[] {"скрытность", "stealth"}, Selector = a => a.Stealth },
    new AbilityStat { Key = "dodge", Aliases = new[] {"уклонение", "dodge"}, Selector = a => a.Dodge },
    new AbilityStat { Key = "survival", Aliases = new[] {"выживание", "survival"}, Selector = a => a.Survival },
    new AbilityStat { Key = "academics", Aliases = new[] {"гум науки", "academics"}, Selector = a => a.Academics },
    new AbilityStat { Key = "computer", Aliases = new[] {"информатика", "computer"}, Selector = a => a.Computer },
    new AbilityStat { Key = "finance", Aliases = new[] {"финансы", "finance"}, Selector = a => a.Finance },
    new AbilityStat { Key = "investigation", Aliases = new[] {"расследование", "investigation"}, Selector = a => a.Investigation },
    new AbilityStat { Key = "law", Aliases = new[] {"законы", "law"}, Selector = a => a.Law },
    new AbilityStat { Key = "medicine", Aliases = new[] {"медицина", "medicine"}, Selector = a => a.Medicine },
    new AbilityStat { Key = "occult", Aliases = new[] {"оккультизм", "occult"}, Selector = a => a.Occult },
    new AbilityStat { Key = "politics", Aliases = new[] {"политика", "politics"}, Selector = a => a.Politics },
    new AbilityStat { Key = "science", Aliases = new[] {"наука", "science"}, Selector = a => a.Science },
    new AbilityStat { Key = "linguistics", Aliases = new[] {"лингвистика", "linguistics"}, Selector = a => a.Linguistics }
        ];
    }

}