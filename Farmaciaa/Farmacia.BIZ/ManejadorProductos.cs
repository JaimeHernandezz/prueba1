using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacia.BIZ
{
	public class ManejadorProductos : IManejadorProducto
	{
		IRepositorio<Productos> repositorio;
		public ManejadorProductos(IRepositorio<Productos> repositorio)
		{
			this.repositorio = repositorio;
		}
		public List<Productos> Listar => repositorio.Read;

		public bool Agregar(Productos entidad)
		{
			return repositorio.Create(entidad);
		}

		public Productos BuscarPorId(string id)
		{
			return Listar.Where(e => e.Id == id).SingleOrDefault();
		}

		public bool Eliminar(string id)
		{
			return repositorio.Delete(id);
		}

		public bool Modificar(Productos entidad)
		{
			return repositorio.Update(entidad);
		}

		public List<Productos> ProductosDeCategoria(string categoria)
		{
			return Listar.Where(e => e.Categoria == categoria).ToList();
		}
	}
}
