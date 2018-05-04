﻿using Farmacia.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Interfaces
{
    public interface IManejadorCategorias: IManejadorGenerico<Categorias>
    {
		List<Categorias> Categorias(string tipodecategoria);
    }
}
