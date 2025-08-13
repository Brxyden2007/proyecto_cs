-- este archivo tiene como proposito tener un molde claro de las tablas(entidades) que se van a trabajar a lo largo del proyecto
DROP DATABASE IF EXISTS proyecto_cs;
CREATE DATABASE IF NOT EXISTS proyecto_cs;
USE proyecto_cs;

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
    email VARCHAR(150) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_id_administradores FOREIGN KEY (id) REFERENCES personas(id) ON DELETE CASCADE
) ENGINE=INNODB;

CREATE TABLE IF NOT EXISTS usuarios (
    id_usuario INT,
    email VARCHAR(150) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_id_usuarios FOREIGN KEY (id) REFERENCES personas(id) ON DELETE CASCADE
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
    nivel VARCHAR(50) UNIQUE NOT NULL,
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
    FOREIGN KEY (id_tamano) REFERENCES tamanos_grano(id_tamano),
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
    obtentor VARCHAR(100),
    familia VARCHAR(100),
    grupo VARCHAR(100),
    descripcion TEXT,
    FOREIGN KEY (id_variedad) REFERENCES variedades(id_variedad) ON DELETE CASCADE
);

-- esto es para la informacion del pdf
CREATE TABLE pdf_catalogos (
    id_pdf INT PRIMARY KEY AUTO_INCREMENT,
    id_usuario INT,
    filtros_usados TEXT,
    ruta_pdf VARCHAR(255),
    fecha_generacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id)
);