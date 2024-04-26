using CapaEntidades;
using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista
{
    public partial class RegistroVenta : Form
    {
        VentaLOG _ventaLOG;
        ProductoLOG _productoLOG;
        DataTable detalleVenta;
        public RegistroVenta()
        {
            InitializeComponent();

            CargarProducto();

            detalleVenta = new DataTable();
            detalleVenta.Columns.Add("Codigo", typeof(int));
            detalleVenta.Columns.Add("Nombre", typeof(string));
            detalleVenta.Columns.Add("Precio", typeof(decimal));
            detalleVenta.Columns.Add("Cantidad", typeof(int));
            detalleVenta.Columns.Add("SubTotal", typeof(decimal));
        }

        private void CargarProducto()
        {
            _productoLOG = new ProductoLOG();
            ProductoBindingSource.DataSource = _productoLOG.ObtenerProductos();
        }
        private void txtCodigoProducto_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtCodigoProducto.Text))
            {
                _productoLOG = new ProductoLOG();

                int codigo = int.Parse(txtCodigoProducto.Text);

                var producto = _productoLOG.ObtenerProductoPorId(codigo);

                if (producto != null && producto.Estado == true)
                {
                    cmbNombreProducto.Text = producto.Nombre;
                }
                else
                {
                    cmbNombreProducto.Text = "";
                }
            }
            else
            {
                CargarProducto();
            }
        }

        private void txtAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                _productoLOG = new ProductoLOG();

                int codigo = int.Parse(txtCodigoProducto.Text);
                int cantidad = int.Parse(txtCantidad.Text);

                var producto = (Producto)ProductoBindingSource.Current;

                if (producto != null)
                {
                    detalleVenta.Rows.Add(codigo, producto.Nombre, producto.PrecioUnitario, 
                        cantidad, (cantidad*producto.PrecioUnitario));

                    dgvDetalleVenta.DataSource = detalleVenta;

                    decimal montoTotal = 0;

                    foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
                    {
                        montoTotal += decimal.Parse(row.Cells["SubTotal"].Value.ToString());
                    }

                    txtMontoTotal.Text = montoTotal.ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrio un error", "Tienda | Registro venta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                _ventaLOG = new VentaLOG();

                Venta venta = new Venta();
                venta.Fecha = DateTime.Now;
                venta.Total = decimal.Parse(txtMontoTotal.Text);

                foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
                {
                    var detalle = new DetalleVenta()
                    {
                        ProductoId = int.Parse(row.Cells["Codigo"].Value.ToString()),
                        Precio = decimal.Parse(row.Cells["Precio"].Value.ToString()),
                        Cantidad = int.Parse(row.Cells["Cantidad"].Value.ToString())
                    };

                    venta.Detalles.Add(detalle);
                }

                int resultado = _ventaLOG.guardarVenta(venta);

                if (resultado >= 0)
                {
                    MessageBox.Show("Venta guardada con exito", "Tienda | Registro venta",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se logro guardar la venta", "Tienda | Registro venta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrio un error", "Tienda | Registro venta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDetalleVenta_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    bool precioValido = decimal.TryParse(dgvDetalleVenta.Rows[e.RowIndex].Cells["Precio"].Value.ToString(), out decimal precio);
                    int cantidad = int.Parse(dgvDetalleVenta.Rows[e.RowIndex].Cells["Cantidad"].Value.ToString());

                    if (precioValido && cantidad > 0)
                    {
                        decimal subTotal = precio * cantidad;
                        dgvDetalleVenta.Rows[e.RowIndex].Cells["SubTotal"].Value = subTotal;

                        CalcularMontoTotal();
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrio un error");
            }
        }

        private void CalcularMontoTotal()
        {
            decimal montoTotal = 0;

            foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
            {
                montoTotal += decimal.Parse(row.Cells["SubTotal"].Value.ToString());
            }

            txtMontoTotal.Text = montoTotal.ToString();
        }
    }
}
