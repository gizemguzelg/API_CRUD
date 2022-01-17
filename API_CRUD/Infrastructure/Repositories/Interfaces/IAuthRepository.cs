using API_CRUD.Model.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Infrastructure.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<AppUser> Authentication(string userName, string password);
    }
}
