using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    // Bu sınıf modelstate hatalarını yakalar ve BadRequestObjectResult döner
    // Örnek olarak ProductDto içerisindeki Name alanı boş bırakılırsa BadRequestObjectResult döner
    // Bu sınıfı kullanmak için [ValidateFilter] attribute kullanılır
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(errors, 400));
        }
    }
}
