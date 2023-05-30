namespace Valkyrie.Entities;

public record SoC
{
    public string CPUName { get; init; } = string.Empty;
    public string CPUArchitecture { get; init; } = string.Empty;
    public string CPUFamily { get; init; } = string.Empty;
    public List<Core> CPUCores { get; init; } = new();
    public string GPUName { get; init; } = string.Empty;
    public List<Memory> Memories { get; init; } = Enumerable.Empty<Memory>().ToList();
    
}
