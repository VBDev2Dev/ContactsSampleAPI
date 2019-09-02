using ContactsAPISample.EF;
using ContactsAPISample.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPISample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsContext _context;

        public ContactsController(ContactsContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public IEnumerable<Contact> GetContacts()
        {
            return _context.Contacts
                .Include(c => c.EmailAddresses).
                AsNoTracking();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts
                .Include(c => c.EmailAddresses)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ID == id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact([FromRoute] long id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.ID)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;
            if (!ProcessEmailAddresses(contact))
            {
                ModelState.AddModelError(nameof(contact.EmailAddresses), "EmailAddresses were not valid.  An email address is already being used by another contact.");
                return BadRequest(ModelState);
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!ProcessEmailAddresses(contact))
            {
                ModelState.AddModelError(nameof(contact.EmailAddresses), "EmailAddresses were not valid.  An email address is already being used by another contact.");
                return BadRequest(ModelState);
            }

            _context.Contacts.Add(contact);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.ID }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        /// <summary>
        /// Figures out how to update the email addresses
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        bool ProcessEmailAddresses(Contact contact)
        {
            var inDB = (from db in _context.EmailAddresses.AsNoTracking() // used to force db access and not entities that are in the context's cache
                        where contact.EmailAddresses.Select(e => e.Email).Contains(db.Email) || contact.ID==db.ContactID
                        select db).ToList();
            if (inDB.Any() && inDB.Select(e => e.ContactID).Distinct().Any(id => id != contact.ID))
            {
                return false;
            }


            var matched = (from db in inDB
                           join updated in contact.EmailAddresses ?? new List<EmailAddress>() on db.Email equals updated.Email
                           select new { FromDB = db, Current = updated }).ToList();

            var toUpdate = matched;
            var toRemove = inDB.Except(matched.Select(m => m.FromDB));
            var toAdd = contact.EmailAddresses.Except(matched.Select(m => m.Current));


            foreach (var address in toUpdate)
            {
                _context.Entry(address.FromDB).State = EntityState.Detached;
                _context.EmailAddresses.Attach(address.Current);
                _context.Entry(address.Current).State = EntityState.Modified;

            }
            foreach (var address in toAdd)
            {
                _context.EmailAddresses.Add(address);
            }
            foreach (var address in toRemove)
            {
                _context.EmailAddresses.Remove(address);
            }
            return true;
        }

        private bool ContactExists(long id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }
    }
}