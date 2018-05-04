using Farmacia.BIZ;
using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Interfaces;
using Farmacia.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Farmacia.GUI.Administrador
{
	/// <summary>
	/// Lógica de interacción para MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		enum accion
		{
			Nuevo,
			Editar
		}

		IManejadorCategorias manejadorCategorias;
		IManejadorClientes manejadorClientes;
		IManejadorProducto manejadorProducto;
		IManejadorEmpleados manejadorEmpleados;

		accion accionCategoria;
		accion accionClientes;
		accion accionProductos;
		accion accionEmpleados;
		public MainWindow()
		{
			InitializeComponent();

			manejadorCategorias = new ManejadorCategorias(new RepositorioDeCategorias());
			manejadorClientes = new ManejadorClientes(new RepositorioDeClientes());
			manejadorProducto = new ManejadorProductos(new RepositorioDeProducto());
			manejadorEmpleados = new ManejadorEmpleados(new RepositorioDeEmpleados());

			PonerBotonesCategoriaEnEdicion(false);
			LimpiarCamposDeCategoria();
			ActualizarTablaCategoria();

			PonerBotonesClientesEnEdicion(false);
			LimpiarCamposClientes();
			ActualizarTablaClientes();

			PonerBotonesProductosEnEdicion(false);
			LimpiarCamposDeProductos();
			ActualizarTablaProdutos();

			PonerBotonesDeEmpleadosEnEdicion(false);
			LimpiarCamposDeEmpleados();
			ActualizarTablaEmpleados();


		}

		private void ActualizarTablaEmpleados()
		{
			dtgEmpleado.ItemsSource = null;
			dtgEmpleado.ItemsSource = manejadorEmpleados.Listar;
		}

		private void LimpiarCamposDeEmpleados()
		{
			txbEmpleadoNombre.Clear();
			txbEmpleadoApellido.Clear();
			txbEmpleadoId.Text = "";
			txbEmpleadoDireccion.Clear();
			txbEmpleadoTelefono.Clear();
			txbEmpleadoMatricula.Clear();
		}

		private void PonerBotonesDeEmpleadosEnEdicion(bool value)
		{
			btnEmpleadoCancelar.IsEnabled = value;
			btnEmpleadoEditar.IsEnabled = !value;
			btnEmpleadoEliminar.IsEnabled = !value;
			btnEmpleadoGuardar.IsEnabled = value;
			btnEmpleadoNuevo.IsEnabled = !value;
		}

		private void ActualizarTablaProdutos()
		{
			dtgProductos.ItemsSource = null;
			dtgProductos.ItemsSource = manejadorProducto.Listar;
		}

		private void LimpiarCamposDeProductos()
		{
			txbProductosNombre.Clear();
			txbProductosCategoria.Clear();
			txbProductosId.Text = "";
			txbProductosDescripcion.Clear();
			txbProductosPrecioCompra.Clear();
			txbProductosPrecioVenta.Clear();
		}

		private void PonerBotonesProductosEnEdicion(bool value)
		{
			btnProductosCancelar.IsEnabled = value;
			btnProductosEditar.IsEnabled = !value;
			btnProductosEliminar.IsEnabled = !value;
			btnProductosGuardar.IsEnabled = value;
			btnProductosNuevo.IsEnabled = !value;
		}

		private void ActualizarTablaClientes()
		{
			dtgCliente.ItemsSource = null;
			dtgCliente.ItemsSource = manejadorClientes.Listar;
		}

		private void LimpiarCamposClientes()
		{
			txbClienteNombre.Clear();
			txbClienteApellido.Clear();
			txbClienteId.Text = "";
			txbClienteTelefono.Clear();
			txbClienteDireccion.Clear();
			txbClienteEstacionamiento.Clear();
		}

		private void PonerBotonesClientesEnEdicion(bool value)
		{
			btnClienteCancelar.IsEnabled = value;
			btnClienteEditar.IsEnabled = !value;
			btnClienteEliminar.IsEnabled = !value;
			btnClienteGuardar.IsEnabled = value;
			btnClienteNuevo.IsEnabled = !value;
		}

		private void ActualizarTablaCategoria()
		{
			dtgCategoria.ItemsSource = null;
			dtgCategoria.ItemsSource = manejadorCategorias.Listar;
		}

		private void LimpiarCamposDeCategoria()
		{
			txbCategoriaTipoDeCategoria.Clear();
			txbCategoriaId.Text = "";
		}

		private void PonerBotonesCategoriaEnEdicion(bool value)
		{
			btnCategoriaCancelar.IsEnabled = value;
			btnCategoriaEditar.IsEnabled = !value;
			btnCategoriaEliminar.IsEnabled = !value;
			btnCategoriaGuardar.IsEnabled = value;
			btnCategoriaNuevo.IsEnabled = !value;
		}

		private void btnCategoriaNuevo_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeCategoria();
			PonerBotonesCategoriaEnEdicion(true);
			accionCategoria = accion.Nuevo;
		}

		private void btnCategoriaEditar_Click(object sender, RoutedEventArgs e)
		{
			Categorias cat= dtgCategoria.SelectedItem as Categorias;
			if (cat != null)
			{
				txbCategoriaId.Text = cat.Id;
				txbCategoriaTipoDeCategoria.Text = cat.TipoDeCategoria;
				accionCategoria = accion.Editar;
				PonerBotonesCategoriaEnEdicion(true);
			}
		}

		private void btnCategoriaGuardar_Click(object sender, RoutedEventArgs e)
		{
			if(accionCategoria == accion.Nuevo)
			{
				Categorias cat = new Categorias()
				{
					TipoDeCategoria = txbCategoriaTipoDeCategoria.Text
				};
				if (manejadorCategorias.Agregar(cat))
				{
					MessageBox.Show("Categoria agregada correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeCategoria();
					ActualizarTablaCategoria();
					PonerBotonesCategoriaEnEdicion(false);
				}
				else
				{
					MessageBox.Show("La Categoria no se pudo agregar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				Categorias cat = dtgCategoria.SelectedItem as Categorias;
				cat.TipoDeCategoria = txbCategoriaTipoDeCategoria.Text;
				if (manejadorCategorias.Modificar(cat))
				{
					MessageBox.Show("categoria modificada correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeCategoria();
					ActualizarTablaCategoria();
					PonerBotonesCategoriaEnEdicion(false);
				}
				else
				{
					MessageBox.Show("La Categoria no se pudo actualizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}

		}

		private void btnCategoriaCancelar_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeCategoria();
			PonerBotonesCategoriaEnEdicion(false);
		}

		private void btnCategoriaEliminar_Click(object sender, RoutedEventArgs e)
		{
			Categorias cat = dtgCategoria.SelectedItem as Categorias;
			if (cat != null)
			{
				if (MessageBox.Show("Realmente deseas eliminar esta Categoria?", "Farmacia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					if (manejadorCategorias.Eliminar(cat.Id))
					{
						MessageBox.Show("Categoria eliminada", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
						ActualizarTablaCategoria();
					}
					else
					{
						MessageBox.Show("No se pudo eliminar la Categoria", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
			}
		}

		
		private void btnProductosNuevo_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeProductos();
			PonerBotonesProductosEnEdicion(true);
			accionProductos = accion.Nuevo;
		}

		private void btnProductosEditar_Click(object sender, RoutedEventArgs e)
		{
			Productos emp = dtgProductos.SelectedItem as Productos;
			if (emp != null)
			{
				txbProductosId.Text = emp.Id;
				txbProductosNombre.Text = emp.Nombre;
				txbProductosCategoria.Text = emp.Categoria;
				txbProductosDescripcion.Text = emp.Descripcion;
				accionProductos = accion.Editar;
				PonerBotonesProductosEnEdicion(true);
			}
		}

		private void btnProductosGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (accionProductos == accion.Nuevo)
			{
				Productos emp = new Productos()
				{
					Nombre = txbProductosNombre.Text,
					Categoria = txbProductosCategoria.Text,
					Descripcion = txbProductosDescripcion.Text,
					PrecioCompra = txbProductosPrecioCompra.Text,
					PrecioVenta = txbProductosPrecioVenta.Text
				};
				if (manejadorProducto.Agregar(emp))
				{
					MessageBox.Show("Producto agregado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeProductos();
					ActualizarTablaProdutos();
					PonerBotonesProductosEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Producto no se pudo agregar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				Productos emp = dtgCliente.SelectedItem as Productos;
				emp.Nombre = txbProductosNombre.Text;
				emp.Categoria = txbProductosCategoria.Text;
				emp.Descripcion = txbProductosDescripcion.Text;
				emp.PrecioCompra = txbProductosPrecioCompra.Text;
				emp.PrecioVenta = txbProductosPrecioVenta.Text;

				if (manejadorProducto.Modificar(emp))
				{
					MessageBox.Show("El Producto modificado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeProductos();
					ActualizarTablaProdutos();
					PonerBotonesProductosEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Peoducto no se pudo actualizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}



		}

		private void btnProductosCancelar_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeProductos();
			PonerBotonesProductosEnEdicion(false);
		}

		private void btnProductosEliminar_Click(object sender, RoutedEventArgs e)
		{
			Productos emp = dtgProductos.SelectedItem as Productos;
			if (emp != null)
			{
				if (MessageBox.Show("Realmente deseas eliminar este productos?", "Farmacia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					if (manejadorProducto.Eliminar(emp.Id))
					{
						MessageBox.Show("Producto eliminado", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
						ActualizarTablaProdutos();
					}
					else
					{
						MessageBox.Show("No se pudo eliminar el producto", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
			}
		}








		private void btnClienteNuevo_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposClientes();
			PonerBotonesClientesEnEdicion(true);
			accionClientes = accion.Nuevo;
		}

		private void btnClienteEditar_Click(object sender, RoutedEventArgs e)
		{
			Cliente emp = dtgCliente.SelectedItem as Cliente;
			if (emp != null)
			{
				txbClienteId.Text = emp.Id;
				txbEmpleadoApellido.Text = emp.Apellido;
				txbClienteEstacionamiento.Text = emp.Telefono;
				txbClienteNombre.Text = emp.Nombre;
				accionClientes = accion.Editar;
				PonerBotonesClientesEnEdicion(true);
			}
		}

		private void btnClienteGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (accionClientes == accion.Nuevo)
			{
				Cliente emp = new Cliente()
				{
					Nombre = txbClienteNombre.Text,
					Apellido = txbClienteApellido.Text,
					Direccion = txbClienteDireccion.Text,
					Telefono = txbClienteTelefono.Text,
					Estacionamiento = txbClienteEstacionamiento.Text
				};
				if (manejadorClientes.Agregar(emp))
				{
					MessageBox.Show("Cliente agregado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposClientes();
					ActualizarTablaClientes();
					PonerBotonesClientesEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Cliente no se pudo agregar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				Cliente emp = dtgCliente.SelectedItem as Cliente;
				emp.Nombre = txbClienteNombre.Text;
				emp.Apellido = txbEmpleadoApellido.Text;
				emp.Direccion = txbClienteDireccion.Text;
				emp.Telefono = txbClienteTelefono.Text;
				emp.Estacionamiento = txbClienteEstacionamiento.Text;

				if (manejadorClientes.Modificar(emp))
				{
					MessageBox.Show("El Cliente modificado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
				    LimpiarCamposClientes();
					ActualizarTablaCategoria();
					PonerBotonesClientesEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Cliente no se pudo actualizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}

		}

		private void btnClienteCancelar_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposClientes();
			PonerBotonesClientesEnEdicion(false);
		}

		private void btnClienteEliminar_Click(object sender, RoutedEventArgs e)
		{
			Cliente emp = dtgCliente.SelectedItem as Cliente;
			if (emp != null)
			{
				if (MessageBox.Show("Realmente deseas eliminar este Cliente?", "Farmacia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					if (manejadorClientes.Eliminar(emp.Id))
					{
						MessageBox.Show("Cliente eliminado", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
						ActualizarTablaClientes();
					}
					else
					{
						MessageBox.Show("No se pudo eliminar el Cliente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
			}
		}





		private void btnEmpleadoNuevo_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeEmpleados();
			PonerBotonesDeEmpleadosEnEdicion(true);
			accionEmpleados = accion.Nuevo;
		}

		private void btnEmpleadoEditar_Click(object sender, RoutedEventArgs e)
		{
			Empleado emp = dtgEmpleado.SelectedItem as Empleado;
			if (emp != null)
			{
				txbEmpleadoId.Text = emp.Id;
				txbEmpleadoApellido.Text = emp.Apellido;
				txbEmpleadoDireccion.Text = emp.Direccion;
				txbEmpleadoNombre.Text = emp.Nombre;
				txbEmpleadoTelefono.Text = emp.Telefono;
				txbEmpleadoMatricula.Text = emp.Matricula;
				accionEmpleados = accion.Editar;
				PonerBotonesDeEmpleadosEnEdicion(true);
			}
		}

		private void btnEmpleadoGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (accionEmpleados == accion.Nuevo)
			{
				Empleado emp = new Empleado()
				{
					Nombre = txbEmpleadoNombre.Text,
					Apellido = txbEmpleadoApellido.Text,
					Direccion = txbEmpleadoDireccion.Text,
					Telefono = txbEmpleadoTelefono.Text,
					Matricula = txbEmpleadoMatricula.Text
				};
				if (manejadorEmpleados.Agregar(emp))
				{
					MessageBox.Show("Empleado agregado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeEmpleados();
					ActualizarTablaEmpleados();
					PonerBotonesDeEmpleadosEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Empleado no se pudo agregar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				Empleado emp = dtgCliente.SelectedItem as Empleado;
				emp.Nombre = txbEmpleadoNombre.Text;
				emp.Apellido = txbEmpleadoApellido.Text;
				emp.Direccion = txbEmpleadoDireccion.Text;
				emp.Telefono = txbEmpleadoTelefono.Text;
				emp.Matricula = txbEmpleadoMatricula.Text;

				if (manejadorEmpleados.Modificar(emp))
				{
					MessageBox.Show("El Empleado modificado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCamposDeEmpleados();
					ActualizarTablaEmpleados();
					PonerBotonesDeEmpleadosEnEdicion(false);
				}
				else
				{
					MessageBox.Show("El Empleado no se pudo actualizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}


		}

		private void btnEmpleadoCancelar_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCamposDeEmpleados();
			PonerBotonesDeEmpleadosEnEdicion(false);
		}

		private void btnEmpleadoEliminar_Click(object sender, RoutedEventArgs e)
		{
			Empleado emp = dtgEmpleado.SelectedItem as Empleado;
			if (emp != null)
			{
				if (MessageBox.Show("Realmente deseas eliminar este Empleado?", "Farmacia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					if (manejadorEmpleados.Eliminar(emp.Id))
					{
						MessageBox.Show("Empleado eliminado", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
						ActualizarTablaEmpleados();
					}
					else
					{
						MessageBox.Show("No se pudo eliminar el Empleado", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
			}
		}
	}
}
