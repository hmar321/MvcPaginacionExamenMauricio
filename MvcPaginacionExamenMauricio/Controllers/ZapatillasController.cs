using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using MvcPaginacionExamenMauricio.Models;
using MvcPaginacionExamenMauricio.Repositories;

namespace MvcPaginacionExamenMauricio.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Lista()
        {
            List<Zapatilla> zapatillas = await this.repo.GetZapatillasAsyc();
            return View(zapatillas);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            Zapatilla zapatilla = await this.repo.FindZapatillaAsyn(id);
            ImagenPaginacion model = await this.repo.GetImagenPaginacionAsync(1, zapatilla.IdZapatilla);
            ViewData["POSICION"] = 1;
            ViewData["REGISTROS"] = model.Registros;
            ViewData["SIGUIENTE"] = 2;
            ViewData["ANTERIOR"] = 1;
            return View(zapatilla);
        }

        public async Task<IActionResult> _Paginacion(int posicion, int idzapatilla)
        {
            ImagenPaginacion model = await this.repo.GetImagenPaginacionAsync(posicion, idzapatilla);
            ImagenZapatilla imagen = model.ImagenZapatilla;
            ViewData["POSICION"] = posicion;
            ViewData["REGISTROS"] = model.Registros;
            ViewData["IMAGEN"] = imagen.Imagen;
            return PartialView("_Paginacion");
        }

        public async Task<IActionResult> Agregar()
        {
            List<Zapatilla> zapatillas = await this.repo.GetZapatillasAsyc();
            return View(zapatillas);
        }
        [HttpPost]
        public async Task<IActionResult> Agregar(List<string> imagen, int idzapatilla)
        {
            foreach (string img in imagen)
            {
                await this.repo.InsertImagen(idzapatilla, img);
            }
            List<Zapatilla> zapatillas = await this.repo.GetZapatillasAsyc();
            ViewData["MENSAJE"] = "Insertado con éxito.";
            return View(zapatillas);
        }
    }
}
