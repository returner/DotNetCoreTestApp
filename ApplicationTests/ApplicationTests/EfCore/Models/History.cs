using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Models
{
    public class History
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreateDateTime { get; set; }
    }
}
