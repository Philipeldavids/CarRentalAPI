using RentalCarInfrastructure.Models;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<bool> AddTransaction(Transaction transaction);
        Transaction GetTransactionReference(string reference);
        Task<bool> UpdateTransaction(Transaction transaction);
    }
}