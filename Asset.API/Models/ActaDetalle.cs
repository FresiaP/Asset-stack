using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class ActaDetalle
{
    public int IdActaDetalle { get; set; }

    public int IdActa { get; set; }

    public int IdActivo { get; set; }

    public int? IdEstadoEntrega { get; set; }

    public int? IdEstadoRecepcion { get; set; }

    public virtual Actum IdActaNavigation { get; set; } = null!;

    public virtual Activo IdActivoNavigation { get; set; } = null!;

    public virtual EstadoFisico? IdEstadoEntregaNavigation { get; set; }

    public virtual EstadoFisico? IdEstadoRecepcionNavigation { get; set; }
}
