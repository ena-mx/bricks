namespace EnaBricks.RestBricks
{
    using System.Collections.Generic;

    public interface ICreateLinks
    {
        IEnumerable<Link> LinksCollection();
    }
}