using MvcNetFramework2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcNetFramework2.Controllers
{
    public class MascotaController : Controller
    {

        const string nombreSession = "ListadoMascotas";
        List<Mascota> ListaMascotas;

        // Para cargar una paguina por GET para enviar o procesar una paguina POST
        // GET: Mascota
        public ActionResult Index()
        {

            /*Para enviar informacion del controlador a la vista usamos: 
             * ViewBag(enviandole un objeto) o trabajando con vistas fuertemente tipiadas(uniendole a un modelo especifico)
             */
            if (Session[nombreSession] == null) {
                var listadoMascotas = ObtenerMascotas();//Este metodo podria ir a una BD o un API lo importante es que me envie un listado Mascotas
                Session[nombreSession] = listadoMascotas;
                return View(listadoMascotas);
            }
            else {
                var listadoMascotas = (List<Mascota>)Session[nombreSession];
                return View(listadoMascotas.OrderBy( x => x.Id ).ToList());
            }
            
        }

        //Cada accion de este tipo se relaciona con una vista
        [HttpGet]
        public ActionResult Nuevo() {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(Mascota mascota)
        {
            var listadoMascotas = (List<Mascota>)Session[nombreSession];
            listadoMascotas.Add(mascota);
            //Session[nombreSession] = listadoMascotas;

            return RedirectToAction("Index");
        }



        private List<Mascota> ObtenerMascotas() {
            //var: solo funciona en un metodo como variable local y temporal de un metodo, como una variable principal no vale poner (var)
            var listado = new List<Mascota>();
            listado.Add(new Mascota { Id = 1, Nombre = "Trusky", Raza = "Perro", Edad = 5, NombreDueno = "Ivan", });
            listado.Add(new Mascota { Id = 2, Nombre = "Chone", Raza = "Gato", Edad = 6, NombreDueno = "Vero", });
            listado.Add(new Mascota { Id = 3, Nombre = "Negro", Raza = "Loro", Edad = 5, NombreDueno = "Julian", });
            return listado;
        }

        [HttpGet]
        public ActionResult Actualizar(int id) {
            var listadoMascotas = (List<Mascota>)Session[nombreSession];
            var mascota = listadoMascotas.FirstOrDefault(x => x.Id == id);

            return View(mascota);
        }

        [HttpPost]
        public ActionResult Actualizar(Mascota mascota) {
            var listadoMascotas = (List<Mascota>)Session[nombreSession];
            var mascotaActualizar = listadoMascotas.FirstOrDefault(x => x.Id == mascota.Id);
            listadoMascotas.Remove(mascotaActualizar);
            listadoMascotas.Add(mascota);
            //foreach (var aux in listadoMascotas) {
            //    if (aux.Id == mascota.Id) {
            //        aux.Nombre = "d";
            //    }
            //}
            //Session[nombreSession] = listadoMascotas;

            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id) {
            var listadoMascotas = (List<Mascota>)Session[nombreSession];
            var mascotaEliminar = listadoMascotas.FirstOrDefault(x => x.Id == id);
            listadoMascotas.Remove(mascotaEliminar);
            return RedirectToAction("Index");
        }

    }
}