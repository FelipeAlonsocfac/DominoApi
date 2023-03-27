# DominoApi
El proceso para utilizar la app es: llamar al endpoint de Register del usercontroller(/v1/User/Register), el cual devolverá un token y utilizar ese token como header 'Authorization' para autorizarse, sin necesidad de enviar el 'bearer '. También, despues de registrado, se puede llamar al endpoint de login(/v1/User/login) para obtener el token.

Posterior a ello se llama al endpoint de sort en el controlador de Domino el cual 
recibe una lista de strings con el formato "[0-6]|[0-6]"

NOTA: Todos los endpoints tienen validaciones de formato correcto.


El paquete  Microsoft.EntityFrameworkCore.Design y Microsoft.EntityFrameworkCore.SqlServer se usa para hacer scaffold. Para correrlo, setear infrastructure como startup project y correr en la terminal, dentro del proyecto:

Dotnet ef dbcontext scaffold "CONNECTIONSTRING" Microsoft.EntityFrameworkCore.SqlServer -o Scaffold


Tambien, se incluye un archivo de documentacion xml configurado en el swaggergen del startup. Para habilitarlo hay que ir al proyecto (api) y activar la 'Documentation File' en las properties del proyecto Build > Output

Se usan validaciones tanto en filtros como automaticas mediante fluent validation.

Se pensó en usar redisCache pero se desistio de ello (esta implementado de igual forma) debido a que soportaria maximo 165 dominoes y haciendo una prueba de ordenamiento de 368 dominoes aleatorios, solo tardó 30ms. En su lugar, tarda unos 100ms en hacer y recoger la llamada de redis.

También se añadio un dockerfile para correr el proceso de CI pero no hubo el tiempo para crear la github action.


