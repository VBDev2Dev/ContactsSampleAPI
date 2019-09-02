using ContactsAPISample.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsAPISample.Models.DB
{
    public class EmailAddress
    {
        [ForeignKey(nameof(ContactID))]
        public Contact Contact { get; set; }
        public long ContactID { get; set; }
        [MaxLength(40)]
        [Key]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(30)]
        public string Type { get; set; }
    }
}


namespace ContactsAPISample.EF
{
    partial class ContactsContext
    {
        public DbSet<EmailAddress> EmailAddresses { get; set; }

    }
}
