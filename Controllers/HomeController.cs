using Microsoft.AspNetCore.Mvc;
using PRUEBA.Models;
using System.Diagnostics;

namespace PRUEBA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            TrenModel model = new TrenModel();
            return View(model);
        }

        [HttpPost]

        public IActionResult AgregarVagon(List<int> Vagones, IFormCollection myform, string submit, string nroVagon)
        {
            int NroVagon;
            TrenModel model = new TrenModel();

            if (!string.IsNullOrEmpty(nroVagon))
            {

                NroVagon = Int32.Parse(nroVagon);

                if (Vagones.Count > 0)
                {
                    // ENCONTRE LA MANERA DE RECONOCER QUE BOTON APRETA SIN PASARLE PARAMETROS ;)

                    switch (submit)
                    {
                        case "Izquierda":
                            Vagones.Insert(0, NroVagon);
                            break;
                        case "Derecha":
                            Vagones.Insert(Vagones.IndexOf(Vagones.Last()) + 1, NroVagon);
                            break;
                    }

                }
                else
                    Vagones.Add(NroVagon);

                model.Vagones = Vagones;
                model.Tren = ArmarTren(Vagones);

            }


            return View("Index", model);
        }

        [HttpPost]

        public IActionResult RemoverVagon( List<int> Vagones, string submit)
        {

            if (Vagones.Count() > 0)
            {
                switch (submit)
                {
                    case "Izquierda":
                        Vagones.RemoveAt(0);
                        break;
                    case "Derecha":
                        Vagones.RemoveAt(Vagones.IndexOf(Vagones.Last()));
                        break;
                }

            }

            TrenModel model = new TrenModel();

            model.Vagones = Vagones;
            model.Tren = ArmarTren(Vagones);

            return View("Index", model);
        }


        public string ArmarTren(List<int> Vagones)
        {
            string Tren = string.Empty;

            foreach (int Vagon in Vagones)
                Tren += Vagon + "-";

            return Tren;

        }


    }
}