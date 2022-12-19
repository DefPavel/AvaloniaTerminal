using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AvaloniaTerminal.Services.Api;

public static class QueryService
{
    private static readonly string? ApiUrl = ConfigurationManager.AppSettings["api"];
    
    public static async ValueTask<T> JsonDeserializeWithToken<T>(string token, string queryUrl, string httpMethod) where T : new()
    {
#pragma warning disable SYSLIB0014 // Тип или член устарел
        var req = (HttpWebRequest)WebRequest.Create(ApiUrl + queryUrl);     // Создаём запрос
#pragma warning restore SYSLIB0014 // Тип или член устарел
        req.Method = httpMethod;                                                        // Выбираем метод запроса
        req.Headers.Add("auth-token", token);
        req.Accept = "application/json";

        using var response = await req.GetResponseAsync();

        await using var responseStream = response.GetResponseStream();
        using StreamReader reader = new(responseStream, Encoding.UTF8);
        return JsonSerializer.Deserialize<T>(await reader.ReadToEndAsync())
               ?? throw new NullReferenceException();// Возвращаем json информацию которая пришла 
    }
    public static async Task JsonObjectWithToken(string token, string queryUrl, string httpMethod)
    {
#pragma warning disable SYSLIB0014 // Тип или член устарел
        var req = (HttpWebRequest)WebRequest.Create(queryUrl);     // Создаём запрос
#pragma warning restore SYSLIB0014 // Тип или член устарел
        req.Method = httpMethod;                                                        // Выбираем метод запроса
        req.Headers.Add("auth-token", token);
        req.Accept = "application/json";
        using var response = await req.GetResponseAsync();
        // await using var responseStream = response.GetResponseStream();
       // using StreamReader reader = new(responseStream, Encoding.UTF8);
       /* return JsonSerializer.Deserialize<T>(await reader.ReadToEndAsync())
               ?? throw new NullReferenceException();
               */    // Возвращаем json информацию которая пришла 
    }
}