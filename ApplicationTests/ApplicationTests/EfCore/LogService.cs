using EfCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore
{
    public class LogService
    {
        private readonly EfCoreDbContext _context;
        public LogService()
        {
            _context = new EfCoreDbContext();
        }

        public async Task WriteLogAsync()
        {
            await _context.Histories.AddAsync(new History 
            {
                Name = Guid.NewGuid().ToString(),
                CreateDateTime = DateTimeOffset.Now,
            });
            await _context.SaveChangesAsync();
        }
    }
}
