﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver
{
    public interface IRegisterComponent
    {
       // Register type method
        void RegisterType<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;
       // Register type with container controlled life time manager
        void RegisterTypeWithControlledLifeTime<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;
    }
}
