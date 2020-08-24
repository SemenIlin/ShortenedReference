using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShortenedReferenceBLL.Interfaces;
using ShortenedReferenceCommon.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReferenceInfoService<ReferenceInfo> _referenceInfoService;
        private readonly ICounterService<Counter> _counterService;

        public HomeController(IReferenceInfoService<ReferenceInfo> referenceInfoService, 
                              ICounterService<Counter> counterService)
        {
            _referenceInfoService = referenceInfoService;
            _counterService = counterService;
        }

        [HttpGet]
        public async Task<ActionResult> ClickOnALink(string shortenedReference)
        {
            try
            {
                var reference = await _referenceInfoService.Find(shortenedReference, false);
                if(reference == null)
                {
                    return View("NotFound");
                }

                await _counterService.Update(reference.Id);

                return View(reference);
            }
            catch
            {
                return View("NotFound");
            }
        }

        public async Task<IActionResult> Index()
        {
            List<ReferenceInfo> references;
            try
            {
                references = await _referenceInfoService.GetAll();
            }
            catch
            {
                references = null;
            }

            return View(references);
        }

        [HttpGet]
        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ReferenceInfo referenceInfo)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            var result = await _referenceInfoService.Create(referenceInfo);
            if (result == null)
            {
                return View("Create");
            }

            var counter = await _counterService.Get(result.Id);
            if (counter != null)
            {
                return RedirectToAction("ReadyLink", result);
            }

            counter = new Counter()
            {
                ReferenceInfoId = result.Id
            };
            await _counterService.Create(counter);

            return RedirectToAction("ReadyLink", result);
        }

        [HttpGet]
        public IActionResult ReadyLink(ReferenceInfo referenceInfo)
        {
            return View(referenceInfo);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                var reference = await _referenceInfoService.Get(id.Value);

                return View(reference);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int? id)
        {
            if (!id.HasValue)
            {
                return View("NotFound");
            }
            try
            {
                var reference = await _referenceInfoService.Get(id.Value); 
                if (reference == null)
                {
                    return View("NotFound");
                }

                await _referenceInfoService.Remove(id.Value);
                return RedirectToAction("Main");
            }
            catch 
            {
                return View("NotFound");
            }
        }
    }
}