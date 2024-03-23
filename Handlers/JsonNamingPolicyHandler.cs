using System.Text.Json;

namespace MariscosSanMartinAPI.Handlers
{
    public class JsonNamingPolicyHandler : JsonNamingPolicy
    {
        public override string ConvertName(string name) =>
            name.ToLower();
    }
}
