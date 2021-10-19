using Microsoft.AspNetCore.Mvc;

namespace Test.Infrastructure.Exceptions
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