using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class ActivoComponente
{
    public int Id { get; set; }

    public int ActivoPadreId { get; set; }

    public int ActivoHijoId { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public DateTime? FechaRemocion { get; set; }

    public virtual Activo ActivoHijo { get; set; } = null!;

    public virtual Activo ActivoPadre { get; set; } = null!;
}
