namespace EnaBricks.RestBricks
{
    using Newtonsoft.Json;

    public sealed class Link
    {
        [JsonProperty(Order = 0)]
        public string Name { get; set; }

        [JsonProperty(Order = 1)]
        public string Method { get; set; }

        [JsonProperty(Order = 2)]
        public string Url { get; set; }
    }
}