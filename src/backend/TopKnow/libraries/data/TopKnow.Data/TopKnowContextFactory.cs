using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using TopKnow.Data.Context;

namespace TopKnow.Data;

public class TopKnowContextFactory : IDesignTimeDbContextFactory<TopKnowContext>
{
    public TopKnowContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TopKnowContext>();
        optionsBuilder.UseSqlServer("Server=127.0.0.1,10001;Database=TopKnowDb;User Id=topknowadmin;Password=1q2w3e4R!;TrustServerCertificate=True");

        return new TopKnowContext(optionsBuilder.Options);
    }
}
