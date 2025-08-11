-- este archivo tiene como proposito tener un molde claro de las tablas(entidades) que se van a trabajar a lo largo del proyecto
DROP DATABASE IF EXISTS proyecto_cs;
CREATE DATABASE IF NOT EXISTS proyecto_cs;
USE proyecto_cs;

CREATE TABLE IF NOT EXISTS personas (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(80),
    apellido VARCHAR(80),
    edad INT,
    nacionalidad VARCHAR(50),
    documento_identidad INT UNIQUE,
    genero VARCHAR(50)
) ENGINE=INNODB;

CREATE TABLE IF NOT EXISTS usuarios (
    id INT,
    email VARCHAR(150) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    -- aqui se coloca varchar para facilitar el ingreso de datos pero como tal las opciones son admin/usuario
    rol VARCHAR(30),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_id_usuarios FOREIGN KEY (id) REFERENCES personas(id) ON DELETE CASCADE
) ENGINE=INNODB;

CREATE TABLE IF NOT EXISTS variedades (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre_comun VARCHAR(150) NOT NULL,
    nombre_cientifico VARCHAR(200),
    imagen_ruta VARCHAR(255),
    descripcion TEXT,
    porte ENUM('Alto', 'Bajo'),
    tamano_grano ENUM('Pequeño', 'Medio', 'Grande'),
    altitud_optima DECIMAL(6,2),
    potencial_rendimiento ENUM('Muy bajo', 'Bajo', 'Medio', 'Alto', 'Excepcional'),
    calidad_grano_altitud ENUM('Nivel 1', 'Nivel 2', 'Nivel 3', 'Nivel 4', 'Nivel 5'),
    resistencia_roya ENUM('Susceptible', 'Tolerante', 'Resistente'),
    resistencia_antracnosis ENUM('Susceptible', 'Tolerante', 'Resistente'),
    resistencia_nematodos ENUM('Susceptible', 'Tolerante', 'Resistente'),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=INNODB;

-- relacion 1 a uno con variedades
CREATE TABLE IF NOT EXISTS atributos_agronomicos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    variedad_id INT NOT NULL,
    tiempo_cosecha VARCHAR(100),
    maduracion VARCHAR(100),
    nutricion VARCHAR(255),
    densidad_siembra VARCHAR(100),
    CONSTRAINT fk_variedad_id_atributos_agronomicos FOREIGN KEY (variedad_id) REFERENCES variedades(id) ON DELETE CASCADE
) ENGINE=INNODB;

-- relacion uno a uno con variedades
CREATE TABLE IF NOT EXISTS historia_genetica (
    id INT AUTO_INCREMENT PRIMARY KEY,
    variedad_id INT NOT NULL,
    obtentor VARCHAR(150),
    familia VARCHAR(150),
    grupo VARCHAR(150),
    CONSTRAINT fk_variedad_id_historia_genetica FOREIGN KEY (variedad_id) REFERENCES variedades(id) ON DELETE CASCADE
) ENGINE=INNODB;