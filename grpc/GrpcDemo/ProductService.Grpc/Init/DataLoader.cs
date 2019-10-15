using System.Linq;
using ProductService.Grpc.DataAccess.EF;

namespace ProductService.Grpc.Init
{
    public class DataLoader
    {
        private readonly ProductDbContext dbContext;

        public DataLoader(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            dbContext.Database.EnsureCreated();
            if (dbContext.Products.Any())
            {
                return;
            }

            dbContext.Products.Add(DemoProductFactory.Travel());
            dbContext.Products.Add(DemoProductFactory.House());
            dbContext.Products.Add(DemoProductFactory.Farm());
            dbContext.Products.Add(DemoProductFactory.Car());

            dbContext.SaveChanges();
        }
    }
}