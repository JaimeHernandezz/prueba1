using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacia.BIZ
{
	public class ManejadorCategorias : IManejadorCategorias
	{
		IRepositorio<Categorias> repositorio;
		public ManejadorCategorias(IRepositorio<Categorias> repositorio)
		{
			this.repositorio = repositorio;
		}
		public List<Categorias> Listar => repositorio.Read;

		public bool Agregar(Categorias entidad)
		{
			return repositorio.Create(entidad);
		}

		public Categorias BuscarPorId(string id)
		{
			return Listar.Where(e => e.Id == id).SingleOrDefault();
		}

		public List<Categorias> Categorias(string tipodecategoria)
		{
			return Listar.Where(e => e.TipoDeCategoria == tipodecategoria).ToList();
		}

		public bool Eliminar(string id)
		{
			return repositorio.Delete(id);
		}

		public bool Modificar(Categorias entidad)
		{
			return repositorio.Update(entidad);
		}
	}
}
