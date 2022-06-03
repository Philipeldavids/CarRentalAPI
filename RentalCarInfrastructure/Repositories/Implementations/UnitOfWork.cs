using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Interfaces;
using RentalCarInfrastructure.Repositories.Interfaces;

namespace RentalCarInfrastructure.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private IUserRepository _userRepository;
        private ITripRepository _tripRepository;

        private ICommentRepository _commentRepository;

        private ICarRepository _carRepository;
        private IRatingRepository _ratingRepository;
        private ITransactionRepository _transactionRepository;

        private IDealerRepository _dealerRepository;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_appDbContext);
        public ITripRepository TripRepository => _tripRepository ??= new TripRepository(_appDbContext);
        public ICarRepository CarRepository => _carRepository  ??= new CarRepository(_appDbContext);

        public IRatingRepository RatingRepository => _ratingRepository ??= new RatingRepository(_appDbContext);

        public ICommentRepository CommentRepository => _commentRepository ??= new CommentRepository(_appDbContext);
        public ITransactionRepository TransactionRepository => _transactionRepository ??= new TransactionRepository(_appDbContext);
        public IDealerRepository DealerRepository => _dealerRepository ??= new DealerRepository(_appDbContext);
    }
}
