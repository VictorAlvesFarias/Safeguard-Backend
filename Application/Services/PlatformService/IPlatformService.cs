﻿using Application.Dtos.Default;
using Application.Dtos.Platform.Base;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IPlatformService
    {
        BaseResponse<Platform> Register(PlatformRequest plat);
        BaseResponse<Platform> Update(PlatformRequest plat, int id);
        DefaultResponse Delete(int id);
        BaseResponse<List<Platform>> Get();
        BaseResponse<Platform> GetPlatformById(int id);
    }
}
