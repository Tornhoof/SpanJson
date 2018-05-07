using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    // Similar to the ones in CoreFxLab
    public class ReadAllTests
    {

        private static readonly string HeavyNestedJson =
            "{\"array\":[{\"_id\":\"56280d1abea79cfca762cd56\",\"index\":0,\"isActive\":false,\"tags\":[\"ad\",\"voluptate\",\"ullamco\",\"reprehenderit\",\"duis\",\"Lorem\",\"anim\"],\"friends\":[{\"id\":0,\"name\":\"Fernandez Barr\",\"friends\":[{\"id\":0,\"name\":\"Selena Hoover\",\"friends\":[{\"id\":0,\"name\":\"Verna Keller\",\"friends\":[{\"id\":0,\"name\":\"Middleton Duncan\",\"friends\":[{\"id\":0,\"name\":\"Fitzgerald Mcbride\",\"friends\":[{\"id\":0,\"name\":\"Boyd Marshall\",\"friends\":[{\"id\":0,\"name\":\"Debbie Hess\",\"friends\":[{\"id\":0,\"name\":\"Larson Mcmahon\",\"friends\":[{\"id\":0,\"name\":\"Noreen Hawkins\",\"friends\":[{\"id\":0,\"name\":\"Pearl Vargas\"}]}]}]}]}]}]}]}]}]}]},{\"_id\":\"56280d1a9662857ada66334f\",\"index\":1,\"isActive\":false,\"tags\":[\"minim\",\"reprehenderit\",\"fugiat\",\"nulla\",\"incididunt\",\"est\",\"consectetur\"],\"friends\":[{\"id\":0,\"name\":\"Blackwell Navarro\",\"friends\":[{\"id\":0,\"name\":\"Cannon Powers\",\"friends\":[{\"id\":0,\"name\":\"Nancy Peterson\",\"friends\":[{\"id\":0,\"name\":\"Shelley Randall\",\"friends\":[{\"id\":0,\"name\":\"Staci Richmond\",\"friends\":[{\"id\":0,\"name\":\"Gwen Arnold\",\"friends\":[{\"id\":0,\"name\":\"Kane Lara\",\"friends\":[{\"id\":0,\"name\":\"Barry Bolton\",\"friends\":[{\"id\":0,\"name\":\"Tracie Rowland\",\"friends\":[{\"id\":0,\"name\":\"Annette Maxwell\"}]}]}]}]}]}]}]}]}]}]}]}";

        private static readonly byte[] HeavyNestedJsonBytes = Encoding.UTF8.GetBytes(HeavyNestedJson);

        private static readonly string HelloWorld = "{ \"message\": \"Hello, World!\" }";
        private static readonly byte[] HelloWorldJsonByte = Encoding.UTF8.GetBytes(HelloWorld);

        [Theory]
        [MemberData(nameof(GetUtf8Data))]
        public void ReadAllUtf8(byte[] input)
        {
            var reader = new JsonReader<byte>(input);
            JsonToken token;
            while ((token = reader.ReadUtf8NextToken()) != JsonToken.None)
            {
                reader.SkipNextUtf8Value(token);
            }
        }
        [Theory]
        [MemberData(nameof(GetUtf16Data))]
        public void ReadAllUtf16(string input)
        {
            var reader = new JsonReader<char>(input);
            JsonToken token;
            while ((token = reader.ReadUtf16NextToken()) != JsonToken.None)
            {
                reader.SkipNextUtf16Value(token);
            }
        }

        public static IEnumerable<object[]> GetUtf16Data()
        {
            yield return new object[] {HeavyNestedJson};
            yield return new object[] {HelloWorld};
        }


        public static IEnumerable<object[]> GetUtf8Data()
        {
            yield return new object[] {HeavyNestedJsonBytes};
            yield return new object[] {HelloWorldJsonByte};
        }
    }
}
