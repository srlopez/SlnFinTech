# FinTech familiar
Una familia decide centralizar todos los gastos familiares en una aplicación. 

Cada mienbro generará registros de gastos que indican quién, cuándo, cuánto y para qué (clasificado el gasto en categoría y subcategoría), etc…   
Se desea controlar quién puede ingresar un gasto y se desea obtener `resúmenes de gastos` de distintos tipos.   


La aplicación presentará un menu para `usuario sin identificar`, en el que sólo podrá consultar las Categorías, un menu para `usuarios identificados`, que podrá consultar los registros de gastos e ingresar registros, y un `modo de administración` para gestionar las categorías.

Ejemplo de datos
**Gastos (ejemplos)**
```
FECHA,          USUAR, CAT, SUBCAT, IMPORTE, DETALLE
18-12-21 18:18, luis,   3,  31,     123.45, Primer Apunte
18-12-21 18:27, luis,   5,  51,     123.00, Libros
18-12-21 18:32, ama,    3,  32,     123.00, Cinesa Urbil
18-12-21 19:40, santi,  4,  41,     200.30, comida Los Riojanos
18-12-21 19:42, aita,   6,  60,     23.00,  Garbanzos ecológicos
19-12-21 10:54, santi,  2,  21,     200.34, Un parking caro
19-12-21 10:58, santi,  4,  41,     0.34,   EL Sábado pasado
19-12-21 12:10, aroa,   4,4 1,      34.56,  Más copas en Donosti
```
**Categorías y Subcategorías (ejemplos)**
```
0,1,Hogar
0,2,Vehículos
0,3,Personal y aficiones
0,4,Social y Restauración
0,5,Consumo familiar
0,6,Inversión en Salud
0,9,Otros
1,10,Luz/Electricidad
1,11,Gas/Butano
1,12,Teléfono/Internet
1,13,Seguros
1,14,Reparaciones/Mantenimiento
1,15,Reformas
1,16,Decoración
1,19,Otros
2,20,Gasolína/Gasoil
2,21,Parking/Estacionamiento
2,22,Reparaciones/Mantenimiento
2,23,Seguros
2,29,Otros
3,30,Libros/Revistas
3,31,Música/Instrumentos
3,32,Cine/Teatro/Espectáculos
3,33,Deporte
3,34,Hobbies
3,35,eSubscripciones
3,36,Ropa/Moda
3,39,Otros
4,40,Comidas
4,41,Copas y fiestas
4,49,Otros
5,50,Educación
5,51,Extra escolares
5,52,Pensiones
5,59,Otros
6,60,Medicinas/Complementos
6,61,Consultas/Revisiones 
6,62,Tratamientos
6,69,Otros
9,90,Compra Hogar
9,91,Compra Vehículo
9,92,Préstamos
9,93,Compras Mercados
9,99,Otros
```

