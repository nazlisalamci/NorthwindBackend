using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
   public interface IUserService
    {
        List<OperationClaim> GetClaim(User user);
        void Add(User user);

        User GetByMail(string email);
    }
}
