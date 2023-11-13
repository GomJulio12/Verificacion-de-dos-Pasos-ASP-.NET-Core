using Microsoft.AspNetCore.Mvc;

using AplicacionWeb.Models;
using AplicacionWeb.Servicios.Contrato;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;



namespace AplicacionWeb.Controllers
{
    public class AuthController : Controller
    {
        public Dictionary<string, string> DiccionarioCifrador = new Dictionary<string, string>();
        private Dictionary<string, string> DescifrarDiccionario = new Dictionary<string, string>();
        private readonly IUsuarioService _usuarioServicio;
        //CREAMOS UNA METODO DONDE VAN TODOS LOS DICCIONARIO DE CADA LETRA A ENCRIPTAR
        public AuthController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;

            // Define el diccionario de encriptación usando un solo bucle y una función para generar los pares
            string[] Caracteres = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                                    "K", "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S",
                                    "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c",
                                    "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
                                    "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v",
                                    "w", "x", "y", "z", "1", "2", "3", "4", "5", "6",
                                    "7", "8", "9", "0", " " };

            string[] EncriptarCaracteres = { "YC", "ZD", "AC", "BF", "CG", "DH", "EI", "FJ", "GK", "HL",
                                             "IM", "JN", "KÑ", "LO", "MP", "NQ", "ÑR", "OS", "PT", "QU",
                                             "RV", "SW", "TX", "UY", "VZ", "WA", "XB", "yc", "zd", "ac", 
                                             "bf", "cg", "dh", "ei", "fj", "gk", "hl", "im", "jn", "kñ",
                                             "lo", "mp", "nq", "ñr", "os", "pt", "qu", "rv", "sw", "tx",
                                             "uy", "vz", "wa", "xb", "93", "04", "15", "26", "37", "48",
                                             "59", "60", "71", "82", " " };

            // Crea el diccionario de encriptación con los arrays definidos
            for (int i = 0; i < Caracteres.Length; i++)
            {
                DiccionarioCifrador.Add(Caracteres[i], EncriptarCaracteres[i]);
            }

            // Genera el diccionario de desencriptación invirtiendo el diccionario de encriptación
            foreach (var entry in DiccionarioCifrador)
            {
                DescifrarDiccionario.Add(entry.Value, entry.Key);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        //METODO DONDE HACE LA ENCRIPTACION
        public string EncriptarPassword(string cadena)
        {
            string EncriptarTexto = "";
            foreach (char character in cadena)
            {
                string charKey = character.ToString();
                if (DiccionarioCifrador.ContainsKey(charKey))
                {
                    EncriptarTexto += DiccionarioCifrador[charKey];
                }
                else
                {
                    EncriptarTexto += charKey; //MANTIENE LOS CARACTERES NO ENCRIPTADAS
                }
            }
            return EncriptarTexto;
        }


        [HttpPost]
        public async Task<IActionResult> Register(Usuario modelo)
        {
            modelo.Password = EncriptarPassword(modelo.Password);

            Usuario usuario_creado = await _usuarioServicio.SaveUsuario(modelo);

            if (usuario_creado.IdUsuario > 0)
                return RedirectToAction("Login", "Auth");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string password)
        {

            Usuario usuario_encontrado = await _usuarioServicio.GetUsuario(correo, EncriptarPassword(password));

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuario_encontrado.Nombreusuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
    }
}
