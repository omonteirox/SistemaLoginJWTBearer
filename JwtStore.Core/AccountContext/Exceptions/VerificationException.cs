﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Core.AccountContext.Exceptions
{
    public class VerificationException : Exception
    {
        public VerificationException(string? message) : base(message)
        {
        }
    }
}