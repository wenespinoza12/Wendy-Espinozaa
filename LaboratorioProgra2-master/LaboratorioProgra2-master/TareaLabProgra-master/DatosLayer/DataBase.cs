using System;                     // Importa las clases básicas del sistema.
using System.Collections.Generic; // Importa clases para trabajar con colecciones genéricas.
using System.Linq;                // Importa clases para consultas a colecciones.
using System.Text;                // Importa clases para manejar textos y codificaciones.
using System.Threading.Tasks;     // Importa clases para tareas asincrónicas.
using System.Configuration;        // Importa clases para manejar configuraciones de la aplicación.
using System.Xml.Linq;            // Importa clases para trabajar con XML y LINQ to XML.
using System.Data.SqlClient;       // Importa clases para manejar la conexión y comandos de SQL Server.
using System.Runtime.CompilerServices; // Importa clases para operaciones de compilador y runtime.

namespace DatosLayer              // Define el espacio de nombres para la capa de datos.
{
    public class DataBase         // Define la clase DataBase para manejar la configuración de la base de datos.
    {
        public static string ConnectionString
        {  // Propiedad estática que obtiene la cadena de conexión a la base de datos.
            get
            {
                // Obtiene la cadena de conexión desde el archivo de configuración de la aplicación.
                string CadenaConexion = ConfigurationManager
                    .ConnectionStrings["NWConnection"]
                    .ConnectionString;

                // Crea un SqlConnectionStringBuilder a partir de la cadena de conexión.
                SqlConnectionStringBuilder conexionBuilder =
                    new SqlConnectionStringBuilder(CadenaConexion);

                // Establece el nombre de la aplicación en el SqlConnectionStringBuilder, si está definido.
                conexionBuilder.ApplicationName =
                    ApplicationName ?? conexionBuilder.ApplicationName;

                // Establece el tiempo de espera de conexión en el SqlConnectionStringBuilder, si es mayor que 0.
                conexionBuilder.ConnectTimeout = (ConnectionTimeout > 0)
                    ? ConnectionTimeout : conexionBuilder.ConnectTimeout;

                // Retorna la cadena de conexión completa construida.
                return conexionBuilder.ToString();
            }
        }

        public static int ConnectionTimeout { get; set; } 
        // Propiedad estática para establecer el tiempo de espera de la conexión.
        public static string ApplicationName { get; set; } 
        // Propiedad estática para establecer el nombre de la aplicación.

        public static SqlConnection GetSqlConnection()  
            // Método estático para obtener una conexión SQL.
        {
            // Crea una nueva instancia de SqlConnection usando la cadena de conexión.
            SqlConnection conexion = new SqlConnection(ConnectionString);
            conexion.Open();  // Abre la conexión a la base de datos.
            return conexion;  // Retorna la conexión abierta.
        }
    }
}
