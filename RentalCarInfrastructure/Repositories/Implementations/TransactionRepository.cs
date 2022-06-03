using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Implementations
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        AppDbContext _appDbContext;
        public TransactionRepository( AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AddTransaction(Transaction transaction)
        {
            var result = await Add(transaction);
            return result;
        }

        public Transaction GetTransactionReference(string reference)
        {
           var result = _appDbContext.Transactions.Where(t=>t.TransactionRef == reference).FirstOrDefault(); 
           return result;
        }

        public void UpdateTransaction(Transaction transaction)
        {
            _appDbContext.Update(transaction);
            _appDbContext.SaveChanges();
        }
    }
}
