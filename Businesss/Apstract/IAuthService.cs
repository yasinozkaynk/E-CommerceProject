﻿using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Securty.JWT;
using Entities.DTOs;

namespace Business.Apstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Update(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IResult UserExistsId(int id);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
