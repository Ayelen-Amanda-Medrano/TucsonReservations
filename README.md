🌟 Tucson Reservations API 🌟
API para la gestión de reservas de mesas en un restaurante y manejo de clientes en lista de espera.

✨ Tecnologías y Componentes Clave
- .NET 8
- ASP.NET Core Web API
- MediatR (Patrón CQRS): Implementación del patrón Command Query Responsibility Segregation, promoviendo una clara separación entre las operaciones de lectura y escritura para mejorar la mantenibilidad y escalabilidad.
- AutoMapper (Mapeo de Objetos)
- Swashbuckle.AspNetCore (Swagger)
- NSubstitute (Mocks en Pruebas): Una biblioteca de mocking amigable para la creación de stubs y mocks en pruebas unitarias.
- XUnit (Framework de Pruebas)
- FluentAssertions (Asserts Expresivos): Proporciona un conjunto de métodos de aserción más legibles y expresivos para las pruebas unitarias.

🏗️ Arquitectura del Proyecto
El proyecto se organiza en capas bien definidas, siguiendo los principios SOLID:

• TucsonReservations.API (Capa de Presentación): Actúa como el punto de entrada de la API, gestionando las solicitudes HTTP entrantes y formateando las respuestas HTTP salientes. Delega la lógica de negocio a la capa Application.

• TucsonReservations.Application (Capa de Lógica de Negocio): Contiene la implementación de los casos de uso del sistema y la orquestación de la lógica del dominio. Define las interfaces que son implementadas por la capa Infrastructure.

• TucsonReservations.Domain (Capa del Modelo del Dominio): Representa el modelo del negocio, incluyendo las entidades, los objetos de valor y las reglas de negocio. Es independiente de cualquier detalle de implementación o framework externo.

• TucsonReservations.Infrastructure (Capa de Implementación): Alberga las implementaciones concretas de las interfaces definidas en otras capas, como la interacción con bases de datos, servicios externos o cualquier otro detalle de infraestructura.

⚙️ Endpoints Principales de la API

Reservas (/api/reservations)
- POST /api/reservations: Permite la creación de una nueva reserva de mesa.
- GET /api/reservations: Recupera la lista de todas las reservas existentes.
- DELETE /api/reservations: Elimina una reserva específica por fecha y numero de mesa.
  
Lista de Espera (/api/waiting-list)
- GET /api/waiting-list: Obtiene la lista de clientes actualmente en espera.
  
🧪 Pruebas Unitarias
Se incluye un proyecto dedicado para las pruebas unitarias, asegurando la calidad y la correctitud de la lógica de negocio:
Proyecto: TucsonReservations.UnitTests
Ejecución: Utilice el comando dotnet test para ejecutar todas las pruebas unitarias.

💡 Consideraciones de Diseño
La gestión de la disponibilidad de mesas se mantiene en memoria para esta implementación.
La lista de espera prioriza a los clientes según su categoría, permitiendo un trato preferencial basado en la lealtad.
 
👤 Clientes Predeterminados
La API incluye la siguiente configuración inicial de clientes:

JSON

- { "MemberNumber": 1, "Category": "Classic" }
- { "MemberNumber": 2, "Category": "Gold" }
- { "MemberNumber": 3, "Category": "Platinum" },
- { "MemberNumber": 4, "Category": "Diamond" }



