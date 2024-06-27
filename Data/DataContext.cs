using Microsoft.EntityFrameworkCore;
using Updates.Entities;

namespace Updates.Data
{
    public class DataContext : DbContext
    {
        //out constructor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        //the table that represents the data entries
        public DbSet<Update> Updates { get; set; }
    }
}
