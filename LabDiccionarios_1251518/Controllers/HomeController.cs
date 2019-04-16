using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabDiccionarios_1251518.Models;
namespace LabDiccionarios_1251518.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new List<Album>());
        }
        static List<Album> album = new List<Album>();
        static Dictionary<string, MiAlbum> mialbum = new Dictionary<string, MiAlbum>();
        static Dictionary<string, Album> albumVacio = new Dictionary<string, Album>();

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            string rutaArchivo = string.Empty;
            //el siguiente if permite seleccionar un archivo en específico
            if (postedFile != null)
            {
                string ruta = Server.MapPath("~/ArchivosBase/");
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                rutaArchivo = ruta + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(rutaArchivo);
                //se lee el archivo del inventario
                string datosCromo = System.IO.File.ReadAllText(rutaArchivo);
                //se tomara y fragmentara cada una de las lineas del archivo
                foreach (string registro in datosCromo.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(registro))
                    {
                        album.Add(new Album
                        {
                            id = Convert.ToInt32(registro.Split(',')[0]),
                            equipo = registro.Split(',')[1],
                            nombre = registro.Split(',')[2]

                        });
                        //el nombre será la llave a utilizar, ya que es unico
                        albumVacio.Add(registro.Split(',')[2], new Album
                        {

                            id = Convert.ToInt32(registro.Split(',')[0]),
                            equipo = registro.Split(',')[1],
                            nombre = registro.Split(',')[2]
                        });
                        //se agregan los elementos al diccionario de nuestro album
                        mialbum.Add(registro.Split(',')[2], new MiAlbum
                            {

                                id = Convert.ToInt32(registro.Split(',')[0]),
                                equipo = registro.Split(',')[1],
                                nombre = registro.Split(',')[2],
                                disponible = registro.Split(',')[3],
                                repetidas = Convert.ToInt32(registro.Split(',')[4])
                            });
                        
                    }
                }
            }
            return View(album);


        }

        public ActionResult mostrar()
        {
            
            var ValoresVacio = albumVacio.Values.ToArray();
            return View(ValoresVacio);
        }
        public ActionResult mostrarAlbum()
        {
            var listaValores = mialbum.Values.ToArray();
            return View(listaValores);
        }
    }
}