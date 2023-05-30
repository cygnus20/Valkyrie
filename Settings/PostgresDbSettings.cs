namespace Valkyrie.Settings;

public class PostgresDbSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string DbName { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string ConnectionString
    {
        get => $"Host={Host};Database={DbName};Username={User};Password={Password}";
    }
}
