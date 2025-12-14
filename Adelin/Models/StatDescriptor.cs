namespace Adelin.Models;

public class StatDescriptor
{
    public Func<int> Get { get; init; } = null!;
    public Action<int> Set { get; init; } = null!;
    public Action<int>? Add { get; init; }
    
    public int? Min { get; init; }
    public int? Max { get; init; }
}