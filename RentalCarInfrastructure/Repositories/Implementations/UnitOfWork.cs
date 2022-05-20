using RentalCarInfrastructure.Context;
using RentalCarInfrastructure.Interfaces;
using RentalCarInfrastructure.Repositories.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private IUserRepository _userRepository;
        private ITripRepository _tripRepository;

        private ICommentRepository _commentRepository;

        private ICarRepository _carRepository;

       
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_appDbContext);
        public ITripRepository TripRepository => _tripRepository ??= new TripRepository(_appDbContext);
        public ICarRepository CarRepository => _carRepository  ??= new CarRepository(_appDbContext);    

        public ICommentRepository CommentRepository => _commentRepository ??= new CommentRepository(_appDbContext);
    }
}
