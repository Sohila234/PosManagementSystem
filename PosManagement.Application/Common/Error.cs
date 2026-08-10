using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PosManagement.Application.Common
{
    public record Error(string Code , string Description , ErrorType Type = ErrorType.Failure)
    {
        public static Error Failure ( string Code = "General.Failure" , string Description = "General Failure")
            => new Error (Code , Description , ErrorType.Failure);
        public static Error Validation(string Code = "General.Validation", string Description = "General Validation")
           => new Error(Code, Description, ErrorType.Validation);
        public static Error NotFound(string Code = "General.NotFound", string Description = "General NotFound")
           => new Error(Code, Description, ErrorType.NotFound);
        public static Error Conflict(string Code = "General.Conflict", string Description = "General Conflict")
           => new Error(Code, Description, ErrorType.Conflict);
        public static Error UnAuthorized(string Code = "General.UnAuthorized", string Description = "General UnAuthorized")
           => new Error(Code, Description, ErrorType.UnAuthorized);
        public static Error Forbidden(string Code = "General.Forbidden", string Description = "General Forbidden")
           => new Error(Code, Description, ErrorType.Forbidden);
        public static Error InValidcredentails(string Code = "General.InValidcredentails", string Description = "General InValidcredentails")
           => new Error(Code, Description, ErrorType.InValidcredentails);
    }
}
