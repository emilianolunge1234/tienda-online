CREATE DATABASE TiendaOnline;
GO
USE TiendaOnline;
GO

CREATE TABLE categorias (
    idCategoria INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_categorias PRIMARY KEY,
    descripcion VARCHAR(100) NOT NULL
);
GO

CREATE TABLE productos (
    idProducto  INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_productos PRIMARY KEY,
    nombre      VARCHAR(100)      NOT NULL,
    precio      DECIMAL(10,2)     NOT NULL,
    idCategoria INT               NOT NULL,
    CONSTRAINT FK_productos_categorias
        FOREIGN KEY (idCategoria) REFERENCES categorias(idCategoria)
);
GO

INSERT INTO categorias (descripcion) VALUES
('Electrónica'), ('Ropa'), ('Hogar'), ('Deportes'), ('Libros');

INSERT INTO productos (nombre, precio, idCategoria) VALUES
('Auriculares inalámbricos', 25999.50, 1),
('Camiseta deportiva',       8999.00,  2),
('Silla ergonómica',         74999.00, 3),
('Pelota de fútbol',         12500.75, 4),
('Libro de recetas',         6300.00,  5),
('Notebook 15"',             899999.99,1);
GO
