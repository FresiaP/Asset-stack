using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class EstadoFisico
{
    public int IdEstadoFisico { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ActaDetalle> ActaDetalleIdEstadoEntregaNavigations { get; set; } = new List<ActaDetalle>();

    public virtual ICollection<ActaDetalle> ActaDetalleIdEstadoRecepcionNavigations { get; set; } = new List<ActaDetalle>();
}
