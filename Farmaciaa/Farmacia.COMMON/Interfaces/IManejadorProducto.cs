﻿using Farmacia.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Interfaces
{
    public interface IManejadorProducto: IManejadorGenerico<Productos>
    {
		List<Productos> ProductosDeCategoria(string categoria);
    }
}
