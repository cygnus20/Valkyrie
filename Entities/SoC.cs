namespace Valkyrie.Entities;

public record SoC
{
    public string CPUName { get; init; } = "";
    public string CPUArchitecture { get; init; } = "";
    public string CPUFamily { get; init; } = "";
    public Dictionary<string, int> MPUCore { get; init; } = new();
    public string GPUName { get; init; } = "";
    public Dictionary<string, int> Memories { get; init; } = new();
    public Dictionary<string, string> NetworkingAndComm { get; set; } = new();
    
}
