using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Infra.DAL;
using chatBotIaCore.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chatBotIaCore.Infra.Repository
{
    public class ContactReposiotry : BaseRepository<Contact>, IContactInterface
    {
        public ContactReposiotry(ChatBotIaCoreContext context) : base(context)
        {
        }

        public async Task<bool> contactIsBlocked(string externalId)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.ConExternalId == externalId);
            if (contact == null)
            {
                return false;
            }
            return contact.ConIsBlocked;
        }

        public async Task<bool> contactIsClient(string externalId)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.ConExternalId == externalId);
            if (contact == null)
            {
                return false;
            }
            return contact.ConIsClient;
        }

        public async Task<Contact> contactExistIfNotCreate(IncomingMessage request)
        {
            var response = await _context.Contacts.FirstOrDefaultAsync(x => x.ConExternalId == request.SenderId);

            if (response == null)
            {
                response = new Contact
                {
                    ConIsBlocked = false,
                    ConIsClient = false,
                    ConCreatedAt = DateTime.Now,
                    ConPhoneNumber = request?.Phone?.Length > 0 ? request.Phone : "",
                    ConExternalId = request?.SenderId?.Length > 0 ? request.SenderId : "",
                    ConProfilePicPath = "",
                    ConDisplayName = request?.Name?.Length > 0 ? request.Name : "",
                    ConType = request!.MappeChannelTypeToContact()
                };

                var result = await _context.Contacts.AddAsync(response);
                response.ConId = result.Entity.ConId;
                await _context.SaveChangesAsync();
            }

            return response;
        }
    }
}
