INSERT INTO personas (id, nombre, apellido, edad, nacionalidad, documento_identidad, genero) VALUES
(1, 'Juan', 'Pérez', 30, 'Mexicano', 12345678, 'Masculino'),
(2, 'Ana', 'Gómez', 25, 'Colombiana', 87654321, 'Femenino'),
(3, 'Carlos', 'López', 45, 'Brasileño', 98765432, 'Masculino'),
(4, 'María', 'Rodríguez', 35, 'Española', 23456789, 'Femenino'),
(5, 'Ricardo', 'Silva', 50, 'Chileno', 11223344, 'Masculino'),
(6, 'Elena', 'Martínez', 22, 'Argentina', 55667788, 'Femenino'),
(7, 'Pedro', 'García', 41, 'Peruano', 99001122, 'Masculino'),
(8, 'Sofía', 'Fernández', 28, 'Uruguaya', 44556677, 'Femenino'),
(9, 'Daniel', 'Torres', 33, 'Ecuatoriano', 88990011, 'Masculino'),
(10, 'Laura', 'Díaz', 29, 'Venezolana', 77889900, 'Femenino');

INSERT INTO usuarios (id, email, password_hash, rol) VALUES
(1, 'juan.perez@email.com', '$2a$10$f/9MhF3S4/d8F4w.x3/3fO.o/kY.y2/5v.o7s/4f.1w.t.6/2v/jA2xSj', 'admin'),
(2, 'ana.gomez@email.com', '$2a$10$g/8NfE2T3/d9G5y.z4/4gP.p/lZ.z3/6w.p8t/5g.2x.u.7/3w/kC3yTk', 'usuario'),
(3, 'carlos.lopez@email.com', '$2a$10$h/7OeF1U2/c7H4z.a3/5hQ.q/mX.x4/7x.q9u/6h.3y.v.8/4x/lD4zUl', 'usuario'),
(4, 'maria.rodriguez@email.com', '$2a$10$i/6PfG0V1/e6I5y.b4/6iR.r/nY.y5/8y.r1v/7i.4z.w.9/5y/mE5aVm', 'usuario'),
(5, 'ricardo.silva@email.com', '$2a$10$j/5QfE8T7/f7J6z.c5/7jS.s/oZ.z6/9z.s2w/8j.5a.x.0/6z/nF6bWn', 'admin'),
(6, 'elena.martinez@email.com', '$2a$10$k/4RfF9U8/g8K7z.d6/8kT.t/pA.a7/0a.t3x/9k.6b.y.1/7a/oG7cWo', 'usuario');

INSERT INTO variedades (nombre_comun, nombre_cientifico, imagen_ruta, descripcion, porte, tamano_grano, altitud_optima, potencial_rendimiento, calidad_grano_altitud, resistencia_roya, resistencia_antracnosis, resistencia_nematodos) VALUES
('Typica', 'Coffea arabica var. typica', 'assets/images/typica.jpg', 'Una de las variedades de café más antiguas y originales.', 'Alto', 'Grande', 1500.00, 'Medio', 'Nivel 4', 'Susceptible', 'Susceptible', 'Susceptible'),
('Caturra', 'Coffea arabica var. caturra', 'assets/images/caturra.jpg', 'Mutación de la variedad Bourbon. Es popular por su alto rendimiento.', 'Bajo', 'Medio', 1200.00, 'Alto', 'Nivel 3', 'Susceptible', 'Susceptible', 'Susceptible'),
('Castillo', 'Coffea arabica var. castillo', 'assets/images/castillo.jpg', 'Variedad colombiana conocida por su resistencia a la roya.', 'Bajo', 'Medio', 1400.00, 'Excepcional', 'Nivel 5', 'Resistente', 'Tolerante', 'Tolerante'),
('Geisha', 'Coffea arabica var. geisha', 'assets/images/geisha.jpg', 'Famosa por su perfil de sabor floral y exótico.', 'Alto', 'Pequeño', 1700.00, 'Bajo', 'Nivel 5', 'Susceptible', 'Susceptible', 'Susceptible'),
('Bourbon', 'Coffea arabica var. bourbon', 'assets/images/bourbon.jpg', 'Variedad muy conocida que da origen a muchas otras, con un sabor dulce y complejo.', 'Alto', 'Grande', 1600.00, 'Medio', 'Nivel 4', 'Susceptible', 'Susceptible', 'Susceptible'),
('Pacamara', 'Coffea arabica var. pacamara', 'assets/images/pacamara.jpg', 'Híbrido de Pacas y Maragogipe, conocido por su grano grande y acidez brillante.', 'Bajo', 'Grande', 1300.00, 'Medio', 'Nivel 3', 'Susceptible', 'Susceptible', 'Susceptible'),
('Maragogipe', 'Coffea arabica var. maragogipe', 'assets/images/maragogipe.jpg', 'También conocida como "café elefante" por el gran tamaño de sus granos.', 'Alto', 'Grande', 1100.00, 'Muy bajo', 'Nivel 2', 'Susceptible', 'Susceptible', 'Susceptible'),
('Catimor', 'Coffea arabica var. catimor', 'assets/images/catimor.jpg', 'Híbrido de Caturra y Híbrido de Timor, conocido por su alta resistencia a enfermedades.', 'Bajo', 'Medio', 900.00, 'Alto', 'Nivel 2', 'Resistente', 'Resistente', 'Resistente');

INSERT INTO atributos_agronomicos (variedad_id, tiempo_cosecha, maduracion, nutricion, densidad_siembra) VALUES
(1, '24 a 26 semanas', 'Tardía', 'Requiere nutrición equilibrada', '3000-5000 plantas/ha'),
(2, '20 a 22 semanas', 'Temprana', 'Alta demanda de nitrógeno', '5000-8000 plantas/ha'),
(3, '22 a 24 semanas', 'Media', 'Buena respuesta a potasio y fósforo', '4000-7000 plantas/ha'),
(4, '26 a 28 semanas', 'Tardía', 'Requiere micro-nutrientes específicos', '2000-4000 plantas/ha'),
(5, '24 a 26 semanas', 'Tardía', 'Alta demanda de boro y zinc', '3000-5000 plantas/ha'),
(6, '22 a 24 semanas', 'Media', 'Requiere suelos ricos en materia orgánica', '2500-4500 plantas/ha'),
(7, '28 a 30 semanas', 'Muy tardía', 'Demanda alta de fósforo y potasio', '1500-3000 plantas/ha'),
(8, '18 a 20 semanas', 'Muy temprana', 'Alta demanda de nitrógeno y calcio', '6000-9000 plantas/ha');

INSERT INTO historia_genetica (variedad_id, obtentor, familia, grupo) VALUES
(1, 'Desconocido', 'Arabica', 'Typica'),
(2, 'Desconocido', 'Bourbon', 'Typica'),
(3, 'Cenicafé (Colombia)', 'Caturra x Híbrido de Timor', 'Compuesto'),
(4, 'Gesha, Etiopía', 'Ethiopian Landrace', 'Typica'),
(5, 'Isla de Bourbon (La Reunión)', 'Ethiopian Landrace', 'Bourbon'),
(6, 'Procafe, El Salvador', 'Pacas x Maragogipe', 'Híbrido'),
(7, 'Maragogipe, Brasil', 'Mutación de Typica', 'Typica'),
(8, 'IPB, Portugal', 'Caturra x Híbrido de Timor', 'Híbrido');
