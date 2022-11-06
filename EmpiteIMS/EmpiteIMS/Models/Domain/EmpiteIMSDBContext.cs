using EmpiteIMS.Models;
using EmpiteIMS.Models.Inventory;
using EmpiteIMS.Models.Distribution;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmpiteIMS.Models.Domain
{
    public class EmpiteIMSDBContext : IdentityDbContext<ApplicationUser>
    {
        public EmpiteIMSDBContext(DbContextOptions<EmpiteIMSDBContext> options) : base(options)
        {

        }
        public DbSet<InventoryModel> Inventory { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
    }
}
