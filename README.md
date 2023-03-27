# DominoApi
El paquete  Microsoft.EntityFrameworkCore.Design y Microsoft.EntityFrameworkCore.SqlServer se usa para hacer scaffold. Para correrlo, setear infrastructure como startup project y correr en la terminal, dentro del proyecto:

Dotnet ef dbcontext scaffold "CONNECTIONSTRING" Microsoft.EntityFrameworkCore.SqlServer -o Scaffold


Tambien, se incluye un archivo de documentacion xml configurado en el swaggergen del startup. Para habilitarlo hay que ir al proyecto (api) y activar la 'Documentation File' en las properties del proyecto Build > Output

Se usan validaciones tanto en filtros como automaticas mediante fluent validation.

Se pensó en usar redisCache pero se desistio de ello (esta implementado de igual forma) debido a que soportaria maximo 165 dominoes y haciendo una prueba de ordenamiento de 368 dominoes aleatorios, solo tardó 30ms. En su lugar, tarda unos 100ms en hacer y recoger la llamada de redis.

También se añadio un dockerfile para correr el proceso de CI.
