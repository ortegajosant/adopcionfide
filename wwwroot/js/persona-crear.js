// Script para la vista Crear de PersonaFrontend.
// Intercepta el envío del formulario HTML y lo envía como JSON al API en lugar de hacer un form POST tradicional.
// Depende de: persona-api.js (debe cargarse antes).
document.addEventListener('DOMContentLoaded', function () {
    // Referencias a los elementos del DOM definidos en Crear.cshtml
    const form = document.getElementById('crear-persona-form');
    const alertContainer = document.getElementById('alert-container');

    // Intercepta el evento submit del formulario
    form.addEventListener('submit', async function (e) {
        e.preventDefault(); // Evita el envío tradicional del formulario (POST con recarga de página)
        limpiarErrores();   // Limpia errores anteriores antes de validar de nuevo

        // Construye el objeto persona leyendo los valores de los inputs del formulario.
        // Este objeto se enviará como JSON al API (no como form-data).
        const persona = {
            cedula: document.getElementById('Cedula').value.trim(),
            nombre: document.getElementById('Nombre').value.trim(),
            edad: parseInt(document.getElementById('Edad').value) || 0
        };

        // Envía los datos al API (POST /api/personas con body JSON)
        const result = await personaApi.crear(persona);

        // Si la creación fue exitosa, redirige al listado
        if (result.success) {
            window.location.href = '/persona-frontend';
            return;
        }

        // Manejo de errores según el código HTTP devuelto por el API:
        // 409 Conflict = ya existe una persona con esa cédula
        // 400 Bad Request = errores de validación del modelo
        // Otro = error inesperado
        if (result.status === 409) {
            mostrarErrorCampo('Cedula', result.data.message);
        } else if (result.status === 400 && result.data.errors) {
            result.data.errors.forEach(error => mostrarAlerta(error));
        } else {
            mostrarAlerta(result.data.message || 'Error inesperado.');
        }
    });

    // Muestra un mensaje de error debajo de un campo específico del formulario.
    // Agrega la clase 'is-invalid' de Bootstrap para resaltar el input en rojo.
    function mostrarErrorCampo(campo, mensaje) {
        const input = document.getElementById(campo);
        const errorSpan = document.querySelector(`[data-valmsg-for="${campo}"]`);
        if (input) input.classList.add('is-invalid');
        if (errorSpan) errorSpan.textContent = mensaje;
    }

    // Muestra una alerta general de error usando componente de Bootstrap.
    // Se usa para errores que no están asociados a un campo específico.
    function mostrarAlerta(mensaje) {
        alertContainer.innerHTML = `
            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                ${mensaje}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>`;
    }

    // Limpia todos los errores visuales del formulario:
    // - Vacía el contenedor de alertas generales
    // - Remueve la clase 'is-invalid' de todos los inputs
    // - Limpia el texto de los spans de validación (data-valmsg-for)
    function limpiarErrores() {
        alertContainer.innerHTML = '';
        document.querySelectorAll('.is-invalid').forEach(el => el.classList.remove('is-invalid'));
        document.querySelectorAll('[data-valmsg-for]').forEach(el => el.textContent = '');
    }
});
