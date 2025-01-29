using System.Text.Json.Serialization;

namespace Dima.Core.BaseResponses;

public class BaseResponse<TData>
{
    private readonly int _code = Configuration.DefaultStatusCode;

    [JsonConstructor]
    public BaseResponse() => _code = Configuration.DefaultStatusCode;   

    public BaseResponse(TData? data, int code = Configuration.DefaultStatusCode, string? message = null)
    {
        Data = data;
        Message = message;
        _code = code;
    }

    public TData? Data { get; set; }

    public string? Message { get; set; }

    public bool IsSuccess => _code is >= 200 and <= 299;
}
