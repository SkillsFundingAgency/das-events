using System.Threading.Tasks;

namespace SFA.DAS.Events.Api.Client
{
    public interface ISecureHttpClient
    {
        Task<string> GetAsync(string url);
        Task<string> PostAsync(string url, string data);
        Task<string> PutAsync(string url, string data);
        Task<string> PatchAsync(string url, string data);
    }
}
