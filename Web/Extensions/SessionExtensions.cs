using System.Text.Json;
using Business.Services;

namespace Web.Extensions;

public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetSessionData<T>(string key, T data)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context != null)
        {
            var json = JsonSerializer.Serialize(data);
            context.Session.SetString(key, json);
        }
    }

    public T GetSessionData<T>(string key)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context != null)
        {
            var sessionData = context.Session.GetString(key);
            return sessionData == null ? default : JsonSerializer.Deserialize<T>(sessionData);
        }
        
        return default;
    }
}