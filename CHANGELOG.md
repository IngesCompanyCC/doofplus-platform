# Changelog

Todos los cambios notables de este proyecto se documentan en este archivo.

El formato se basa en [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/) y el proyecto sigue [Semantic Versioning](https://semver.org/lang/es/).

## [Unreleased]
### Added
- Manufacturing: `POST /api/v1/production-orders/{productionOrderId}/rejections` rechaza una orden `Planned` con su motivo (US57): sin motivo responde `400` y si la orden ya no está `Planned`, `409`. La orden queda en el estado nuevo `Rejected` y su recurso incluye `rejectionReason`; una orden rechazada ya no se puede aprobar ni iniciar.
- Organizations: fachada ACL (IOrganizationsContextFacade). Subscriptions la usa para comprobar que la organización existe: POST /api/v1/subscriptions responde 404 si no existe.
### Changed
- **Breaking:** las rutas ya no usan verbos y los sub-recursos que registran un evento van en sustantivo plural: lotes de producción `POST /executions`, `/resumptions`, `/closures` y `/distribution-releases` (antes `/start`, `/resume`, `/close` y `/release`); liberaciones de lote `POST /approvals` y `/rejections` (antes `PUT /approve` y `/reject`); lotes de materia prima `POST /inspections` (antes `PUT /inspection`), `/approvals`, `/rejections` y `/allocations`; fórmulas maestras `POST /approvals`; suscripciones `POST /activations`, `/renewals` y `/cancellations`.
- **Breaking:** `GET /api/v1/production-batches?batchNumber=` y `GET /api/v1/batch-releases?batchId=` reemplazan a `/by-number/{batchNumber}` y `/by-batch/{batchId}`; las colecciones siempre responden una lista.
- `GET /api/v1/equipment?code=` responde una lista vacía cuando ningún equipo tiene ese código, en lugar de `404`.
- `PUT /subscriptions/{id}/plan`, `PUT /production-orders/{id}/status` y `PATCH /production-batches/{id}/progress` siguen en singular: cada recurso tiene un solo plan, estado o avance, que se actualiza.
### Fixed
- Manufacturing: los comandos sobre un producto, una fórmula maestra, una orden de producción, una operación o un lote de materia prima que no existe responden `404` en lugar de `500`. El detalle localizado necesitaba el id y no lo recibía; ahora usa el texto del error, que ya lo incluye.
- OpenAPI: el documento publicado (`/openapi/v1.json`) y Scalar muestran la versión del proyecto (`<Version>` del `.csproj`) en lugar de `1.0.0`.
- OpenAPI y Scalar: detrás del proxy de Render, el servidor del documento usa `https://` y la URL pública, porque el API respeta las cabeceras `X-Forwarded-Proto`, `X-Forwarded-Host` y `X-Forwarded-For`.
### Security
- CORS: solo la Web Application (Firebase), el Website (GitHub Pages) y `http://localhost:5173` pueden llamar al API (`Cors:AllowedOrigins`), en lugar de cualquier origen.
- IAM: la clave que firma los JWT ya no tiene un valor por defecto fuera de Development; el API no arranca sin `TokenSettings__Secret` (mínimo 32 caracteres).
- Todos los endpoints exigen el token JWT, salvo los públicos: sign-in, sign-up, `POST /api/v1/tokens`, `POST /api/v1/users`, `POST /api/v1/contact-inquiries`, `POST /api/v1/telemetry` (con la API key de ThingsBoard), `GET /api/v1/plans` y las llamadas del registro de una organización nueva. Sin token, `GET /api/v1/organizations` solo busca por `ruc` y `POST /api/v1/facilities` solo agrega la primera planta de una organización. Una prueba recorre todos los controllers y falla si un endpoint queda sin `[Authorize]` ni `[AllowAnonymous]`.

## [1.1.1] - 2026-11-25
### Security
- IAM: el registro público (`POST /api/v1/authentication/sign-up` y `POST /api/v1/users`) ya no toma el rol, el privilegio ni la organización del cliente. El primer usuario de una organización queda como administrador; los demás se unen con el rol, la organización y la planta de su invitación pendiente, y sin invitación el API responde `403 Forbidden`.

## [1.1.0] - 2026-11-15
### Added
- IAM: registro e inicio de sesión con JWT y verificación en dos pasos con un código de seis dígitos; usuarios y roles (`/api/v1/users`, `/api/v1/roles`).
- IAM: invitaciones de nuevos miembros (`POST /api/v1/invitations`, `GET /api/v1/invitations?organizationId=`, `GET /api/v1/invitations/{id}`), solo para administradores.
- Organizations: registro de organizaciones y perfiles con los campos de la Web App, búsqueda por RUC y preferencias del perfil.
- Subscriptions: cambio de plan y de ciclo de facturación; pruebas del dominio, de los pagos y de Niubiz.
- Manufacturing: filtro de lotes de producción por número de lote.

### Changed
- Quality & Compliance sigue los diagramas de clases y de base de datos: 13 recursos con las personas por nombre y los registros relacionados por código, `PUT /{id}` para editar, `DELETE /api/v1/evidence/{id}` (409 si ya fue revisada), `POST /api/v1/audit-events` y filtros por código (`?batchCode=`, `?recordCode=`, `?capaCode=`...). Las liberaciones de lotes no cambian.

### Fixed
- Manufacturing: los lotes de materia prima se reciben en muestreo y las órdenes se pueden crear sin operaciones.

## [1.0.0] - 2026-11-14
### Added
- Primera versión de la plataforma con los bounded contexts IAM, Organizations, Subscriptions, Manufacturing, Quality & Compliance e IoT Monitoring.
