using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Producto> ObtenerProductos()
        {
            _productoDal = new ProductoDAL();

            return _productoDal.Leer();
        }

        public Producto ObtenerProductoPorId(int id)
        {
            _productoDal = new ProductoDAL();
            return _productoDal.LeerPorId(id);
        }
    }
}
