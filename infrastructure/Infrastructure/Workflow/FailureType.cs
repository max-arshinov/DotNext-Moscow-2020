namespace Infrastructure.Workflow
{
    public enum FailureType: byte
    {
        Unauthorized,
        Invalid,
        Exception,
        ConfigurationError,
        Other,
        NotImplemented
    }
}