using CapaEntidades;
using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista
{
    public partial class RegistroProducto : Form
    {
        ProductoLOG _productoLOG;

        public RegistroProducto()
        {
            InitializeComponent();

            productoBindingSource.MoveLast();
            productoBindingSource.AddNew();
        }

        private void RegistroProducto_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarProducto();
        }

        private void GuardarProducto()
        {
            try
            {
                _productoLOG = new ProductoLOG();
                //throw new Exception();
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    MessageBox.Show("Se requiere el nombre del producto", "Tienda | Registro Productos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    txtNombre.BackColor = Color.LightYellow;
                    return;
                }

                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    MessageBox.Show("Se requiere la descripcion del producto", "Tienda | Registro Productos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtPrecioUnitario.Text) || Convert.ToDecimal(txtPrecioUnitario.Text) == 0)
                {
                    MessageBox.Show("Se requiere el nombre del producto", "Tienda | Registro Productos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtExistencias.Text) || Convert.ToDecimal(txtExistencias.Text) == 0)
                {
                    MessageBox.Show("Se requiere el nombre del producto", "Tienda | Registro Productos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!chbActivo.Checked)
                {
                    var dialogo = MessageBox.Show("Estas seguro que desea guardar el producto inactivo?","Tienda | Registro Producto",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (dialogo != DialogResult.Yes)
                    {
                        MessageBox.Show("Seleccione el ncuadro estado como activo", "Tienda | Registro Productos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                Producto producto;
                producto = (Producto)productoBindingSource.Current;
                int resultado = _productoLOG.GuardarProducto(producto);

                if (resultado > 0)
                {
                    MessageBox.Show("Producto Agregado con Exito", "Tienda | Registro Productos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se logro agregagr el producto", "Tienda | Registro Productos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrio un error", "Tienda | Registro Productos",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
