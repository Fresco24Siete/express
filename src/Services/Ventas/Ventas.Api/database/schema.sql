-- Esquema de base de datos para el Servicio de Ventas
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- 1. Tabla: Carrito
CREATE TABLE IF NOT EXISTS carrito (
    id_carrito BIGSERIAL PRIMARY KEY,
    precio_total DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    numero_articulos INT NOT NULL DEFAULT 0,
    expirado_en TIMESTAMP WITH TIME ZONE NULL,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 2. Tabla: Item Carrito
CREATE TABLE IF NOT EXISTS item_carrito (
    id_item_carrito UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    id_carrito BIGINT NOT NULL,
    id_producto UUID NOT NULL,
    cantidad INT NOT NULL DEFAULT 1,
    precio_unitario DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_item_carrito_carrito FOREIGN KEY (id_carrito) REFERENCES carrito(id_carrito) ON DELETE CASCADE
);

-- 3. Tabla: Orden
CREATE TABLE IF NOT EXISTS orden (
    id_orden UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    numero_orden VARCHAR(50) NOT NULL UNIQUE,
    id_usuario UUID NOT NULL,
    id_direccion_envio UUID NOT NULL,
    id_carrito BIGINT NULL,
    estado VARCHAR(50) NOT NULL DEFAULT 'pendiente_pago',
    precio_subtotal DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    precio_descuento DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    precio_envio DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    precio_total DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    codigo_descuento VARCHAR(50) NULL,
    notas_cliente TEXT NULL,
    fecha_pagada TIMESTAMP WITH TIME ZONE NULL,
    fecha_cancelacion TIMESTAMP WITH TIME ZONE NULL,
    razon_cancelacion TEXT NULL,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_orden_carrito FOREIGN KEY (id_carrito) REFERENCES carrito(id_carrito) ON DELETE SET NULL,
    CONSTRAINT fk_orden_usuario FOREIGN KEY (id_usuario) REFERENCES usuario(id_usuario) ON DELETE RESTRICT,
    CONSTRAINT fk_orden_direccion FOREIGN KEY (id_direccion_envio) REFERENCES direccion(id_direccion) ON DELETE RESTRICT
);

-- 4. Tabla: Detalle de Orden
CREATE TABLE IF NOT EXISTS detalle_orden (
    id_detalle_orden UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    id_orden UUID NOT NULL,
    id_producto UUID NOT NULL,
    cantidad INT NOT NULL DEFAULT 1,
    precio_unitario DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    subtotal DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    CONSTRAINT fk_detalle_orden_orden FOREIGN KEY (id_orden) REFERENCES orden(id_orden) ON DELETE CASCADE
);

-- 5. Tabla: Pago
CREATE TABLE IF NOT EXISTS pago (
    id_pago UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    id_orden UUID NOT NULL UNIQUE,
    monto DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    moneda VARCHAR(3) NOT NULL DEFAULT 'COP',
    metodo VARCHAR(50) NOT NULL DEFAULT 'tarjeta_credito',
    estado VARCHAR(50) NOT NULL DEFAULT 'pendiente',
    referencia_externo VARCHAR(255) NULL,
    token_encriptado VARCHAR(500) NULL,
    ultimos_4_digitos VARCHAR(4) NULL,
    fecha_intento TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    fecha_completado TIMESTAMP WITH TIME ZONE NULL,
    razon_fallo TEXT NULL,
    created_by UUID NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_pago_orden FOREIGN KEY (id_orden) REFERENCES orden(id_orden) ON DELETE CASCADE,
    CONSTRAINT fk_pago_usuario FOREIGN KEY (created_by) REFERENCES usuario(id_usuario) ON DELETE RESTRICT
);

-- 6. Tabla: Devolucion
CREATE TABLE IF NOT EXISTS devolucion (
    id_devolucion UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    id_orden UUID NOT NULL,
    id_detalle_orden UUID NOT NULL,
    cantidad_devuelta INT NOT NULL DEFAULT 1,
    razon VARCHAR(50) NOT NULL,
    estado VARCHAR(50) NOT NULL DEFAULT 'solicitada',
    monto_reembolso DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    fecha_solicitud TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    fecha_resolucion TIMESTAMP WITH TIME ZONE NULL,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_devolucion_orden FOREIGN KEY (id_orden) REFERENCES orden(id_orden) ON DELETE CASCADE,
    CONSTRAINT fk_devolucion_detalle FOREIGN KEY (id_detalle_orden) REFERENCES detalle_orden(id_detalle_orden) ON DELETE RESTRICT
);

-- Índices para optimizar consultas
CREATE INDEX IF NOT EXISTS idx_item_carrito_carrito ON item_carrito(id_carrito);
CREATE INDEX IF NOT EXISTS idx_item_carrito_producto ON item_carrito(id_producto);
CREATE INDEX IF NOT EXISTS idx_orden_usuario ON orden(id_usuario);
CREATE INDEX IF NOT EXISTS idx_orden_numero ON orden(numero_orden);
CREATE INDEX IF NOT EXISTS idx_orden_estado ON orden(estado);
CREATE INDEX IF NOT EXISTS idx_detalle_orden_orden ON detalle_orden(id_orden);
CREATE INDEX IF NOT EXISTS idx_detalle_orden_producto ON detalle_orden(id_producto);
CREATE INDEX IF NOT EXISTS idx_pago_orden ON pago(id_orden);
CREATE INDEX IF NOT EXISTS idx_pago_estado ON pago(estado);
CREATE INDEX IF NOT EXISTS idx_devolucion_orden ON devolucion(id_orden);
CREATE INDEX IF NOT EXISTS idx_devolucion_detalle ON devolucion(id_detalle_orden);
CREATE INDEX IF NOT EXISTS idx_devolucion_estado ON devolucion(estado);
