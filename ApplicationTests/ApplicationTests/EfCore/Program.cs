using EfCore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new EfCoreDbContext();
            for (int i = 0; i < 10; i++)
            {
                await context.Schools.AddAsync(new School { Name = Guid.NewGuid().ToString(), CreateDateTime = DateTimeOffset.Now });
            }

            var service = new LogService();
            await service.WriteLogAsync();

            for (int i = 0; i < 10; i++)
            {
                await context.Schools.AddAsync(new School { Name = Guid.NewGuid().ToString(), CreateDateTime = DateTimeOffset.Now });
            }
            await service.WriteLogAsync();

            await context.SaveChangesAsync();
        }
    }
}
