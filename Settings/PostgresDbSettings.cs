namespace Valkyrie.Settings;

public class PostgresDbSettings
{
    public string Host { get; set; } = "";
    public int Port { get; set; }
    public string DbName { get; set; } = "";
    public string User { get; set; } = "";
    public string Password { get; set; } = "";

    public string ConnectionString
    {
        get => $"Host={Host};Database={DbName};Username={User};Password={Password}";
    }
}
