using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketServices;
using Store.Service.Services.BasketServices.Dtos;

namespace Store.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketContoller : BaseController
    {
        private readonly IBasketService _basketService;

        public BasketContoller(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketAsync(string id)
            => Ok(await _basketService.GetBasketAsync(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto input)
            => Ok(await _basketService.UpdateBasketAsync(input));
        
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
            => Ok(await _basketService.DeleteBasketAsync(id));
    }
}
