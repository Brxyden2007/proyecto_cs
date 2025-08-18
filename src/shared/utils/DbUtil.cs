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
  public void CrearInserts(string connectionStringNoDB, string db_name)
  {
    Console.Clear();
    string inserts_query = $"""
    INSERT INTO portes (nombre) VALUES
    ('Bajo'),
    ('Mediano'),
    ('Alto'),
    ('Muy Alto');

    INSERT INTO tamanios_grano (nombre) VALUES
    ('Pequeño'),
    ('Mediano'),
    ('Grande'),
    ('Muy Grande');

    INSERT INTO altitudes (rango) VALUES
    ('Bajo (800-1200 msnm)'),
    ('Medio (1200-1600 msnm)'),
    ('Alto (1600-2000 msnm)'),
    ('Muy Alto (>2000 msnm)');

    INSERT INTO rendimientos (nivel) VALUES
    ('Bajo'),
    ('Medio'),
    ('Alto'),
    ('Muy Alto');

    INSERT INTO calidades_altitudes (nivel) VALUES
    ('Baja'),
    ('Media'),
    ('Alta'),
    ('Excelente');

    INSERT INTO resistencias (enfermedad, nivel) VALUES
    ('Roya del café', 'Alta'),
    ('Antracnosis', 'Media'),
    ('Mal de Panamá', 'Baja'),
    ('Broca del café', 'Baja'),
    ('Ojo de gallo', 'Alta'),
    ('Mancha de hierro', 'Media'),
    ('Nemátodos', 'Media');

    INSERT INTO personas (nombre, apellido, edad, nacionalidad, documento_identidad, genero) VALUES
    ('Juan', 'Pérez', 30, 'Colombiana', 101010101, 'Masculino'),
    ('María', 'García', 25, 'Colombiana', 102020202, 'Femenino'),
    ('Carlos', 'López', 45, 'Ecuatoriana', 103030303, 'Masculino'),
    ('Ana', 'Rodríguez', 35, 'Venezolana', 104040404, 'Femenino'),
    ('Luis', 'González', 50, 'Peruana', 105050505, 'Masculino'),
    ('Pedro', 'Díaz', 28, 'Mexicana', 106060606, 'Masculino'),
    ('Laura', 'Martínez', 33, 'Colombiana', 107070707, 'Femenino'),
    ('Andrés', 'Fernández', 40, 'Chilena', 108080808, 'Masculino'),
    ('Sofía', 'Ramírez', 22, 'Argentina', 109090909, 'Femenino'),
    ('Javier', 'Silva', 38, 'Uruguaya', 110101010, 'Masculino'),
    ('Valentina', 'Torres', 29, 'Paraguaya', 111111111, 'Femenino'),
    ('Felipe', 'Ruiz', 31, 'Boliviana', 112121212, 'Masculino'),
    ('Camila', 'Vargas', 26, 'Panameña', 113131313, 'Femenino'),
    ('Daniel', 'Cabrera', 42, 'Costarricense', 114141414, 'Masculino'),
    ('Lucía', 'Herrera', 27, 'Cubana', 115151515, 'Femenino'),
    ('Ricardo', 'Pinto', 36, 'Dominicana', 116161616, 'Masculino'),
    ('Elena', 'Castro', 39, 'Colombiana', 117171717, 'Femenino'),
    ('Miguel', 'Rojas', 34, 'Ecuatoriana', 118181818, 'Masculino'),
    ('Patricia', 'Ortega', 41, 'Venezolana', 119191919, 'Femenino'),
    ('Fernando', 'Soto', 48, 'Peruana', 120202020, 'Masculino'),
    ('Silvia', 'Luna', 23, 'Mexicana', 121212121, 'Femenino'),
    ('David', 'Mendoza', 37, 'Colombiana', 122222222, 'Masculino'),
    ('Diana', 'Navarro', 32, 'Chilena', 123232323, 'Femenino'),
    ('Esteban', 'Blanco', 44, 'Argentina', 124242424, 'Masculino'),
    ('Sara', 'Ochoa', 24, 'Uruguaya', 125252525, 'Femenino'),
    ('Gabriel', 'Salazar', 46, 'Paraguaya', 126262626, 'Masculino'),
    ('Paula', 'Bustamante', 21, 'Boliviana', 127272727, 'Femenino'),
    ('Alejandro', 'Vidal', 55, 'Panameño', 128282828, 'Masculino'),
    ('Marta', 'Guerrero', 52, 'Costarricense', 129292929, 'Femenino'),
    ('Sergio', 'Morales', 29, 'Cubano', 130303030, 'Masculino'),
    ('Isabel', 'Cruz', 30, 'Dominicana', 131313131, 'Femenino'),
    ('Jorge', 'Santos', 27, 'Colombiano', 132323232, 'Masculino'),
    ('Natalia', 'Pardo', 34, 'Ecuatoriana', 133333333, 'Femenino'),
    ('Guillermo', 'Valencia', 49, 'Venezolano', 134343434, 'Masculino'),
    ('Monica', 'Linares', 28, 'Peruana', 135353535, 'Femenino'),
    ('Kevin', 'Pacheco', 33, 'Mexicano', 136363636, 'Masculino'),
    ('Vanessa', 'Cortés', 31, 'Colombiana', 137373737, 'Femenino'),
    ('Hugo', 'Aguilar', 43, 'Chileno', 138383838, 'Masculino'),
    ('Carla', 'Cáceres', 26, 'Argentina', 139393939, 'Femenino'),
    ('Raul', 'Bellido', 51, 'Uruguayo', 140404040, 'Masculino'),
    ('Adriana', 'Lagos', 25, 'Paraguaya', 141414141, 'Femenino'),
    ('Ivan', 'Ramos', 47, 'Boliviano', 142424242, 'Masculino'),
    ('Andrea', 'Salinas', 35, 'Panameña', 143434343, 'Femenino'),
    ('Benjamín', 'Maldonado', 29, 'Costarricense', 144444444, 'Masculino'),
    ('Karla', 'Arias', 30, 'Cubana', 145454545, 'Femenino'),
    ('Erick', 'Duran', 40, 'Dominicano', 146464646, 'Masculino'),
    ('Jessica', 'Solano', 32, 'Colombiana', 147474747, 'Femenino'),
    ('Hector', 'Barrios', 37, 'Ecuatoriano', 148484848, 'Masculino'),
    ('Liliana', 'Velez', 38, 'Venezolana', 149494949, 'Femenino'),
    ('Diego', 'Acosta', 53, 'Peruana', 150505050, 'Masculino'),
    ('Paola', 'Guzmán', 23, 'Mexicana', 151515151, 'Femenino'),
    ('Mario', 'Ríos', 41, 'Colombiano', 152525252, 'Masculino'),
    ('Roxana', 'Castañeda', 36, 'Chilena', 153535353, 'Femenino'),
    ('Julio', 'Vega', 50, 'Argentino', 154545454, 'Masculino'),
    ('Carolina', 'Bravo', 24, 'Uruguaya', 155555555, 'Femenino'),
    ('Joaquín', 'Tapia', 45, 'Paraguayo', 156565656, 'Masculino'),
    ('Alejandra', 'Navas', 22, 'Boliviana', 157575757, 'Femenino'),
    ('Oscar', 'Reyes', 54, 'Panameño', 158585858, 'Masculino'),
    ('Rosario', 'Fuentes', 51, 'Costarricense', 159595959, 'Femenino'),
    ('Eduardo', 'Zamora', 30, 'Cubano', 160606060, 'Masculino'),
    ('Yolanda', 'Velasquez', 28, 'Dominicana', 161616161, 'Femenino'),
    ('Manuel', 'Cortés', 35, 'Colombiano', 162626262, 'Masculino'),
    ('Marcela', 'Bustillo', 33, 'Ecuatoriana', 163636363, 'Femenino'),
    ('Alfredo', 'Ospina', 47, 'Venezolano', 164646464, 'Masculino'),
    ('Claudia', 'Gallo', 29, 'Peruana', 165656565, 'Femenino'),
    ('Emilio', 'Mejía', 39, 'Mexicano', 166666666, 'Masculino'),
    ('Gloria', 'Guevara', 31, 'Colombiana', 167676767, 'Femenino'),
    ('Ignacio', 'Peralta', 42, 'Chileno', 168686868, 'Masculino'),
    ('Rebeca', 'Herrera', 27, 'Argentina', 169696969, 'Femenino'),
    ('Roberto', 'Romero', 52, 'Uruguayo', 170707070, 'Masculino'),
    ('Silvana', 'Ibarra', 26, 'Paraguaya', 171717171, 'Femenino'),
    ('Arturo', 'Castillo', 48, 'Boliviano', 172727272, 'Masculino'),
    ('Lorena', 'Lara', 34, 'Panameña', 173737373, 'Femenino'),
    ('Vicente', 'Molina', 36, 'Costarricense', 174747474, 'Masculino'),
    ('Teresa', 'Zúñiga', 25, 'Cubana', 175757575, 'Femenino'),
    ('Germán', 'Campos', 41, 'Dominicano', 176767676, 'Masculino'),
    ('Viviana', 'Nuñez', 30, 'Colombiana', 177777777, 'Femenino'),
    ('Rodrigo', 'Gómez', 33, 'Ecuatoriano', 178787878, 'Masculino'),
    ('Ana', 'María', 29, 'Venezolana', 179797979, 'Femenino'),
    ('Ricardo', 'Alvarez', 45, 'Peruana', 180808080, 'Masculino'),
    ('Elena', 'Gutiérrez', 28, 'Mexicana', 181818181, 'Femenino'),
    ('Jorge', 'Herrera', 40, 'Colombiano', 182828282, 'Masculino'),
    ('María', 'Paz', 31, 'Chilena', 183838383, 'Femenino'),
    ('Daniel', 'Torres', 38, 'Argentino', 184848484, 'Masculino'),
    ('Lucía', 'Vidal', 26, 'Uruguaya', 185858585, 'Femenino'),
    ('Carlos', 'Sánchez', 44, 'Paraguayo', 186868686, 'Masculino'),
    ('Sofía', 'Perez', 23, 'Boliviana', 187878787, 'Femenino'),
    ('Pedro', 'Morales', 50, 'Panameño', 188888888, 'Masculino'),
    ('Gabriela', 'Ramirez', 52, 'Costarricense', 189898989, 'Femenino'),
    ('Andrés', 'Linares', 29, 'Cubano', 190909090, 'Masculino'),
    ('Valeria', 'Ruiz', 30, 'Dominicana', 191919191, 'Femenino'),
    ('Juan', 'Gomez', 27, 'Colombiano', 192929292, 'Masculino'),
    ('Camila', 'Vargas', 34, 'Ecuatoriana', 193939393, 'Femenino'),
    ('Luis', 'Acosta', 49, 'Venezolano', 194949494, 'Masculino'),
    ('Ana', 'Bustos', 28, 'Peruana', 195959595, 'Femenino'),
    ('Felipe', 'Cortés', 33, 'Mexicano', 196969696, 'Masculino'),
    ('Natalia', 'Duran', 31, 'Colombiana', 197979797, 'Femenino'),
    ('David', 'López', 43, 'Chileno', 198989898, 'Masculino'),
    ('Isabella', 'Guerra', 26, 'Argentina', 199999999, 'Femenino'),
    ('Alejandro', 'Mendoza', 51, 'Uruguayo', 200200200, 'Masculino'),
    ('María', 'Cáceres', 25, 'Paraguaya', 201201201, 'Femenino'),
    ('Javier', 'Silva', 47, 'Boliviano', 202202202, 'Masculino'),
    ('Lucía', 'Ortega', 35, 'Panameña', 203203203, 'Femenino'),
    ('Gabriel', 'Rojas', 29, 'Costarricense', 204204204, 'Masculino'),
    ('Daniela', 'Soto', 30, 'Cubana', 205205205, 'Femenino');



    INSERT INTO administradores (id_administradores, nombre, apellido, email, password_hash) VALUES
    (1, 'Juan', 'Pérez', 'juan.perez@admin.com', 'a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6q7r8s9t0u1v2w3x4y5z6'),
    (3, 'Carlos', 'López', 'carlos.lopez@admin.com', 'b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6q7r8s9t0u1v2w3x4y5z6a1'),
    (5, 'Luis', 'González', 'luis.gonzalez@admin.com', 'c3d4e5f6g7h8i9j0k1l2m3n4o5p6q7r8s9t0u1v2w3x4y5z6a1b2'),
    (7, 'Laura', 'Martínez', 'laura.martinez@admin.com', 'd4e5f6g7h8i9j0k1l2m3n4o5p6q7r8s9t0u1v2w3x4y5z6a1b2c3'),
    (9, 'Sofía', 'Ramírez', 'sofia.ramirez@admin.com', 'e5f6g7h8i9j0k1l2m3n4o5p6q7r8s9t0u1v2w3x4y5z6a1b2c3d4');


    INSERT INTO usuarios (id_usuario, nombre, apellido, email, password_hash) VALUES
    (2, 'María', 'García', 'maria.garcia@user.com', '12345'),
    (4, 'Ana', 'Rodríguez', 'ana.rodriguez@user.com', '12345'),
    (6, 'Pedro', 'Díaz', 'pedro.diaz@user.com', '12345'),
    (8, 'Andrés', 'Fernández', 'andres.fernandez@user.com', '12345'),
    (10, 'Javier', 'Silva', 'javier.silva@user.com', '12345'),
    (11, 'Valentina', 'Torres', 'valentina.torres@user.com', '12345'),
    (12, 'Felipe', 'Ruiz', 'felipe.ruiz@user.com', '12345'),
    (13, 'Camila', 'Vargas', 'camila.vargas@user.com', '12345'),
    (14, 'Daniel', 'Cabrera', 'daniel.cabrera@user.com', '12345'),
    (15, 'Lucía', 'Herrera', 'lucia.herrera@user.com', '12345'),
    (16, 'Ricardo', 'Pinto', 'ricardo.pinto@user.com', '12345'),
    (17, 'Elena', 'Castro', 'elena.castro@user.com', '12345'),
    (18, 'Miguel', 'Rojas', 'miguel.rojas@user.com', '12345'),
    (19, 'Patricia', 'Ortega', 'patricia.ortega@user.com', '12345'),
    (20, 'Fernando', 'Soto', 'fernando.soto@user.com', '12345'),
    (21, 'Silvia', 'Luna', 'silvia.luna@user.com', '12345'),
    (22, 'David', 'Mendoza', 'david.mendoza@user.com', '12345'),
    (23, 'Diana', 'Navarro', 'diana.navarro@user.com', '12345'),
    (24, 'Esteban', 'Blanco', 'esteban.blanco@user.com', '12345'),
    (25, 'Sara', 'Ochoa', 'sara.ochoa@user.com', '12345'),
    (26, 'Gabriel', 'Salazar', 'gabriel.salazar@user.com', '12345');


    INSERT INTO variedades (nombre_comun, nombre_cientifico, descripcion, imagen_url, id_porte, id_tamanio, id_altitud, id_rendimiento, id_calidad) VALUES
    ('Caturra', 'Coffea arabica var. Caturra', 'Una mutación natural del Bourbon, conocida por su alta producción y sabor afrutado. De porte bajo, lo que facilita la recolección.', './src/shared/utils/pdf/images/caturra.jpeg', 1, 2, 2, 3, 3),
    ('Castillo', 'Híbrido Castillo', 'Híbrido colombiano desarrollado para ser resistente a la roya del café. Tiene una taza limpia y equilibrada.', './src/shared/utils/pdf/images/castillo.jpeg', 2, 2, 2, 4, 2),
    ('Geisha', 'Coffea arabica var. Geisha', 'Variedad muy apreciada por su perfil de taza floral y complejo. Exige condiciones específicas de cultivo.', './src/shared/utils/pdf/images/geisha.jpeg', 3, 3, 3, 1, 4),
    ('Bourbon', 'Coffea arabica var. Bourbon', 'Una de las variedades más antiguas y conocidas, con un sabor dulce y cuerpo cremoso. Es susceptible a enfermedades.', './src/shared/utils/pdf/images/bourbon.jpeg', 2, 2, 2, 2, 3),
    ('Pacamara', 'Híbrido de Pacas y Maragogipe', 'Híbrido salvadoreño de granos grandes. Ofrece notas de chocolate y especias con una acidez brillante.', './src/shared/utils/pdf/images/pacamara.jpeg', 2, 4, 3, 2, 3),
    ('Typica', 'Coffea arabica var. Typica', 'Considerada la base de muchas variedades de café arábica. Sabor limpio y dulce, pero de bajo rendimiento.', './src/shared/utils/pdf/images/typica.jpeg', 3, 2, 2, 1, 3),
    ('Gesha', 'Coffea arabica var. Gesha', 'Similar al Geisha, pero a menudo se cultiva a menor altitud. Mantiene sus notas florales y de jazmín.', './src/shared/utils/pdf/images/gesha.jpeg', 3, 3, 3, 1, 4),
    ('Colombia', 'Híbrido Colombia', 'Híbrido de Caturra y Timor, resistente a la roya. Rendimiento alto y perfil de taza balanceado.', './src/shared/utils/pdf/images/colombia.jpeg', 2, 2, 2, 4, 2),
    ('Maragogipe', 'Coffea arabica var. Maragogipe', 'Variedad con granos gigantes. Perfil de taza suave y delicado, pero muy bajo rendimiento.', './src/shared/utils/pdf/images/maragogipe.jpeg', 3, 4, 2, 1, 2),
    ('Catimor', 'Híbrido de Caturra y Timor', 'Híbrido de alta producción y resistencia a la roya, pero puede presentar un sabor menos refinado.', './src/shared/utils/pdf/images/catimor.jpeg', 1, 2, 1, 4, 1);

    INSERT INTO variedad_resistencia (id_variedad, id_resistencia) VALUES
    (1, 1), -- Caturra es vulnerable a la Roya
    (2, 1), -- Castillo es resistente a la Roya
    (2, 2), -- Castillo con resistencia media a Antracnosis
    (3, 1), -- Geisha es vulnerable
    (4, 1), -- Bourbon es vulnerable
    (5, 6), -- Pacamara con resistencia a Mancha de hierro
    (6, 1), -- Typica es muy vulnerable
    (8, 1), -- Colombia es resistente a la Roya
    (8, 5), -- Colombia es resistente a Ojo de gallo
    (9, 1), -- Maragogipe es vulnerable
    (10, 1), -- Catimor es resistente a la Roya
    (10, 5); -- Catimor es resistente a Ojo de gallo

    INSERT INTO atributos_agronomicos (id_variedad, tiempo_cosecha, maduracion, nutricion, densidad_siembra) VALUES
    (1, '24-26 meses', 'Temprana', 'Requiere suelos fértiles y fertilización constante', '3000-4000 plantas/ha'),
    (2, '24 meses', 'Media', 'Adaptable a diversos suelos, buena respuesta a fertilizantes NPK', '4000-5000 plantas/ha'),
    (3, '36 meses', 'Tardía', 'Exigente en microelementos y nutrientes, sensible al estrés hídrico', '2000-3000 plantas/ha'),
    (4, '30 meses', 'Media', 'Responde bien a la fertilización orgánica', '3000-4000 plantas/ha'),
    (5, '28 meses', 'Temprana', 'Demanda potasio para el desarrollo del grano', '2500-3500 plantas/ha'),
    (6, '36 meses', 'Tardía', 'Necesita suelos profundos y bien drenados', '2000-3000 plantas/ha'),
    (7, '36 meses', 'Tardía', 'Exigente en microelementos y nutrientes', '2000-3000 plantas/ha'),
    (8, '24 meses', 'Media', 'Adaptable, buena respuesta a fertilización balanceada', '4000-5000 plantas/ha'),
    (9, '30 meses', 'Tardía', 'Requiere suelos muy ricos en nutrientes', '1500-2500 plantas/ha'),
    (10, '20 meses', 'Temprana', 'Tolerante a suelos menos fértiles, pero responde bien a la fertilización', '4500-6000 plantas/ha');


    INSERT INTO historias_geneticas (id_variedad, obtentor, familia, grupo, descripcion) VALUES
    (1, 'Descubrimiento natural', 'Bourbon', 'Arábica', 'Mutación natural del Bourbon, encontrada en Brasil en 1937.'),
    (2, 'Cenicafé', 'Híbrido de Caturra x Híbrido de Timor', 'Híbrido', 'Desarrollada en Colombia por Cenicafé para resistencia a la roya.'),
    (3, 'Descubrimiento en Etiopía', 'Etíope', 'Arábica', 'Descubierta en Etiopía y llevada a América Latina, popularizada en Panamá.'),
    (4, 'Desconocido', 'Typica', 'Arábica', 'Una de las primeras variedades de café Arábica, cultivada en la isla de Bourbon (ahora Reunión).'),
    (5, 'Cruzamiento selectivo', 'Híbrido', 'Híbrido', 'Cruzamiento entre las variedades Pacas y Maragogipe en El Salvador.'),
    (6, 'Descubrimiento natural', 'Bourbon', 'Arábica', 'La variedad base de la mayoría de los cafés Arábicas del mundo, originaria de Yemen.'),
    (7, 'Descubrimiento en Etiopía', 'Etíope', 'Arábica', 'Clonación y propagación de la variedad Gesha, similar a Geisha.'),
    (8, 'Cenicafé', 'Híbrido', 'Híbrido', 'Variedad desarrollada por Cenicafé en Colombia, liberada en 1982.'),
    (9, 'Descubrimiento natural', 'Typica', 'Arábica', 'Mutación natural del Typica, encontrada en Brasil. Conocida como "Elefante de grano".'),
    (10, 'Cruzamiento selectivo', 'Híbrido', 'Híbrido', 'Cruzamiento entre las variedades Caturra y Híbrido de Timor en Portugal.');
    """;

    using (var connection = new MySqlConnection(connectionStringNoDB))
    {
      connection.Open();

      // Verificar si existe la base de datos
      using (var verificar_command = new MySqlCommand($"SHOW DATABASES LIKE '{db_name}'", connection))
      using (var lector_basedatos = verificar_command.ExecuteReader())
      {
        if (lector_basedatos.HasRows)
        {
          lector_basedatos.Close(); // 🔑 cerrar antes de ejecutar más comandos

          // Crear una transacción para agrupar los inserts
          using var transaction = connection.BeginTransaction();

          try
          {
            // Dividir en sentencias individuales por ';'
            string[] queries = inserts_query.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var query in queries)
            {
                using var command = new MySqlCommand(query.Trim(), connection, transaction);
                command.ExecuteNonQuery();
            }

            // Confirmar los cambios si todo salió bien
            transaction.Commit();
            Console.WriteLine("✅ Inserts de prueba creados correctamente.");
          }
          catch (Exception ex)
          {
            // Revertir si algo falla
            transaction.Rollback();
            Console.WriteLine("❌ Error al ejecutar los inserts: " + ex.Message);
          }
        }
        else
        {
          Console.WriteLine($"⚠️ La base de datos '{db_name}' no existe.");
        }
      }
    }
  }
}
