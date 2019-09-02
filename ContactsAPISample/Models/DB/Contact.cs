using ContactsAPISample.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ContactsAPISample.Models.DB
{
    public class Contact : IValidatableObject
    {

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Birthdate > DateTime.Now)
            {
                yield return new ValidationResult("A person cannot be born in the future.", new string[] { nameof(Birthdate) });
            }

           
        }

        public DateTime Birthdate { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string NickName { get; set; }
    }
}

namespace ContactsAPISample.EF
{

    partial class ContactsContext
    {
        public DbSet<Contact> Contacts { get; set; }

    }

}
