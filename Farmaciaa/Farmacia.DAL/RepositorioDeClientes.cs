using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacia.DAL
{
	public class RepositorioDeClientes : IRepositorio<Cliente>
	{
		private string DBName = "Farmacia.db";
		private string TableName = "Clientes";
		public List<Cliente> Read {
			get
			{
				List<Cliente> datos = new List<Cliente>();
				using (var db = new LiteDatabase(DBName))
				{
					datos = db.GetCollection<Cliente>(TableName).FindAll().ToList();
				}
				return datos;
			}

		}

		public bool Create(Cliente entidad)
		{
			entidad.Id = Guid.NewGuid().ToString();
			try
			{
				using (var db = new LiteDatabase(DBName))
				{
					var coleccion = db.GetCollection<Cliente>(TableName);
					coleccion.Insert(entidad);
				}
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Delete(string id)
		{
			try
			{
				int r;
				using (var db = new LiteDatabase(DBName))
				{
					var coleccion = db.GetCollection<Cliente>(TableName);
					r = coleccion.Delete(e => e.Id == id);
				}
				return r > 0;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Update(Cliente entidadModificada)
		{
			try
			{
				using (var db = new LiteDatabase(DBName))
				{
					var coleccion = db.GetCollection<Cliente>(TableName);
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
