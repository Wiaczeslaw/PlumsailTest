using Microsoft.AspNetCore.Mvc;

namespace PlumsailTest.Infrastructure.Exceptions
{
    public class ActionResultException: AppException
    {
        public ActionResultException(ActionResult result)
        {
            Result = result;
        }
        
        public ActionResult Result { get; }
    }
}