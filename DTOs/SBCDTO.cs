using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Valkyrie.Entities;

namespace Valkyrie.DTOs;

public record SBCDTO
{
    public Guid Guid { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Platform { get; init; } = string.Empty;
    public List<string> OperatingSystems { get; init; } = Enumerable.Empty<string>().ToList();
    public List<string> Sensors { get; init; } = Enumerable.Empty<String>().ToList();
    public List<string>? ExtraInterfaces { get; init; }
    public Power Power { get; init; } = new();
    public Pins Pins { get; init; } = new();
    public Communications Communications { get; set; } = new();
    public List<string> IO { get; init; } = Enumerable.Empty<string>().ToList();

    public List<NetworkComm> NetworkingAndComm { get; set; } = new();
    public List<string> SpecialFeatures { get; init; } = Enumerable.Empty<string>().ToList();
}
