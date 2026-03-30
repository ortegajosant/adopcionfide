// Script para la vista Index de PersonaFrontend.
// Se ejecuta cuando el DOM está listo y carga la tabla de personas desde el API.
// Depende de: persona-api.js (debe cargarse antes).
document.addEventListener('DOMContentLoaded', cargarPersonas);

async function cargarPersonas() {
    // Referencias a los elementos del DOM definidos en Index.cshtml
    const tbody = document.getElementById('personas-tbody');
    const loading = document.getElementById('personas-loading');

    try {
        // Llama al API para obtener todas las personas (GET /api/personas)
        const personas = await personaApi.obtenerTodas();

        // Oculta el spinner de carga una vez que tenemos los datos
        loading.classList.add('d-none');

        // Si no hay personas, muestra un mensaje informativo en la tabla
        if (personas.length === 0) {
            tbody.innerHTML = `
                <tr>
                    <td colspan="4" class="text-center text-muted">No hay personas registradas.</td>
                </tr>`;
            return;
        }

        // Genera las filas de la tabla dinámicamente usando template literals.
        // .map() transforma cada persona en un <tr> con sus datos.
        // .join('') concatena todos los <tr> en un solo string HTML.
        tbody.innerHTML = personas.map(p => `
            <tr>
                <td>${p.cedula}</td>
                <td>${p.nombre}</td>
                <td>${p.edad}</td>
                <td>
                    <button class="btn btn-primary btn-sm btn-detalle" data-cedula="${p.cedula}">
                        Ver detalle
                    </button>
                </td>
            </tr>`).join('');

        // Agrega evento click a cada botón "Ver detalle".
        // Usa data-cedula (atributo HTML data-*) para saber qué persona seleccionó el usuario.
        document.querySelectorAll('.btn-detalle').forEach(btn => {
            btn.addEventListener('click', function () {
                const cedula = this.dataset.cedula;
                window.location.href = `/persona-frontend/detalle/${cedula}`;
            });
        });

    } catch (error) {
        // Si el fetch falla (red, servidor caído, etc.), muestra un mensaje de error
        loading.classList.add('d-none');
        tbody.innerHTML = `
            <tr>
                <td colspan="4" class="text-center text-danger">Error al cargar personas.</td>
            </tr>`;
    }
}
