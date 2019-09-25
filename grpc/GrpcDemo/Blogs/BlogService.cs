using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Models;
using Blogs.Protos;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Blogs
{
    public class BlogService : BlogContextService.BlogContextServiceBase
    {
        private readonly BlogContext _blogContext;

        public BlogService(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public override async Task<BlogReply> AddBlog(BlogRequest request, ServerCallContext context)
        {
            var blog = new Blog(request.Title, request.Url);

            _blogContext.Blogs.Add(blog);

            await _blogContext.SaveChangesAsync();

            blog = await _blogContext.Blogs.FirstOrDefaultAsync(f => f.Title == request.Title);

            return new BlogReply()
            {
                BlogId = blog.BlogId
            };
        }
    }
}
