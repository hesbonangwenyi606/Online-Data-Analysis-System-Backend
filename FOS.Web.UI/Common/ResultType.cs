using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOS.Web.UI.Common
{
    public enum ResultType
    {
        Success = 1,
        Failure = 2,
        Empty = 3,
        Warning = 4,
        Information = 5,
        ValidationErrors = 6,
        Exception = 7,
        CodeWord = 8,
        Redirect = 9,
        Function = 10,
        Code = 11,
        Duplicate = 12,
        AccessRights = 13,
        DeActivated = 14
    }
}