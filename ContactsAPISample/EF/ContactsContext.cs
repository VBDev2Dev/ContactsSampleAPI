using Microsoft.EntityFrameworkCore;

namespace ContactsAPISample.EF
{
    public partial class ContactsContext : DbContext
    {
        public ContactsContext(DbContextOptions options)
            : base(options)
        {
        }

    }
}
