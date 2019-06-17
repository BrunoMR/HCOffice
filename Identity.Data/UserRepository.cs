namespace Identity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Dto;

    public class UserRepository : IUserRepository
    {
        private readonly IdentityContext context;

        public UserRepository()
        {
            context = new IdentityContext();
        }

        public User GetById(string id)
        {
            return this.context.Users.Find(id);
        }

        /// <summary>The get by customer id.</summary>
        /// <param name="customerId">The customer id.</param>
        /// <returns>Returns the users of custumer by Id</returns>
        public IEnumerable<User> GetByCustomerId(int customerId)
        {
            return this.context.Users
                .Include(u => u.UserRoles.Select(r => r.Role))
                .Where(
                    u => u.CustomerId == customerId
                    && u.UserRoles.Any(r => r.Role.Name == "Operator"))
                .ToList();
        }

        public IEnumerable<User> GetAll()
        {
            return this.context.Users.ToList();
        }

        public void Dispose()
        {
            this.context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
