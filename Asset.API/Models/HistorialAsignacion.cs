using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class HistorialAsignacion
{
    public int IdHistorial { get; set; }

    public int IdActivo { get; set; }

    public int IdEmpleado { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    public virtual Activo IdActivoNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
}
