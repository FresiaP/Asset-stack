using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }

    public bool? Activo { get; set; }

    public int? IdEmpleado { get; set; }

    public virtual ICollection<Actum> Acta { get; set; } = new List<Actum>();

    public virtual ICollection<BajaActivo> BajaActivos { get; set; } = new List<BajaActivo>();

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual ICollection<UsuarioRol> UsuarioRols { get; set; } = new List<UsuarioRol>();
}
