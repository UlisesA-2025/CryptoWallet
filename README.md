# 🪙 CryptoWallet

> Aplicación web full stack para la gestión de una cartera de criptomonedas con cotizaciones en tiempo real.

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![Vue.js](https://img.shields.io/badge/Vue.js-3.x-4FC08D?style=flat-square&logo=vue.js)](https://vuejs.org/)
[![SQLite](https://img.shields.io/badge/SQLite-003B57?style=flat-square&logo=sqlite)](https://sqlite.org/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](LICENSE)

---

## 📋 Descripción

**CryptoWallet** es una aplicación web full stack que permite llevar el control de una cartera personal de criptomonedas. El sistema integra la API de [CriptoYa](https://criptoya.com/) para obtener precios en tiempo real en pesos argentinos (ARS), y permite registrar, consultar, editar y eliminar operaciones de compra y venta.

Desarrollado como Trabajo Integrador Final de la materia **Programación III** en la **Universidad Tecnológica Nacional (UTN)**.

---

## 🚀 Funcionalidades

| Funcionalidad | Descripción |
|---|---|
| 📥 **Alta de compra** | Registra la compra de criptomonedas. El precio en ARS se calcula automáticamente consultando CriptoYa. |
| 📤 **Alta de venta** | Registra la venta de criptomonedas con validación de stock disponible. |
| 📋 **Historial de transacciones** | Listado completo de todas las operaciones realizadas, ordenadas por fecha. |
| ✏️ **Edición y borrado** | Permite ver, editar o eliminar cualquier transacción con confirmación de borrado. |
| 📊 **Análisis de cartera** | Vista del estado actual: cantidad por criptomoneda, valor en ARS y total de la cartera. |
| 💱 **Cotizaciones en tiempo real** | Integración con la API de CriptoYa para precios actualizados en el momento de cada operación. |

---

## 🛠️ Stack Tecnológico

### Backend
- **ASP.NET Core 8** — API REST con arquitectura por capas
- **C#** — Lógica de negocio y validaciones
- **Entity Framework Core** — ORM para acceso a datos
- **SQLite** — Base de datos relacional local
- **HttpClient** — Consumo de la API externa de CriptoYa

### Frontend
- **Vue.js 3** — Framework progresivo con Composition API
- **Vue Router** — Navegación entre vistas (SPA)
- **Axios** — Cliente HTTP para consumo de la API propia
- **HTML5 / CSS3** — Estructura y estilos de la interfaz

---

## 📁 Estructura del Proyecto

```
CryptoWallet/
├── backend/
│   └── CryptoWallet.Api/
│       ├── Controllers/          # Endpoints REST
│       ├── Models/               # Entidades de dominio
│       ├── DTOs/                 # Objetos de transferencia de datos
│       ├── Services/             # Lógica de negocio
│       └── Data/                 # DbContext y migraciones
└── frontend/
    └── CryptoWallet/
        ├── src/
        │   ├── views/            # Pantallas principales
        │   ├── components/       # Componentes reutilizables
        │   ├── services/         # Llamadas a la API
        │   └── router/           # Configuración de rutas
        └── public/
```

---

## ⚙️ Cómo ejecutar el proyecto

### Prerrequisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [Git](https://git-scm.com/)

### 1. Clonar el repositorio

```bash
git clone https://github.com/UlisesA-2025/CryptoWallet.git
cd CryptoWallet
```

### 2. Levantar el Backend

```bash
cd backend/CryptoWallet.Api
dotnet restore
dotnet ef database update   # Crea la base de datos SQLite
dotnet run
```

La API quedará disponible en: `https://localhost:7XXX` (ver consola)

### 3. Levantar el Frontend

```bash
cd frontend/CryptoWallet
npm install
npm run dev
```

La app quedará disponible en: `http://localhost:5173`

---

## 🔌 Endpoints de la API

| Método | Endpoint | Descripción |
|---|---|---|
| `GET` | `/api/transactions` | Obtiene todas las transacciones |
| `GET` | `/api/transactions/{id}` | Obtiene una transacción por ID |
| `POST` | `/api/transactions` | Crea una nueva transacción (compra o venta) |
| `PATCH` | `/api/transactions/{id}` | Edita una transacción existente |
| `DELETE` | `/api/transactions/{id}` | Elimina una transacción |
| `GET` | `/api/portfolio` | Obtiene el estado actual de la cartera |

---

## 💡 Criptomonedas soportadas

| Símbolo | Nombre |
|---|---|
| BTC | Bitcoin |
| ETH | Ethereum |
| USDC | USD Coin |

---

## 🔗 API Externa

Este proyecto consume la API pública de **[CriptoYa](https://criptoya.com/api)** para obtener las cotizaciones actuales de criptomonedas en Pesos Argentinos (ARS) en tiempo real.

**Ejemplo de consulta:**
```
GET https://criptoya.com/api/satoshitango/btc/ars/
```

---

## 📸 Capturas de pantalla

> *(Próximamente — se agregarán capturas de las vistas principales)*

---

## 🎓 Contexto académico

Trabajo Integrador Final — **Programación III**
**Universidad Tecnológica Nacional (UTN)**
Año: 2025

---

## 👤 Autor

**Ulises A.**
[![GitHub](https://img.shields.io/badge/GitHub-UlisesA--2025-181717?style=flat-square&logo=github)](https://github.com/UlisesA-2025)

---

## 📄 Licencia

Este proyecto está bajo la licencia MIT. Ver el archivo [LICENSE](LICENSE) para más detalles.
