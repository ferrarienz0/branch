using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Branch.Models.NoSQL
{
    public class PostComparer : IEqualityComparer<Post>
    {
        bool IEqualityComparer<Post>.Equals(Post x, Post y)
        {
            return x.Id == y.Id;
        }

        int IEqualityComparer<Post>.GetHashCode(Post obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}