using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class Modelo
{
    public int IdModelo { get; set; }

    public string NombreModelo { get; set; } = null!;

    public int IdMarca { get; set; }

    public int IdTipoActivo { get; set; }

    public virtual ICollection<Activo> Activos { get; set; } = new List<Activo>();

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual TipoActivo IdTipoActivoNavigation { get; set; } = null!;
}
