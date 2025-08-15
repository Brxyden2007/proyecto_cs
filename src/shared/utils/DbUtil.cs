using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using proyecto_cs.src.shared.utils;
using proyecto_cs;

namespace proyecto_cs.src.shared.utils;

public class DbUtil
{
  private readonly Validaciones validate_data = new Validaciones();
  public void CrearBaseDeDatos(string connectionStringNoDB, string db_name)
  {
    Console.Clear();
    string tablas_query = $"""
    DROP DATABASE IF EXISTS {db_name};
    CREATE DATABASE IF NOT EXISTS {db_name};
    USE {db_name};

    -- toda esta parte corresponde a la entidad personas y a sus fk
    CREATE TABLE IF NOT EXISTS personas (
        id INT PRIMARY KEY AUTO_INCREMENT,
        nombre VARCHAR(80),
        apellido VARCHAR(80),
        edad INT,
        nacionalidad VARCHAR(50),
        documento_identidad INT UNIQUE,
        genero VARCHAR(50)
    ) ENGINE=INNODB;

    -- la gracia de las tablas de administradores y usuarios es que no haga falta definir un rol en una unica tabla si no que directamente se separan en 2 tipos porque son unicamente 2
    CREATE TABLE IF NOT EXISTS administradores (
        id_administradores INT,
        nombre VARCHAR(100) NOT NULL,
        apellido VARCHAR(100) NOT NULL,
        email VARCHAR(150) UNIQUE NOT NULL,
        password_hash VARCHAR(255) NOT NULL,
        createdat TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        CONSTRAINT fk_id_administradores FOREIGN KEY (id_administradores) REFERENCES personas(id) ON DELETE CASCADE
    ) ENGINE=INNODB;

    CREATE TABLE IF NOT EXISTS usuarios (
        id_usuario INT,
        nombre VARCHAR(100) NOT NULL,
        apellido VARCHAR(100) NOT NULL,
        email VARCHAR(150) UNIQUE NOT NULL,
        password_hash VARCHAR(255) NOT NULL,
        createdat TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        CONSTRAINT fk_id_usuarios FOREIGN KEY (id_usuario) REFERENCES personas(id) ON DELETE CASCADE
    ) ENGINE=INNODB;

    -- esto corresponde a las entidades de catalogos y sus respectivos atibutos
    CREATE TABLE portes (
        id_porte INT PRIMARY KEY AUTO_INCREMENT,
        nombre VARCHAR(50) UNIQUE NOT NULL
    );

    CREATE TABLE tamanios_grano (
        id_tamanio INT PRIMARY KEY AUTO_INCREMENT,
        nombre VARCHAR(50) UNIQUE NOT NULL
    );

    CREATE TABLE altitudes (
        id_altitud INT PRIMARY KEY AUTO_INCREMENT,
        rango VARCHAR(100) UNIQUE NOT NULL
    );

    CREATE TABLE rendimientos (
        id_rendimiento INT PRIMARY KEY AUTO_INCREMENT,
        nivel VARCHAR(50) UNIQUE NOT NULL
    );

    CREATE TABLE calidades_altitudes (
        id_calidad INT PRIMARY KEY AUTO_INCREMENT,
        nivel VARCHAR(50) UNIQUE NOT NULL
    );

    CREATE TABLE resistencias (
        id_resistencia INT PRIMARY KEY AUTO_INCREMENT,
        enfermedad VARCHAR(50) UNIQUE NOT NULL,
        nivel VARCHAR(50) UNIQUE NOT NULL
    );

    -- tabla de variedades, esto se podria considerar la entidad principal
    CREATE TABLE variedades (
        id_variedad INT PRIMARY KEY AUTO_INCREMENT,
        nombre_comun VARCHAR(100) NOT NULL,
        nombre_cientifico VARCHAR(150),
        descripcion TEXT,
        imagen_url VARCHAR(255),
        id_porte INT,
        id_tamano INT,
        id_altitud INT,
        id_rendimiento INT,
        id_calidad INT,
        FOREIGN KEY (id_porte) REFERENCES portes(id_porte),
        FOREIGN KEY (id_tamano) REFERENCES tamanios_grano(id_tamanio),
        FOREIGN KEY (id_altitud) REFERENCES altitudes(id_altitud),
        FOREIGN KEY (id_rendimiento) REFERENCES rendimientos(id_rendimiento),
        FOREIGN KEY (id_calidad) REFERENCES calidades_altitudes(id_calidad)
    );

    -- Relación de variedades con resistencias (N:M)
    CREATE TABLE variedad_resistencia (
        id_variedad INT,
        id_resistencia INT,
        PRIMARY KEY (id_variedad, id_resistencia),
        FOREIGN KEY (id_variedad) REFERENCES variedades(id_variedad) ON DELETE CASCADE,
        FOREIGN KEY (id_resistencia) REFERENCES resistencias(id_resistencia) ON DELETE CASCADE
    );

    -- Información agronómica complementaria
    CREATE TABLE atributos_agronomicos (
        id_atributo INT PRIMARY KEY AUTO_INCREMENT,
        id_variedad INT NOT NULL,
        tiempo_cosecha VARCHAR(50),
        maduracion VARCHAR(50),
        nutricion TEXT,
        densidad_siembra VARCHAR(50),
        FOREIGN KEY (id_variedad) REFERENCES variedades(id_variedad) ON DELETE CASCADE
    );

    -- Historia y linaje genético
    CREATE TABLE historias_geneticas (
        id_historia INT PRIMARY KEY AUTO_INCREMENT,
        id_variedad INT NOT NULL,
        obtentor VARCHAR(80),
        familia VARCHAR(80),
        grupo VARCHAR(80),
        descripcion TEXT,
        FOREIGN KEY (id_variedad) REFERENCES variedades(id_variedad) ON DELETE CASCADE
    );
    """;

    using var connection = new MySqlConnection(connectionStringNoDB);
    connection.Open();

    using var verificar_command = new MySqlCommand(
      $"SHOW DATABASES LIKE '{db_name}'", connection);
    using var lector_basedatos = verificar_command.ExecuteReader();
    // verfica si el show muestra alguna base de datos 
    if (lector_basedatos.HasRows)
    {
      Console.WriteLine($"la base de datos '{db_name}' ya existe.");
      Console.WriteLine("Presione una tecla para continuar...");
      Console.ReadLine();
      return;
    }
    lector_basedatos.Close();

    using var command = new MySqlCommand(tablas_query, connection);
    command.ExecuteNonQuery();
    Console.WriteLine("Base de datos creada :D");
    Console.ReadLine();
  }
  public void BorrarBaseDeDatos(string connectionStringNoDB, string db_name)
  {
    Console.Clear();
    Console.WriteLine("¿Estás seguro de que deseas borrar la base de datos? (s/n)");
    string? confirmacion = validate_data.ValidarTexto(Console.ReadLine());
    if (validate_data.ValidarBoleano(confirmacion))
    {
      string tablas_query = $"DROP DATABASE IF EXISTS `{db_name};";

      using var connection = new MySqlConnection(connectionStringNoDB);
      connection.Open();

      using var command = new MySqlCommand(tablas_query, connection);
      command.ExecuteNonQuery();
      Console.WriteLine("Base de datos borrada :D");
    }
    else
    {
      Console.WriteLine("La base de datos no ha sido borrada :(");
      Console.ReadLine();
    }
  }
}
