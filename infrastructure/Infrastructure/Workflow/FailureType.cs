namespace Infrastructure.Workflow
{
    public enum FailureType: byte
    {
        Unauthorized,
        Invalid,
        ConfigurationError,
        Other,
        NotImplemented
    }
}