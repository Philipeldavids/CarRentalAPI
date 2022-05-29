using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Models;
using RentalCarInfrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ITripRepository TripRepository { get; }

        ICommentRepository CommentRepository { get; }   

        ICarRepository CarRepository { get; }

        IRatingRepository RatingRepository { get; }
        ITransactionRepository TransactionRepository { get; }

        IDealerRepository DealerRepository { get; }
    }
}
