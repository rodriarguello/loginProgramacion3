using LoginProgramacion3.Models;
using LoginProgramacion3.Models.ViewModels;
using LoginProgramacion3.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LoginProgramacion3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly Seguridad _seguridad;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, Seguridad seguridad)
        {
            _context = context;
            _logger = logger;
            _seguridad = seguridad;
        }

        public IActionResult Index()
        {
            string title = "ASDAD";
            ViewData["Title"] = title;
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(CredencialesViewModel credencialesViewModel)
        {
            

            if(ModelState.IsValid)
            {
                var usuario = await _context.Usuarios.Where(u => u.Email.ToLower() == credencialesViewModel.Email.ToLower()).FirstOrDefaultAsync();

                if (usuario == null)
                {
                    ViewBag.credInvalidas = true;
                    return View();

                }

                var resultado = _seguridad.VericarPassword(usuario, credencialesViewModel.Password);

                if (!resultado)
                {
                    ViewBag.credInvalidas = true;
                    return View();
                }
                usuario.Password = "";
                TempData["nombreUsuario"] = usuario.Nombre;
                return RedirectToAction("Dashboard");
               
            }
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(UsuarioViewModel usuarioViewModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Usuario nuevoUsuario = new ();

                    nuevoUsuario.Nombre = usuarioViewModel.Nombre;
                    nuevoUsuario.Email = usuarioViewModel.Email;

                    nuevoUsuario.Password = _seguridad.EncriptarPassword(usuarioViewModel.Password);

                    _context.Add(nuevoUsuario);
                    await _context.SaveChangesAsync();

                    TempData["registroExitoso"] = true;

                    return RedirectToAction("Login","Home");
                }
                catch (Exception)
                {
                    ViewBag.error = true;
                    return View();
                }
            }

            ViewBag.error = true;
            return View();
        }

        public IActionResult Dashboard()
        {
            if (TempData["nombreUsuario"] != null)
            {
                ViewBag.nombre = TempData["nombreUsuario"];
                
                return View();

            }


            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}