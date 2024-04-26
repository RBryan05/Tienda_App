using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class ProductoLOG
    {
        ProductoDAL _productoDal;

        public int GuardarProducto(Producto producto, int id = 0, bool esActualizacion = false)
        {
            _productoDal = new ProductoDAL();

            return _productoDal.Guardar(producto, id, esActualizacion);
        }

        public int ActualizarProducto(Producto producto, int id)
        {
            _productoDal = new ProductoDAL();
            return _productoDal.Guardar(producto, id, true);
        }

        public List<Producto> ObtenerProductos(bool inacativos = false)
        {
            _productoDal = new ProductoDAL();

            return _productoDal.Leer(inacativos);
        }

        public Producto ObtenerProductoPorId(int id)
        {
            _productoDal = new ProductoDAL();
            return _productoDal.LeerPorId(id);
        }

        public int EliminarProducto(int id)
        {
            _productoDal = new ProductoDAL();
            return _productoDal.EliminarProducto(id);
        }
    }
}
