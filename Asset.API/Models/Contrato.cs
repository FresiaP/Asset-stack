using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class Contrato
{
    public int IdContrato { get; set; }

    public int IdProveedor { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public decimal? CostoMensual { get; set; }

    public string? Observaciones { get; set; }

    public virtual ICollection<Activo> Activos { get; set; } = new List<Activo>();

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}
