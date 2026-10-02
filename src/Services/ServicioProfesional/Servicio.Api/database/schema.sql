-- Esquema de base de datos para el Servicio de Servicios Profesionales
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- 1. Tabla: Categoria Servicio
CREATE TABLE IF NOT EXISTS categoria_servicio (
    id_categoria_servicio BIGSERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT NULL,
    duracion_promedio_horas INT NULL,
    requiere_ubicacion BOOLEAN NOT NULL DEFAULT true,
    precio_base_sugerido DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    activa BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 2. Tabla: Servicio
CREATE TABLE IF NOT EXISTS servicio (
    id_servicio UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    id_cliente UUID NOT NULL,
    id_emprendedor UUID NOT NULL,
    id_categoria_servicio BIGINT NOT NULL,
    descripcion_solicitud TEXT NOT NULL,
    ubicacion_lat DECIMAL(9,6) NULL,
    ubicacion_lon DECIMAL(9,6) NULL,
    fecha_estimada_inicio TIMESTAMP WITH TIME ZONE NULL,
    fecha_real_inicio TIMESTAMP WITH TIME ZONE NULL,
    duracion_estimada_horas INT NULL,
    duracion_real_horas DECIMAL(4,2) NULL,
    costo_base DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    costo_adicional DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    costo_total DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    estado VARCHAR(50) NOT NULL DEFAULT 'solicitado',
    razon_cancelacion TEXT NULL,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Índices para optimizar consultas
CREATE INDEX IF NOT EXISTS idx_servicio_cliente ON servicio(id_cliente);
CREATE INDEX IF NOT EXISTS idx_servicio_emprendedor ON servicio(id_emprendedor);
CREATE INDEX IF NOT EXISTS idx_servicio_categoria ON servicio(id_categoria_servicio);
CREATE INDEX IF NOT EXISTS idx_servicio_estado ON servicio(estado);
CREATE INDEX IF NOT EXISTS idx_servicio_fecha_estimada ON servicio(fecha_estimada_inicio);
CREATE INDEX IF NOT EXISTS idx_categoria_servicio_activa ON categoria_servicio(activa);
