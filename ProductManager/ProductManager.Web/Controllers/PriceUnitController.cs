using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProductManager.DataLayer.Repositories;
using ProductManager.Web.Filters;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{
    [AdministratorFilter]
    public class PriceUnitController : Controller
    {
        private readonly IPricePlanRepository _pricePlanRepository;
        private readonly IPriceUnitRepository _priceUnitRepository;

        public PriceUnitController(IPricePlanRepository pricePlanRepository, IPriceUnitRepository priceUnitRepository)
        {
            _pricePlanRepository = pricePlanRepository;
            _priceUnitRepository = priceUnitRepository;
        }

        // GET: PriceUnit
        public async Task<ActionResult> EditPriceUnit(int pricePlanId, int priceUnitId)
        {
            var currentPricePlan = await _pricePlanRepository.GetByIdAsync(pricePlanId);
            var currentPriceUnitViewModel =
                currentPricePlan.PriceUnits.Where(x => x.Id == priceUnitId).Select(y => new PriceUnitViewModel
                {
                    Id = y.Id,
                    Width = y.Width,
                    Height = y.Height,
                    Price = y.Price,
                    PricePlanId = pricePlanId

                }).Single();
            return View(currentPriceUnitViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> EditPriceUnit(PriceUnitViewModel priceUnitViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentPricePlan = await _pricePlanRepository.GetByIdAsync(priceUnitViewModel.PricePlanId);
                var currentPriceUnit = currentPricePlan.PriceUnits.Single(x => x.Id == priceUnitViewModel.Id);
                currentPriceUnit.Height = priceUnitViewModel.Height;
                currentPriceUnit.Width = priceUnitViewModel.Width;
                currentPriceUnit.Price = priceUnitViewModel.Price;

                await _pricePlanRepository.Update(currentPricePlan);

                return RedirectToAction("Details", "PricePlan", new { Id = priceUnitViewModel.PricePlanId });
            }
            return View(priceUnitViewModel);
        }

        public async Task<ActionResult> DeletePriceUnit(int pricePlanId, int priceUnitId)
        {
            await _priceUnitRepository.Remove(priceUnitId);
            return RedirectToAction("Details", "PricePlan", new { Id = pricePlanId });
        }






    }
}