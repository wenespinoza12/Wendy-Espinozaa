﻿using System;                           // Importa las clases básicas del sistema.
using System.Collections.Generic;       // Importa clases para trabajar con colecciones genéricas.
using System.Data.SqlClient;            // Importa clases para interactuar con bases de datos SQL Server.
using System.Linq;                      // Importa clases para consultas a colecciones.
using System.Net.Http.Headers;          // Importa clases para manejar encabezados HTTP (no utilizado en el código).
using System.Text;                      // Importa clases para manejar textos y codificaciones.
using System.Threading.Tasks;           // Importa clases para tareas asincrónicas.

namespace DatosLayer                    // Define el espacio de nombres para la capa de datos.
{
    public class CustomerRepository      // Define la clase CustomerRepository para manejar operaciones con clientes.
    {
        public List<Customers> ObtenerTodos()
        {   // Método para obtener todos los clientes de la base de datos.
            using (var conexion = DataBase.GetSqlConnection())
            {  // Crea una conexión a la base de datos utilizando una conexión predefinida.
                String selectFrom = "";                    
                // Declara una cadena para construir la consulta SQL.
                selectFrom = selectFrom + "SELECT [CustomerID] " + "\n"; 
                // Agrega columnas a la consulta SQL.
                selectFrom = selectFrom + "      ,[CompanyName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";
                selectFrom = selectFrom + "      ,[Address] " + "\n";
                selectFrom = selectFrom + "      ,[City] " + "\n";
                selectFrom = selectFrom + "      ,[Region] " + "\n";
                selectFrom = selectFrom + "      ,[PostalCode] " + "\n";
                selectFrom = selectFrom + "      ,[Country] " + "\n";
                selectFrom = selectFrom + "      ,[Phone] " + "\n";
                selectFrom = selectFrom + "      ,[Fax] " + "\n";
                selectFrom = selectFrom + "  FROM [dbo].[Customers]"; 
                // Especifica la tabla desde la cual se recuperarán los datos.

                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                {  // Crea un comando SQL para ejecutar la consulta.
                    SqlDataReader reader = comando.ExecuteReader();  
                    // Ejecuta el comando y obtiene un lector de datos.
                    List<Customers> Customers = new List<Customers>();  
                    // Declara una lista para almacenar los clientes.

                    while (reader.Read())  // Lee cada fila del lector de datos.
                    {
                        var customers = LeerDelDataReader(reader); 
                        // Convierte la fila actual en un objeto Customers.
                        Customers.Add(customers); 
                        // Agrega el cliente a la lista.
                    }
                    return Customers; 
                    // Retorna la lista de clientes.
                }
            }
        }

        public Customers ObtenerPorID(string id)
        {   // Método para obtener un cliente por su ID.
            using (var conexion = DataBase.GetSqlConnection())
            {  // Crea una conexión a la base de datos utilizando una conexión predefinida.

                String selectForID = "";                   
                // Declara una cadena para construir la consulta SQL.
                selectForID = selectForID + "SELECT [CustomerID] " + "\n";  
                // Agrega columnas a la consulta SQL.
                selectForID = selectForID + "      ,[CompanyName] " + "\n";
                selectForID = selectForID + "      ,[ContactName] " + "\n";
                selectForID = selectForID + "      ,[ContactTitle] " + "\n";
                selectForID = selectForID + "      ,[Address] " + "\n";
                selectForID = selectForID + "      ,[City] " + "\n";
                selectForID = selectForID + "      ,[Region] " + "\n";
                selectForID = selectForID + "      ,[PostalCode] " + "\n";
                selectForID = selectForID + "      ,[Country] " + "\n";
                selectForID = selectForID + "      ,[Phone] " + "\n";
                selectForID = selectForID + "      ,[Fax] " + "\n";
                selectForID = selectForID + "  FROM [dbo].[Customers] " + "\n"; 
                // Especifica la tabla desde la cual se recuperarán los datos.
                selectForID = selectForID + $"  Where CustomerID = @customerId"; 
                // Agrega una cláusula WHERE para filtrar por ID del cliente.

                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {  // Crea un comando SQL para ejecutar la consulta.
                    comando.Parameters.AddWithValue("customerId", id); 
                    // Agrega el parámetro del ID del cliente a la consulta.

                    var reader = comando.ExecuteReader();  
                    // Ejecuta el comando y obtiene un lector de datos.
                    Customers customers = null;         
                    // Declara una variable para almacenar el cliente.

                    if (reader.Read())
                    {                   // Lee la primera fila del lector de datos si está disponible.
                        customers = LeerDelDataReader(reader); 
                        // Convierte la fila actual en un objeto Customers.
                    }
                    return customers; 
                    // Retorna el cliente obtenido o null si no se encontró.
                }
            }
        }

        public Customers LeerDelDataReader(SqlDataReader reader)
        {  // Método para convertir un SqlDataReader en un objeto Customers.
            Customers customers = new Customers();  // Crea una instancia de la clase Customers.
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? " " : (String)reader["CustomerID"]; 
            // Asigna el valor del CustomerID, manejando valores nulos.
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];  
            // Asigna el valor del CompanyName, manejando valores nulos.
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"]; 
            // Asigna el valor del ContactName, manejando valores nulos.
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"]; 
            // Asigna el valor del ContactTitle, manejando valores nulos.
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];  
            // Asigna el valor del Address, manejando valores nulos.
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];  
            // Asigna el valor del City, manejando valores nulos.
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];  
            // Asigna el valor del Region, manejando valores nulos.
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            // Asigna el valor del PostalCode, manejando valores nulos.
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"]; 
            // Asigna el valor del Country, manejando valores nulos.
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            // Asigna el valor del Phone, manejando valores nulos.
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];  
            // Asigna el valor del Fax, manejando valores nulos.
            return customers; 
            // Retorna el objeto Customers con los datos del lector.
        }

        public int InsertarCliente(Customers customer)
        {  // Método para insertar un nuevo cliente en la base de datos.
            using (var conexion = DataBase.GetSqlConnection())
            {  // Crea una conexión a la base de datos utilizando una conexión predefinida.
                String insertInto = "";                      // Declara una cadena para construir la consulta SQL.
                insertInto = insertInto + "INSERT INTO [dbo].[Customers] " + "\n";  // Especifica la tabla y columnas para la inserción.
                insertInto = insertInto + "           ([CustomerID] " + "\n";
                insertInto = insertInto + "           ,[CompanyName] " + "\n";
                insertInto = insertInto + "           ,[ContactName] " + "\n";
                insertInto = insertInto + "           ,[ContactTitle] " + "\n";
                insertInto = insertInto + "           ,[Address] " + "\n";
                insertInto = insertInto + "           ,[City]) " + "\n";  // Lista las columnas que serán insertadas.
                insertInto = insertInto + "     VALUES " + "\n";  // Especifica los valores que se insertarán.
                insertInto = insertInto + "           (@CustomerID " + "\n";
                insertInto = insertInto + "           ,@CompanyName " + "\n";
                insertInto = insertInto + "           ,@ContactName " + "\n";
                insertInto = insertInto + "           ,@ContactTitle " + "\n";
                insertInto = insertInto + "           ,@Address " + "\n";
                insertInto = insertInto + "           ,@City)";

                using (var comando = new SqlCommand(insertInto, conexion))
                {  // Crea un comando SQL para ejecutar la inserción.
                    int insertados = parametrosCliente(customer, comando);  // Asigna los parámetros y ejecuta la consulta.
                    return insertados;  // Retorna el número de filas afectadas.
                }
            }
        }

        public int ActualizarCliente(Customers customer)
        {  // Método para actualizar un cliente existente en la base de datos.
            using (var conexion = DataBase.GetSqlConnection())
            {  // Crea una conexión a la base de datos utilizando una conexión predefinida.
                String ActualizarCustomerPorID = "";         // Declara una cadena para construir la consulta SQL.
                ActualizarCustomerPorID = ActualizarCustomerPorID + "UPDATE [dbo].[Customers] " + "\n";  // Especifica la tabla a actualizar.
                ActualizarCustomerPorID = ActualizarCustomerPorID + "   SET [CustomerID] = @CustomerID " + "\n";  // Lista las columnas a actualizar.
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[CompanyName] = @CompanyName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactName] = @ContactName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactTitle] = @ContactTitle " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[Address] = @Address " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[City] = @City " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " WHERE CustomerID= @CustomerID";  // Agrega una cláusula WHERE para filtrar por ID del cliente.

                using (var comando = new SqlCommand(ActualizarCustomerPorID, conexion))
                {  // Crea un comando SQL para ejecutar la actualización.
                    int actualizados = parametrosCliente(customer, comando);  // Asigna los parámetros y ejecuta la consulta.
                    return actualizados;  // Retorna el número de filas afectadas.
                }
            }
        }

        public int parametrosCliente(Customers customer, SqlCommand comando)
        {  // Método para asignar parámetros al comando SQL.
            comando.Parameters.AddWithValue("CustomerID", customer.CustomerID);  
            // Asigna el valor del parámetro CustomerID.
            comando.Parameters.AddWithValue("CompanyName", customer.CompanyName);  
            // Asigna el valor del parámetro CompanyName.
            comando.Parameters.AddWithValue("ContactName", customer.ContactName);  
            // Asigna el valor del parámetro ContactName.
            comando.Parameters.AddWithValue("ContactTitle", customer.ContactName); 
            // Asigna el valor del parámetro ContactTitle (parece un error, debería ser ContactTitle).
            comando.Parameters.AddWithValue("Address", customer.Address); 
            // Asigna el valor del parámetro Address.
            comando.Parameters.AddWithValue("City", customer.City);  
            // Asigna el valor del parámetro City.
            var insertados = comando.ExecuteNonQuery();  
            // Ejecuta el comando SQL y retorna el número de filas afectadas.
            return insertados;  
            // Retorna el número de filas afectadas.
        }

        public int EliminarCliente(string id)
        {  // Método para eliminar un cliente por su ID.
            using (var conexion = DataBase.GetSqlConnection())
            {  // Crea una conexión a la base de datos utilizando una conexión predefinida.
                String EliminarCliente = "";               
                // Declara una cadena para construir la consulta SQL.
                EliminarCliente = EliminarCliente + "DELETE FROM [dbo].[Customers] " + "\n"; 
                // Especifica la tabla de la cual se eliminarán los datos.
                EliminarCliente = EliminarCliente + "      WHERE CustomerID = @CustomerID"; 
                // Agrega una cláusula WHERE para filtrar por ID del cliente.


                using (SqlCommand comando = new SqlCommand(EliminarCliente, conexion))
                {  // Crea un comando SQL para ejecutar la eliminación.
                    comando.Parameters.AddWithValue("@CustomerID", id);
                    // Asigna el valor del parámetro CustomerID.
                    int elimindos = comando.ExecuteNonQuery(); 
                    // Ejecuta el comando SQL y retorna el número de filas afectadas.
                    return elimindos; 
                    // Retorna el número de filas afectadas.
                }
            }
        }
    }
}
