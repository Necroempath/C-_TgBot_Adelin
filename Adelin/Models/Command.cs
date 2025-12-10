namespace Adelin.Models;

public record Command(Func<string[], string> Handler, string Description);