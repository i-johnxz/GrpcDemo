using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Models
{
    public class Blog
    {
        public int BlogId { get; protected set; }

        public string Title { get; protected set; }

        public string Url { get; protected set; }

        public List<Post> Posts { get; set; }

        public Blog()
        {
            
        }


        public Blog(string title, string url)
        {
            Title = title;
            Url = url;
        }

    }
}
