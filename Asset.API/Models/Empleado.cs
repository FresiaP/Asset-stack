using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Email { get; set; }

    public int? IdDepartamento { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Actum> Acta { get; set; } = new List<Actum>();

    public virtual ICollection<HistorialAsignacion> HistorialAsignacions { get; set; } = new List<HistorialAsignacion>();

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
