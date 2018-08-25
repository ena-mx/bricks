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

        [JsonIgnore]
        public int TotalRows { get; set; }

        [JsonIgnore]
        public int PageRowsCount { get; set; }

        public IEnumerable<Link> LinksCollection()
        {
            string urlTemplate = Uri + "offset={0}&limit={1}";

            int newOffset = Offset + PageRowsCount;

            yield return new Link("self", "GET", string.Format(urlTemplate, Offset, Limit));

            if (Offset > 0)
                yield return new Link("first", "GET", string.Format(urlTemplate, 0, Limit));

            if (Offset - Limit >= 0)
                yield return new Link("prev", "GET", string.Format(urlTemplate, Offset - Limit, Limit));

            if (Offset + Limit <= TotalRows)
                yield return new Link("next", "GET", string.Format(urlTemplate, newOffset, Limit));

            if (TotalRows - Limit > 0 && newOffset < TotalRows)
                yield return new Link("last", "GET", string.Format(urlTemplate,
                    ((int)Math.Floor((double)TotalRows / (double)Limit) * Limit), Limit));
        }
    }
}