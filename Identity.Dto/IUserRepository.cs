using System;
using System.Collections.Generic;

namespace Identity.Dto
{
    public interface IUserRepository : IDisposable
    {
        User GetById(string id);
        IEnumerable<User> GetByCustomerId(int customerId);
        IEnumerable<User> GetAll();
    }
}
