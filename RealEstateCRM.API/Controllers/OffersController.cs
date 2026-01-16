using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateCRM.Application.Interfaces.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IGenericRepository<Domain.Entities.Offer> _offerRepository;

        public OffersController(IGenericRepository<Domain.Entities.Offer> offerRepository)
        {
            _offerRepository = offerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var offers = await _offerRepository.GetAllAsync();
            return Ok(offers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _offerRepository.GetByIdAsync(id);

            if (offer == null)
            {
                return NotFound(new { message = "Teklif bulunamadı" });
            }

            return Ok(offer);
        }
    }
}