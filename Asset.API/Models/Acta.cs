using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class Actum
{
    public int IdActa { get; set; }

    public string Tipo { get; set; } = null!;

    public int IdEmpleado { get; set; }

    public int IdUsuario { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Observaciones { get; set; }

    public virtual ICollection<ActaDetalle> ActaDetalles { get; set; } = new List<ActaDetalle>();

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
