using RentalCarInfrastructure.Models;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<bool> AddTransaction(Transaction transaction);
        Transaction GetTransactionReference(string reference);
        void UpdateTransaction(Transaction transaction);
    }
}