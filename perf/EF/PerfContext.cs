using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace perf.Import.EF
{
    class PerfContext:DbContext
    {
        public DbSet<Perfume> Perfumes { get; set; }

        public PerfContext() : base("DbConnection")
        { }
    }
}
