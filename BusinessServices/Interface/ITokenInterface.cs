﻿using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interface
{
    public interface ITokenInterface
    {
        TokenEntity GenerateToken(Guid? userId);
        bool ValidateToken(string tokenId);
        bool Kill(string tokenId);
        bool DeleteByUserId(Guid userId);
    }
}
