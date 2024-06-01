using Final.Core;
using System.Text.Json.Serialization;

namespace Fina.Core.Response;

public class Response<TData>
{
    private int _code = Configurations.DefaultStatusCode;

    [JsonConstructor]
    public Response()
        => _code = Configurations.DefaultStatusCode;

    public Response(TData? data, int code = Configurations.DefaultStatusCode, string? message = null)
    {
        Data = data;
        _code = code;
        Message = message;
    }

    public TData? Data { get; set; }
    public string? Message { get; set; }

    [JsonIgnore]
    public bool IsSuccess => _code is >= 200 and <= 299;
}