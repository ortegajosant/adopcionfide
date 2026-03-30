document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('crear-persona-form');
    const alertContainer = document.getElementById('alert-container');

    form.addEventListener('submit', async function (e) {
        e.preventDefault();
        limpiarErrores();

        const persona = {
            cedula: document.getElementById('Cedula').value.trim(),
            nombre: document.getElementById('Nombre').value.trim(),
            edad: parseInt(document.getElementById('Edad').value) || 0
        };

        const result = await personaApi.crear(persona);

        if (result.success) {
            window.location.href = '/persona-frontend';
            return;
        }

        if (result.status === 409) {
            mostrarErrorCampo('Cedula', result.data.message);
        } else if (result.status === 400 && result.data.errors) {
            result.data.errors.forEach(error => mostrarAlerta(error));
        } else {
            mostrarAlerta(result.data.message || 'Error inesperado.');
        }
    });

    function mostrarErrorCampo(campo, mensaje) {
        const input = document.getElementById(campo);
        const errorSpan = document.querySelector(`[data-valmsg-for="${campo}"]`);
        if (input) input.classList.add('is-invalid');
        if (errorSpan) errorSpan.textContent = mensaje;
    }

    function mostrarAlerta(mensaje) {
        alertContainer.innerHTML = `
            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                ${mensaje}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>`;
    }

    function limpiarErrores() {
        alertContainer.innerHTML = '';
        document.querySelectorAll('.is-invalid').forEach(el => el.classList.remove('is-invalid'));
        document.querySelectorAll('[data-valmsg-for]').forEach(el => el.textContent = '');
    }
});
