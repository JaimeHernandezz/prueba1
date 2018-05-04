using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacia.DAL
{
	public class RepositorioDeProducto : IRepositorio<Productos>
	{
		private string DBName = "Farmacia.db";
		private string TableName = "Productos";
		public List<Productos> Read {
			get
			{
				List<Productos> datos = new List<Productos>();
				using (var db = new LiteDatabase(DBName))
				{
					datos = db.GetCollection<Productos>(TableName).FindAll().ToList();
				}
				return datos;
			}
		}

		public bool Create(Productos entidad)
		{
			entidad.Id = Guid.NewGuid().ToString();
			try
			{
				using (var db = new LiteDatabase(DBName))
				{
					var coleccion = db.GetCollection<Productos>(TableName);
					coleccion.Insert(entidad);
				}
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Delete(string	id)
		{
			try
			{
				int r;
				using (var db = new LiteDatabase(DBName))
				{
					var coleccion = db.GetCollection<Productos>(TableName);
					r = coleccion.Delete(e => e.Id == id);
				}
				return r > 0;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Update(Productos entidadModificada)
		{
			try
			{
				using (var db = new LiteDatabase(DBName))
				{
					var coleccion = db.GetCollection<Productos>(TableName);
					coleccion.Update(entidadModificada);
				}
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
