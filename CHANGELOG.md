# Changelog

Todos los cambios notables de este proyecto se documentan en este archivo.

El formato se basa en [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/) y el proyecto sigue [Semantic Versioning](https://semver.org/lang/es/).

## [Unreleased]
### Changed
- **Breaking:** las rutas ya no usan verbos y los sub-recursos que registran un evento van en sustantivo plural: lotes de producción `POST /executions`, `/resumptions`, `/closures` y `/distribution-releases` (antes `/start`, `/resume`, `/close` y `/release`); liberaciones de lote `POST /approvals` y `/rejections` (antes `PUT /approve` y `/reject`); lotes de materia prima `POST /inspections` (antes `PUT /inspection`), `/approvals`, `/rejections` y `/allocations`; fórmulas maestras `POST /approvals`; suscripciones `POST /activations`, `/renewals` y `/cancellations`.
- **Breaking:** `GET /api/v1/production-batches?batchNumber=` y `GET /api/v1/batch-releases?batchId=` reemplazan a `/by-number/{batchNumber}` y `/by-batch/{batchId}`; las colecciones siempre responden una lista.
- `PUT /subscriptions/{id}/plan`, `PUT /production-orders/{id}/status` y `PATCH /production-batches/{id}/progress` siguen en singular: cada recurso tiene un solo plan, estado o avance, que se actualiza.
### Security
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
