using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Apstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>>GetClaims(User user);
        IResult Add(User user);
        IResult Update(User user);
        IDataResult<List<User>> GetByUser(int id);
        IDataResult<User>GetByMail(string email);
        User GetByMaill(string email);
    }
}
