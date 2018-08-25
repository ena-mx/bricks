namespace EnaBricks.RestBricks
{
    using Newtonsoft.Json;
    using System;

    public sealed class Link
    {
        [JsonProperty(Order = 0)]
        public string Name { get; set; }

        [JsonProperty(Order = 1)]
        public string Method { get; set; }

        [JsonProperty(Order = 2)]
        public string Url { get; private set; }

        public Link(string name, string method, string url)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("message", nameof(method));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("message", nameof(url));
            }

            Name = name;
            Url = url;
            Method = method;
        }
    }
}