namespace EnaBricks.RestBricks
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ResourcePage<T> : ICreateLinks
    {
        [JsonProperty(Order = 0)]
        public int Offset { get; set; }

        [JsonProperty(Order = 1)]
        public int Limit { get; set; }

        [JsonProperty(Order = 2)]
        public int Count { get; set; }

        [JsonProperty(Order = 3)]
        public int Total { get; set; }

        [JsonProperty(Order = 4)]
        public int CurrentPage => ((int)Math.Ceiling((double)Offset / (double)Limit));

        [JsonProperty(Order = 5)]
        public int PagesCount => ((int)Math.Ceiling((double)Total / (double)Limit));

        [JsonProperty(Order = 6)]
        public Link[] Links => LinksCollection().ToArray();

        [JsonProperty(Order = 7)]
        public T[] Items { get; set; }

        [JsonIgnore]
        public string Uri { get; set; }

        public IEnumerable<Link> LinksCollection()
        {
            string urlTemplate = Uri + "offset={0}&limit={1}";

            int newOffset = Offset + Count;
            yield return new Link
            {
                Name = "self",
                Method = "GET",
                Url = string.Format(urlTemplate, Offset, Limit)
            };

            if (Offset > 0)
            {
                yield return new Link
                {
                    Name = "first",
                    Method = "GET",
                    Url = string.Format(urlTemplate, 0, Limit)
                };
            }
            if (Offset - Limit >= 0)
            {
                yield return new Link
                {
                    Name = "prev",
                    Method = "GET",
                    Url = string.Format(urlTemplate, Offset - Limit, Limit)
                };
            }
            if (Offset + Limit <= Total)
            {
                yield return new Link
                {
                    Name = "next",
                    Method = "GET",
                    Url = string.Format(urlTemplate, newOffset, Limit)
                };
            }
            if (Total - Limit > 0 && newOffset < Total)
            {
                yield return new Link
                {
                    Name = "last",
                    Method = "GET",
                    Url = string.Format(urlTemplate,
                    ((int)Math.Floor((double)Total / (double)Limit) * Limit), Limit)
                };
            }
        }
    }
}