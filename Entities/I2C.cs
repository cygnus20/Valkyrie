using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Valkyrie.Entities;

[Keyless]
public record I2C
{
    public string SDA { get; init; } = string.Empty;
    public string SCL { get; init; } = string.Empty;
}
