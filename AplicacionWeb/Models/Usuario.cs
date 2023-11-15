using System;
using System.Collections.Generic;

namespace AplicacionWeb.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Nombreusuario { get; set; }

    public string? Correo { get; set; }

    public string? Password { get; set; }
    public bool TwoFactorEnabled { get; set; }
}
