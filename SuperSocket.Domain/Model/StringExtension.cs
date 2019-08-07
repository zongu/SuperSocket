
namespace SuperSocket.Domain.Model
{
    using Newtonsoft.Json;

    public static class StringExtension
    {
        public static T JsonStringDeserialize<T>(this string str)
            => JsonConvert.DeserializeObject<T>(str);
    }
}
