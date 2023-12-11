using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpAPP.Services.Email.DbContexts;
using TpAPP.Services.Email.Messages;
using TpAPP.Services.Email.Models;

namespace TpAPP.Services.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContext;
        public EmailRepository(DbContextOptions<ApplicationDbContext> dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task SendAndLogEmail(UpdatePaymentResultMessage message)
        {
            // implement an email sender or call some other class library 
            EmailLog emailLog = new EmailLog()
            {
                Email = message.Email,
                EmailSent = DateTime.Now,
                Log = $"Order -{message.OrderId} has been created successfully."
            };
            await using var _db = new ApplicationDbContext(_dbContext);
            _db.Add(emailLog);
            await _db.SaveChangesAsync();
        }
    }
}
