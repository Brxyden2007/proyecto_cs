-- DML (Data Manipulation Language) para la base de datos 'proyecto_cs'
-- Este script inserta datos de ejemplo en todas las tablas para poblar la base de datos.
-- Se ha creado siguiendo el orden de las dependencias para evitar errores de clave foránea.

-- ======================================================================
-- 1. Tablas sin dependencias de claves foráneas
-- ======================================================================

-- --------------------------------------------------
-- Insertar datos en la tabla 'personas'
-- --------------------------------------------------
-- Se insertan registros de personas que luego se asociarán con usuarios y administradores.
INSERT INTO personas (nombre, apellido, edad, nacionalidad, documento_identidad, genero) VALUES
('Juan', 'Pérez', 35, 'Colombiana', 1018456789, 'Masculino'),
('Ana', 'Gómez', 28, 'Mexicana', 2029567890, 'Femenino'),
('Carlos', 'López', 45, 'Brasileño', 3031678901, 'Masculino'),
('María', 'Rodríguez', 22, 'Argentina', 4042789012, 'Femenino'),
('Diego', 'Sánchez', 31, 'Colombiano', 5053890123, 'Masculino'),
('Luisa', 'Fernández', 33, 'Chilena', 6064901234, 'Femenino');

-- --------------------------------------------------
-- Insertar datos en la tabla 'portes'
-- --------------------------------------------------
-- Catálogo de tipos de porte de las plantas de café.
INSERT INTO portes (nombre) VALUES
('Compacto'),
('Alto'),
('Mediano');

-- --------------------------------------------------
-- Insertar datos en la tabla 'tamanios_grano'
-- --------------------------------------------------
-- Catálogo de tamaños de grano de café.
INSERT INTO tamanios_grano (nombre) VALUES
('Pequeño'),
('Mediano'),
('Grande');

-- --------------------------------------------------
-- Insertar datos en la tabla 'altitudes'
-- --------------------------------------------------
-- Catálogo de rangos de altitud para el cultivo.
INSERT INTO altitudes (rango) VALUES
('Baja (0-1000 msnm)'),
('Media (1000-1500 msnm)'),
('Alta (1500-2000 msnm)'),
('Muy alta (> 2000 msnm)');

-- --------------------------------------------------
-- Insertar datos en la tabla 'rendimientos'
-- --------------------------------------------------
-- Catálogo de niveles de rendimiento de la planta.
INSERT INTO rendimientos (nivel) VALUES
('Bajo'),
('Medio'),
('Alto'),
('Muy alto');

-- --------------------------------------------------
-- Insertar datos en la tabla 'calidades_altitud'
-- --------------------------------------------------
-- Catálogo de calidades según la altitud.
INSERT INTO calidades_altitud (nivel) VALUES
('Baja'),
('Media'),
('Alta'),
('Muy alta');

-- --------------------------------------------------
-- Insertar datos en la tabla 'resistencias'
-- --------------------------------------------------
-- Catálogo de resistencias a enfermedades con su respectivo nivel.
INSERT INTO resistencias (enfermedad, nivel) VALUES
('Roya del café', 'Alta'),
('Ojo de gallo', 'Media'),
('Mal de la tela', 'Baja'),
('Bichos del cafeto', 'Alta'),
('Broca', 'Baja');

-- ======================================================================
-- 2. Tablas con dependencias básicas
-- ======================================================================

-- --------------------------------------------------
-- Insertar datos en la tabla 'administradores'
-- --------------------------------------------------
-- Se asocian los registros de 'personas' a roles de administrador.
-- Se usa un hash de contraseña de ejemplo (el password real sería 'admin123').
INSERT INTO administradores (id_administradores, email, password_hash) VALUES
(1, 'juan.perez@proyecto_cs.com', '$2a$12$R.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0e'),
(2, 'ana.gomez@proyecto_cs.com', '$2a$12$R.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0e');

-- --------------------------------------------------
-- Insertar datos en la tabla 'usuarios'
-- --------------------------------------------------
-- Se asocian los registros de 'personas' a roles de usuario.
-- Se usa un hash de contraseña de ejemplo (el password real sería 'user123').
INSERT INTO usuarios (id_usuario, email, password_hash) VALUES
(3, 'carlos.lopez@proyecto_cs.com', '$2a$12$p.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0e'),
(4, 'maria.rodriguez@proyecto_cs.com', '$2a$12$p.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0e'),
(5, 'diego.sanchez@proyecto_cs.com', '$2a$12$p.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0e'),
(6, 'luisa.fernandez@proyecto_cs.com', '$2a$12$p.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0eR.1yYlS0e');

-- --------------------------------------------------
-- Insertar datos en la tabla 'variedades'
-- --------------------------------------------------
-- Variedades de café, con sus respectivos FK a las tablas de catálogos.
INSERT INTO variedades (nombre_comun, nombre_cientifico, descripcion, imagen_url, id_porte, id_tamano, id_altitud, id_rendimiento, id_calidad) VALUES
('Caturra', 'Coffea arabica var. Caturra', 'Variedad de porte compacto y alta producción, originaria de Brasil. Muy popular en Latinoamérica.', 'https://example.com/imagen_caturra.jpg', 1, 2, 3, 4, 3),
('Bourbon', 'Coffea arabica var. Bourbon', 'Una de las variedades de café más antiguas y conocidas, con granos de alta calidad. Requiere altitudes altas.', 'https://example.com/imagen_bourbon.jpg', 2, 3, 4, 2, 4),
('Colombia', 'Coffea arabica var. Colombia', 'Variedad de porte mediano, resistente a la roya del café. Desarrollada en Colombia.', 'https://example.com/imagen_colombia.jpg', 3, 2, 3, 3, 3),
('Geisha', 'Coffea arabica var. Geisha', 'Conocida por su excepcional calidad en taza, con notas florales y frutales. Requiere un manejo cuidadoso.', 'https://example.com/imagen_geisha.jpg', 2, 3, 4, 1, 4),
('Pacamara', 'Coffea arabica var. Pacamara', 'Un híbrido de Pacas y Maragogipe, conocido por su grano grande y perfil de sabor complejo.', 'https://example.com/imagen_pacamara.jpg', 2, 3, 3, 2, 4),
('Catimor', 'Híbrido de Caturra y Timor', 'Híbrido robusto con alta resistencia a enfermedades, aunque a veces sacrifica un poco la calidad en taza.', 'https://example.com/imagen_catimor.jpg', 1, 2, 2, 4, 2);

-- ======================================================================
-- 3. Tablas con dependencias complejas (relaciones N:M, etc.)
-- ======================================================================

-- --------------------------------------------------
-- Insertar datos en la tabla 'variedad_resistencia'
-- --------------------------------------------------
-- Tabla de relación entre variedades y sus resistencias.
INSERT INTO variedad_resistencia (id_variedad, id_resistencia) VALUES
(1, 1), -- Caturra vs Roya del café
(1, 2), -- Caturra vs Ojo de gallo
(2, 2), -- Bourbon vs Ojo de gallo
(3, 1), -- Colombia vs Roya del café (principal resistencia)
(4, 2), -- Geisha vs Ojo de gallo
(5, 4), -- Pacamara vs Bichos del cafeto
(6, 1), -- Catimor vs Roya del café (principal resistencia)
(6, 4); -- Catimor vs Bichos del cafeto

-- --------------------------------------------------
-- Insertar datos en la tabla 'atributos_agronomicos'
-- --------------------------------------------------
-- Atributos agronómicos detallados para cada variedad.
INSERT INTO atributos_agronomicos (id_variedad, tiempo_cosecha, maduracion, nutricion, densidad_siembra) VALUES
(1, '24 a 30 meses', 'Temprana', 'Requiere una nutrición balanceada de N-P-K', '3,000-5,000 plantas/ha'),
(2, '36 a 48 meses', 'Tardía', 'Beneficia la aplicación de microelementos', '1,500-3,000 plantas/ha'),
(3, '20 a 24 meses', 'Temprana', 'Buena respuesta a la fertilización foliar', '3,000-5,000 plantas/ha'),
(4, '36 a 48 meses', 'Tardía', 'Sensible a la falta de nutrientes', '1,000-2,000 plantas/ha'),
(5, '24 a 30 meses', 'Media', 'Requiere suelos ricos en materia orgánica', '2,500-4,000 plantas/ha'),
(6, '24 a 30 meses', 'Temprana', 'Se adapta bien a diferentes planes de nutrición', '4,000-6,000 plantas/ha');

-- --------------------------------------------------
-- Insertar datos en la tabla 'historias_geneticas'
-- --------------------------------------------------
-- Historia y linaje genético de las variedades.
INSERT INTO historias_geneticas (id_variedad, obtentor, familia, grupo, descripcion) VALUES
(1, 'Centro de Genética de Campinas', 'Bourbon', 'Typica', 'Mutación natural de la variedad Bourbon, descubierta en Brasil.'),
(2, 'Misioneros franceses', 'Typica', 'Typica', 'Introducida desde la isla de Reunión (anteriormente Bourbon) a la península arábiga.'),
(3, 'Cenicafé', 'Catimor', 'Híbrido', 'Híbrido entre la variedad Caturra y el Híbrido de Timor.'),
(4, 'N/A', 'Etíope', 'Typica', 'Descubierta en la región de Gesha, Etiopía, pero popularizada en Panamá.'),
(5, 'Centro de Investigaciones en Café de El Salvador', 'Bourbon', 'Híbrido', 'Híbrido de las variedades Pacas y Maragogipe.'),
(6, 'Instituto de Investigación Agrícola de Portugal', 'Catimor', 'Híbrido', 'Cruza entre Caturra y el Híbrido de Timor.');

-- --------------------------------------------------
-- Insertar datos en la tabla 'logs_acciones'
-- --------------------------------------------------
-- Registros de las acciones de los usuarios.
INSERT INTO logs_acciones (id_usuario, accion) VALUES
(3, 'Inició sesión en el sistema'),
(4, 'Generó un catálogo en PDF'),
(3, 'Consultó la variedad Bourbon'),
(5, 'Creó un nuevo usuario'),
(6, 'Actualizó su perfil');

-- --------------------------------------------------
-- Insertar datos en la tabla 'pdf_catalogos'
-- --------------------------------------------------
-- Registros de los PDFs generados por los usuarios.
INSERT INTO pdf_catalogos (id_usuario, filtros_usados, ruta_pdf) VALUES
(4, '{"porte": "Compacto", "altitud": "Alta"}', '/catalogs/pdf_4_202508131.pdf'),
(3, '{"rendimiento": "Alto", "resistencia": "Roya del café"}', '/catalogs/pdf_3_202508132.pdf'),
(5, '{"calidad": "Muy alta"}', '/catalogs/pdf_5_202508133.pdf');