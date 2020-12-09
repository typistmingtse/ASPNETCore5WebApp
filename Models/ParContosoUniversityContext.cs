using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo.Models
{
    public partial class ParContosoUniversityContext : DbContext
    {
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}