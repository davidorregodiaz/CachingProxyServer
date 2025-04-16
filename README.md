# 🧠 Caching Proxy Server

Un simple servidor proxy con caché implementado en C# que intercepta peticiones HTTP y almacena en memoria las respuestas para mejorar el rendimiento y reducir el tráfico hacia el servidor de origen.

---

## 📦 Características

- Escucha peticiones HTTP en un puerto local configurable.
- Reenvía solicitudes al servidor de origen (--origin).
- Guarda en caché las respuestas basadas en método y URL (clave única: METHOD:URL).
- Devuelve respuestas en caché si están disponibles (CACHE HIT) o consulta el servidor original (CACHE MISS).
- Agrega cabecera Cache-Control a las respuestas indicando si fue un hit o miss.

---

## 🛠️ Requisitos

- [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download)
- Sistema operativo Windows, Linux o macOS

## Ejemplo de uso:
--port 5000 --origin https://jsonplaceholder.typicode.com

## 💡 Futuras Funciones

✅ Soporte para diferentes métodos HTTP (POST, PUT, DELETE): actualmente solo funciona bien con GET.

✅ Agregar expiración de caché: por ejemplo, usar MemoryCache de .NET con políticas de vencimiento.

✅ Guardar cuerpo de respuesta como binario para soportar imágenes u otros tipos de contenido.

✅ Soporte para cabeceras personalizadas al reenviar peticiones.

✅ Interfaz web o CLI interactiva para visualizar estadísticas de caché.

✅ Logs estructurados usando ILogger o Serilog.

https://roadmap.sh/projects/caching-server
