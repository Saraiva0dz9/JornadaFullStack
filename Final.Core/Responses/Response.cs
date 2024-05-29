using System.Text.Json.Serialization;

namespace Final.Core.Responses;

public class Response<TData>
{
    private int _code = 200;

    [JsonConstructor]
    public Response(Models.Category category) => this._code = Configurations.DefaultStatusCode;

    public Response(TData? data, int code = Configurations.DefaultStatusCode, string? message = null)
    {
        this.Data = data;
        this._code = code;
        this.Message = message;
    }

    private TData? Data { get; set; }
    private string? Message { get; set; } 
    [JsonIgnore]
    public bool IsSuccess => _code is > 200 and < 299;
}
