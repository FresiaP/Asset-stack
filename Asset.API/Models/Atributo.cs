using System;
using System.Collections.Generic;

namespace Asset.API.Models;

public partial class Atributo
{
    public int IdAtributo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ActivoAtributo> ActivoAtributos { get; set; } = new List<ActivoAtributo>();
}
