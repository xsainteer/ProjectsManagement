using System.Text.Json;

namespace Business.Services;

public interface ISessionService
{
    void SetSessionData<T>(string key, T data);
    T GetSessionData<T>(string key);
}