// Script para la vista Detalle de PersonaFrontend.
// Carga los datos de una persona específica desde el API y los muestra en una tarjeta.
// Depende de: persona-api.js (debe cargarse antes).
// La cédula se obtiene del atributo data-cedula del HTML, que fue generado por Razor en Detalle.cshtml.
document.addEventListener('DOMContentLoaded', async function () {
    // Referencias a los elementos del DOM definidos en Detalle.cshtml
    const container = document.getElementById('detalle-container');
    const loading = document.getElementById('detalle-loading');

    // Lee la cédula desde el atributo data-cedula del contenedor.
    // Este valor fue inyectado por Razor: data-cedula="@Context.Request.RouteValues["id"]"
    // Es la forma de pasar datos del servidor al JavaScript sin usar variables globales.
    const cedula = container.dataset.cedula;

    try {
        // Llama al API para obtener la persona por cédula (GET /api/personas/{cedula})
        const persona = await personaApi.obtenerPorCedula(cedula);

        // Oculta el spinner de carga
        loading.classList.add('d-none');

        // Si el API retornó null (404), muestra un mensaje de advertencia
        if (!persona) {
            container.innerHTML = `
                <div class="alert alert-warning">Persona no encontrada.</div>
                <a href="/persona-frontend" class="btn btn-secondary">Volver al listado</a>`;
            return;
        }

        // Inyecta los datos de la persona en los elementos de la tarjeta.
        // Usa .textContent (no .innerHTML) para evitar inyección de HTML malicioso (XSS).
        document.getElementById('persona-nombre').textContent = persona.nombre;
        document.getElementById('persona-cedula').textContent = persona.cedula;
        document.getElementById('persona-edad').textContent = persona.edad;
        document.getElementById('persona-email').textContent = persona.email || 'No registrado';

        // Muestra la tarjeta (estaba oculta con la clase d-none de Bootstrap)
        document.getElementById('detalle-card').classList.remove('d-none');

    } catch (error) {
        // Si el fetch falla (red, servidor caído, etc.), muestra un mensaje de error
        loading.classList.add('d-none');
        container.innerHTML = `
        <div class="alert alert-danger">Error al cargar el detalle.</div>
        <a href="/persona-frontend" class="btn btn-secondary">Volver al listado</a>`;
    }
});
