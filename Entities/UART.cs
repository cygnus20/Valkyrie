namespace Valkyrie.Entities;

public record UART
{
    public string RX { get; init; } = "";
    public string TX { get; init; } = "";
}