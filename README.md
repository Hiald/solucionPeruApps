# solucionPeruApps

1. En el proyecto "backendOlimpiada" debe cambiarse las credenciales dentro del archivo "Web.Config", por defecto está con las credenciales de mi SQL server de acceso local.

Línea 8 : "connectionString="Data Source=JONHIALD\SQLEXPRESS; uid=sa; pwd=12345; Initial Catalog=bdolimpiada"

*esto apunta a la base de datos, siendo como:*

Data Source: la ruta del servidor
uid = usuario
pwd = clave
Initial Catalog = el nombre de la base de datos ("bdolimpiada" se llama)

2. Al ejecutarse el backend, lo hará en la siguiente ruta http://localhost:51596/, cambiar si hay algun servicio ejecutándose con esa ruta.

3. En el archivo "Web.config" del proyecto frontendOlimpiada, debe redirigir a dicha ruta ya mencionada en el punto 3. Por defecto lo está, pero de lo contrario cambiarlo.

Línea 13: <add key="routews" value="http://localhost:51596" /> (esto apunta al proyecto "backendOlimpiada")

4. No hay usuarios registrados, por favor registre una cuenta para que pueda logearse o iniciar sesión en caso contrario
