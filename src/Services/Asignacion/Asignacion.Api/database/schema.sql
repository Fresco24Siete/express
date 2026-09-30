-- Esquema de base de datos para el servicio de Asignacion de Envío
-- Tabla: asignacion_envio

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS asignacion_envio (
    id_asignacion_envio UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    id_orden UUID NOT NULL,
    id_domiciliario UUID NOT NULL,
    estado VARCHAR(50) NOT NULL DEFAULT 'Asignado',
    fecha_asignacion TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    fecha_estimada_entrega TIMESTAMP WITH TIME ZONE NULL,
    fecha_real_entrega TIMESTAMP WITH TIME ZONE NULL,
    latitud_actual DECIMAL(9,6) NULL,
    longitud_actual DECIMAL(9,6) NULL,
    distancia_km DECIMAL(10,2) NULL,
    tiempo_estimado_min INT NULL,
    numero_intento INT NOT NULL DEFAULT 1,
    razon_fallo TEXT NULL,
    firma_cliente VARCHAR(1000) NULL,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_asignacion_envio_orden ON asignacion_envio(id_orden);
CREATE INDEX IF NOT EXISTS idx_asignacion_envio_domiciliario ON asignacion_envio(id_domiciliario);
CREATE INDEX IF NOT EXISTS idx_asignacion_envio_estado ON asignacion_envio(estado);
