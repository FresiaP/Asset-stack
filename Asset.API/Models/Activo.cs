using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class Activo
{
    public int IdActivo { get; set; }

    public string CodigoInventario { get; set; } = null!;

    public string NombreActivo { get; set; } = null!;

    public int IdTipoActivo { get; set; }

    public int IdModelo { get; set; }

    public string? Serie { get; set; }

    public int IdEstadoActivo { get; set; }

    public int IdTipoPropiedad { get; set; }

    public int? IdContrato { get; set; }

    public DateOnly? FechaAdquisicion { get; set; }

    public string? Observaciones { get; set; }

    public virtual ICollection<ActaDetalle> ActaDetalles { get; set; } = new List<ActaDetalle>();

    public virtual ICollection<ActivoAtributo> ActivoAtributos { get; set; } = new List<ActivoAtributo>();

    public virtual ICollection<ActivoComponente> ActivoComponenteActivoHijos { get; set; } = new List<ActivoComponente>();

    public virtual ICollection<ActivoComponente> ActivoComponenteActivoPadres { get; set; } = new List<ActivoComponente>();

    public virtual ICollection<BajaActivo> BajaActivos { get; set; } = new List<BajaActivo>();

    public virtual ICollection<HistorialAsignacion> HistorialAsignacions { get; set; } = new List<HistorialAsignacion>();

    public virtual Contrato? IdContratoNavigation { get; set; }

    public virtual EstadoActivo IdEstadoActivoNavigation { get; set; } = null!;

    public virtual Modelo IdModeloNavigation { get; set; } = null!;

    public virtual TipoActivo IdTipoActivoNavigation { get; set; } = null!;

    public virtual TipoPropiedad IdTipoPropiedadNavigation { get; set; } = null!;
}
