using Microsoft.AspNetCore.Mvc;
using ShortenedReferenceBLL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Mappers;
using System.Linq;
using ShortenedReferenceBLL.ModelDtos;

namespace WebApplication1.Controllers
{
    public class ReferenceController : Controller
    {
        private readonly IReferenceInfoService<ReferenceInfoDto> _referenceInfoService;

        public ReferenceController(IReferenceInfoService<ReferenceInfoDto> referenceInfoService)
        {
            _referenceInfoService = referenceInfoService;
        }

        [HttpGet]
        [Route("{url:maxlength(100)}")]
        public async Task<ActionResult> TransitionShortReference(string url)
        {
            try
            {
                var reference = await _referenceInfoService.Find(url, false);
                if (reference == null)
                {
                    return View("NotFound");
                }

                await _referenceInfoService.Update(reference.Id);

                return Redirect(reference.LongReference);
            }
            catch
            {
                return View("NotFound");
            }
        }

        public async Task<ViewResult> Index()
        {
            List<ReferenceInfoViewModel> references;
            try
            {
                references = (await _referenceInfoService.GetAll()).MapToListViewModels().ToList();
            }
            catch
            {
                references = null;
            }

            ViewBag.Url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";

            return View(references) ;
        }

        [HttpGet]
        public ViewResult Create()
        {            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ReferenceInfoViewModel referenceInfo)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            var result = await _referenceInfoService.Create(referenceInfo.MapToDtoModel());
            if (result == null)
            {
                return View("Create");
            }

            return RedirectToAction("ReadyLink", result);
        }

        [HttpGet]
        public ViewResult ReadyLink(ReferenceInfoViewModel referenceInfo)
        {
            ViewBag.Url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
            return View(referenceInfo);
        }

        [HttpGet]
        public async Task<ViewResult> Delete(int? id)
        {
            try
            {
                var reference = (await _referenceInfoService.Get(id.Value)).MapToViewModel();
                ViewBag.Url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";

                return View(reference);
            }
            catch
            {
                return View("NotFound");
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
                return RedirectToAction("Index");
            }
            catch 
            {
                return View("NotFound");
            }
        }
    }
}