using Microsoft.EntityFrameworkCore;
using AppDotNet.Entities;

namespace AppDotNet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Items> Item {  get; set; }


    }
}
