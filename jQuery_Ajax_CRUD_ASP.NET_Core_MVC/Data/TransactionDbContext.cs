using jQuery_Ajax_CRUD_ASP.NET_Core_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace jQuery_Ajax_CRUD_ASP.NET_Core_MVC.Data
{
    public class TransactionDbContext : DbContext
    {
        private readonly DbContextOptions options;

        public TransactionDbContext(DbContextOptions options) : base(options)
        {
            this.options = options;
        }
        public DbSet<TransactionModel> Transactions { get; set; }
    }

}
