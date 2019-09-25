using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogsClient.Models;
using Grpc.Net.Client;

namespace BlogsClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly BlogContextService.BlogContextServiceClient _blogContextServiceClient;

        public HomeController(ILogger<HomeController> logger, BlogContextService.BlogContextServiceClient blogContextServiceClient)
        {
            _logger = logger;
            _blogContextServiceClient = blogContextServiceClient;

            //AppContext.SetSwitch(
            //    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            //var channel = GrpcChannel.ForAddress("http://localhost:5000");

            //_blogContextServiceClient = new BlogContextService.BlogContextServiceClient(channel);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {

            var result = await _blogContextServiceClient.AddBlogAsync(new BlogRequest()
            {
                Title = "测试博客",
                Url = "http://www.baidu.com"
            });
            return Content(result.BlogId.ToString());
            //return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
