using Microsoft.EntityFrameworkCore;
namespace crypto.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Bitcoin> bitcoin { get; set; }
        public DbSet<BitCoinCash> bcash { get; set; }
        public DbSet<Ethereum> ethereum { get; set; }
        public DbSet<Litecoin> litecoin { get; set; }
    }
}