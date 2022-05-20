using RentalCarInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarInfrastructure.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<bool> AddComment(Comment comment);
    }
}
