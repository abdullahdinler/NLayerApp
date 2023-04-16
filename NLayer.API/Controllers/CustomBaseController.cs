using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    // Bu sınıf API için özel bir yanıt oluşturmak için kullanılır
    // 200 gelince ok 404 gelince notfound 400 gelince badrequest 500 gelince internalservererror
    // Bu sınıfı kullanmak için controller sınıflarında : CustomBaseController yazmak yeterli
    // Örnek kullanım: return CreateActionResult(CustomResponseDto<ProductDto>.Success(productDto , 200));
    // Bu şekilde kullanırsak 200 döner

    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> responseDto) 
        {
            switch (responseDto.StatusCode)
            {
                case 200:
                    return Ok(responseDto);
                case 204:
                    return NoContent();
                case 400:
                    return BadRequest(responseDto);
                case 404:
                    return NotFound(responseDto);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, responseDto);
            }
        }
    }
}
