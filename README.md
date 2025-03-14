# 📌 Strade - Backend

## 📖 Índice
1. 📌 [Problemática](#problemática)
2. 🎯 [Objetivo General](#objetivo-general)
3. 📢 [Justificación](#justificación)
4. 🏗️ [Arquitectura](#arquitectura)
5. 👥 [Integrantes](#integrantes)
6. 📦 [Librerías Utilizadas](#librerías-utilizadas)
7. ⚙️ [Cómo Correr el Proyecto](#cómo-correr-el-proyecto)
8. 🤝 [Contribución](#contribución)
9. 📩 [Contacto](#contacto)
10. ⚖️ [Derechos Reservados](#derechos-reservados)

---

## 🛑 Problemática
En la actualidad, muchas personas tienen objetos que ya no utilizan pero que pueden ser valiosos para otros. Sin embargo, las plataformas existentes para la compra y venta de productos no facilitan el intercambio directo, generando limitaciones para quienes desean realizar trueques sin necesidad de transacciones monetarias. Esto ocasiona desperdicio de recursos y dificulta el acceso a bienes sin costo adicional.

---

## 🎯 Objetivo General
Desarrollar una aplicación que permita a los usuarios el intercambio de productos de manera segura y eficiente, fomentando la reutilización de bienes y la economía circular a través de un entorno digital intuitivo y accesible haciendo uso de tecnologías modernas como Vue.js, Tailwind CSS, .NET, JWT y MySQL.

---

## 📢 Justificación
La creación de esta aplicación responde a la necesidad de contar con una plataforma especializada en el intercambio de productos, promoviendo el consumo responsable y la economía colaborativa. Al facilitar la reutilización de bienes, se reduce el impacto ambiental generado por la producción masiva de nuevos productos y se promueve una cultura de aprovechamiento de recursos disponibles.  
Adicionalmente, la aplicación fomenta la conexión de comunidades de intercambio de productos o bienes, fortaleciendo la colaboración y la interacción entre las personas basadas en la confianza y el beneficio mutuo.

---

## 🏗️ Arquitectura
El desarrollo de la aplicación seguirá una arquitectura en capas basada en **Clean Architecture**, lo que permitirá una mejor separación de responsabilidades, escalabilidad y facilidad de mantenimiento.

### 🔹 Capa de Presentación (Frontend)
- Desarrollada con Vue.js y Tailwind CSS.
- Comunicación con el backend mediante peticiones HTTP a una API REST.
- Manejo de estados con Pinia.

### 🔹 Capa de Aplicación (Backend)
- Implementación en **.NET** con **C#**, siguiendo principios de **Clean Architecture**.
- **Controladores en ASP.NET Web API** para manejar las solicitudes del frontend.
- **Servicios** que encapsulan la lógica de negocio y la comunicación con la capa de datos.
- Uso de **JWT** para autenticación y autorización.

### 🔹 Capa de Datos
- Base de datos **MySQL**, gestionada a través de **Entity Framework Core**.
- Modelado de datos con **entidades relacionales** (usuarios, productos, transacciones de intercambio).

### 🔹 Capa de Infraestructura
- Configuración de migraciones en **MySQL con Entity Framework Core**.

---

## 👥 Integrantes

| Nombre                          | Rol            | GitHub Usuario  | Correo Institucional            |
|--------------------------------|---------------|----------------|--------------------------------|
| Antonio Emmanuel Kantun Cahum  | Desarrollador Backend / Tester | [antoniokantun](https://github.com/antoniokantun)  | 22393267@utcancun.edu.mx  |
| Fernando Gomez Toledo          | Analista / Documentador | [FernandoGT1](https://github.com/FernandoGT1)      | 22393139@utcancun.edu.mx  |
| Jesus Alexander Carrillo Gonzalez | Project Manager / Líder del proyecto | [AlexUT22393235](https://github.com/AlexUT22393235) | 22393235@utcancun.edu.mx  |
| Kenia Sinai Escamilla Cohuo    | Diseñador UX/UI | [Tommoko3ds](https://github.com/Tommoko3ds)              | 22393140@utcancun.edu.mx  |
| Octavio Jesus Cruz Cruz        | Desarrollador Frontend | [MrDorer](https://github.com/MrDorer)        | 22393264@utcancun.edu.mx  |

---

## 📦 Librerías Utilizadas
✅ **Microsoft.AspNetCore** - Framework para construir aplicaciones web y APIs en .NET.  
✅ **Entity Framework Core** - ORM para la gestión y acceso a la base de datos MySQL.  
✅ **JWT (Json Web Token)** - Autenticación y autorización seguras.  
✅ **FluentValidation** - Librería para validaciones de datos en .NET.  
✅ **Serilog** - Registro y manejo de logs para el monitoreo de la aplicación.  

---

## ⚙️ Cómo ejecutar el proyecto
Asegúrate de tener instalado **.NET 8 SDK** y **MySQL** en tu sistema. Luego, sigue estos pasos:

```bash
# Clona el repositorio
git clone https://github.com/MrDorer/backend_strade.git

# Ingresa a la carpeta del proyecto
cd backend_strade

# Aplica las migraciones en MySQL
cd EcoCircular.Infrastructure

# Ejecuta el comando para aplicar migraciones
 dotnet ef database update

# Regresa a la carpeta principal y ejecuta el proyecto
cd ..
dotnet run
```

El backend se ejecutará en `http://localhost:5000` por defecto.

---

## 🤝 Contribución
Si encuentras algún error o problema en la aplicación, te invitamos a reportarlo siguiendo estos pasos:
1. Ve a la sección de **"Issues"** en el repositorio.
2. Crea un nuevo **issue** con una descripción clara del problema encontrado.
3. Si es posible, adjunta capturas de pantalla o pasos para reproducir el error.
4. Nuestro equipo revisará tu reporte y trabajará en la solución.

Agradecemos tu ayuda para mejorar **Strade**. 😊

---

## 📩 Contacto
📧 Para cualquier duda o sugerencia, puedes contactarnos a través de nuestros correos institucionales.

---

## ⚖️ Derechos Reservados
© 2025 Strade. Todos los derechos reservados.
