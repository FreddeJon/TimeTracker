namespace Application.Responses;
public class BaseResponse : IResponse
{
    public BaseResponse()
    {
        StatusCode = IResponse.Status.Success;
        StatusText = "Success";
    }

    public IResponse.Status StatusCode { get; set; }
    public string StatusText { get; set; }
}
