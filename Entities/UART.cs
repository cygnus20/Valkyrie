namespace Valkyrie.Entities;

public record UART
{
    public string RX { get; init; } = string.Empty;
    public string TX { get; init; } = string.Empty;
}