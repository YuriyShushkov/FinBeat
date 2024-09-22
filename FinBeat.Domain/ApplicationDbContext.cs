using FinBeat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinBeat.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<DataRecord> DataRecords { get; set; }
        public DbSet<ApiLog> ApiLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
