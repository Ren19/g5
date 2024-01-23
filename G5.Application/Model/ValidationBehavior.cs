﻿using G5.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Application.Model
{
    public class ValidationBehavior<TRequst, TRespons> : IPipelineBehavior<TRequst, TRespons>
    {
        public Task<TRespons> Handle(TRequst request, MediatR.RequestHandlerDelegate<TRespons> next)
        {
            throw new NotImplementedException();
        }
    }
}
