using Adelin.Realizations;

public sealed class AttributeRegistry
{
   public IReadOnlyCollection<AttributeStat> All { get; }

    public AttributeRegistry()
    {
        All =
        [
            new AttributeStat
            {
                Key = "strength",
                Aliases = ["сила", "strength"],
                Selector = a => a.Strength
            },
            new AttributeStat
            {
                Key = "dexterity",
                Aliases = ["ловкость", "dexterity"],
                Selector = a => a.Dexterity
            },
            new AttributeStat
            {
                Key = "stamina",
                Aliases = ["выносливость", "stamina"],
                Selector = a => a.Stamina
            },
            new AttributeStat
            {
                Key = "charisma",
                Aliases = ["обаяние", "харизма", "charisma"],
                Selector = a => a.Charisma
            },
            new AttributeStat
            {
                Key = "manipulation",
                Aliases = ["манипулирование", "манипуляция", "manipulation"],
                Selector = a => a.Manipulation
            },
            new AttributeStat
            {
                Key = "appearance",
                Aliases = ["привлекательность", "внешность", "appearance"],
                Selector = a => a.Appearance
            },
            new AttributeStat
            {
                Key = "perception",
                Aliases = ["восприятие", "perception"],
                Selector = a => a.Perception
            },
            new AttributeStat
            {
                Key = "intelligence",
                Aliases = ["интеллект", "intelligence"],
                Selector = a => a.Intelligence
            },
            new AttributeStat
            {
                Key = "wits",
                Aliases = ["смекалка", "wits"],
                Selector = a => a.Wits
            }
        ];
    }
}