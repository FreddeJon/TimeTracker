namespace Application.Contracts.Responses;
public interface IResponse
{
    public Status StatusCode { get; set; }
    public string StatusText { get; set; }

    public enum Status
    {
        Error,
        Success
    }
}
