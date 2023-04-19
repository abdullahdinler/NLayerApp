using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    #region Info
    // Generic bir yapıya bir bir notfound filterı yazdık.
    // Burada action ilk paremetresının değerini alıp, ona göre işlem yapacağız.
    // Örneğin, id değerini alıp, o id değerine sahip bir entity var mı yok mu kontrol edeceğiz.
    // Eğer id var ise , actiona devam edeceğiz.
    // Eğer id yok ise, notfound döneceğiz.
    // new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail($"{typeof(T).Name}({id}) not found", 404)); kullanarak notfound döndürdük.
    // TODO: Bu filterı kullanmak için, controllera [ServiceFilter(typeof(NotFoundFilter<>))] attributeunu eklememiz gerekiyor.
    #endregion

    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get id from context
            // Check if entity exists
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue is null)
            {
                await next.Invoke();
                return;
            }
            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail($"{typeof(T).Name}({id}) not found", 404));

        }
    }
}
