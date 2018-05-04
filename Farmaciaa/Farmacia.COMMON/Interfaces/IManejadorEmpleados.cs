using Farmacia.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Interfaces
{
    public interface IManejadorEmpleados: IManejadorGenerico<Empleado>
    {
		List<Empleado> EmpleadosPorMatricula(string matricula);
    }
}
