using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDGS
{
    public class DDGSearchResDto
    {
        [JsonProperty("deep_answers")]
        public System.Collections.Generic.Dictionary<string, object> DeepAnswers { get; set; }

        [JsonProperty("results")]
        public System.Collections.Generic.List<Result> Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("a")]
        public string A { get; set; }

        [JsonProperty("ae")]
        public object Ae { get; set; }

        [JsonProperty("b")]
        public string B { get; set; }

        [JsonProperty("c")]
        public string C { get; set; }

        [JsonProperty("d")]
        public string D { get; set; }

        [JsonProperty("da")]
        public string Da { get; set; }

        [JsonProperty("h")]
        public long H { get; set; }

        [JsonProperty("i")]
        public string I { get; set; }

        [JsonProperty("l", NullValueHandling = NullValueHandling.Ignore)]
        public System.Collections.Generic.List<L> L { get; set; }

        [JsonProperty("m")]
        public long M { get; set; }

        [JsonProperty("n", NullValueHandling = NullValueHandling.Ignore)]
        public string N { get; set; }

        [JsonProperty("o")]
        public long O { get; set; }

        [JsonProperty("p")]
        public long P { get; set; }

        [JsonProperty("s")]
        public string S { get; set; }

        [JsonProperty("t")]
        public string T { get; set; }

        [JsonProperty("u")]
        public string U { get; set; }
    }

    public partial class L
    {
        [JsonProperty("snippet")]
        public object Snippet { get; set; }

        [JsonProperty("targetUrl")]
        public string TargetUrl { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
