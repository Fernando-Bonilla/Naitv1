﻿using Microsoft.AspNetCore.Mvc;
using Naitv1.Models;
using Naitv1.Data;
using Naitv1.Helpers;

namespace Naitv1.Controllers
{
    public class ActividadesController : Controller
    {
        private readonly AppDbContext _context;

        public ActividadesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string mensajeDelAnfitrion, string tipoActividad, float lat, float lon, float? latSuperAdmin, float? lonSuperAdmin)
        {
            Usuario usuario = UsuarioLogueado.Usuario(HttpContext.Session);
            Actividad actividad = new Actividad();

            actividad.MensajeDelAnfitrion = mensajeDelAnfitrion;
            actividad.TipoActividad = tipoActividad;
            if (latSuperAdmin != null && lonSuperAdmin != null && UsuarioLogueado.esSuperAdmin(HttpContext.Session))
            {
                actividad.Lat = (float) latSuperAdmin;
                actividad.Lon = (float) lonSuperAdmin;
            }
            else
            {
                actividad.Lat = lat;
                actividad.Lon = lon;
            }
            actividad.AnfitrionId = usuario.Id;

            _context.Actividades.Add(actividad);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
