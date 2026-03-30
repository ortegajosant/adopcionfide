document.addEventListener('DOMContentLoaded', async function () {
    const container = document.getElementById('detalle-container');
    const loading = document.getElementById('detalle-loading');
    const cedula = container.dataset.cedula;

    try {
        const persona = await personaApi.obtenerPorCedula(cedula);

        loading.classList.add('d-none');

        if (!persona) {
            container.innerHTML = `
                <div class="alert alert-warning">Persona no encontrada.</div>
                <a href="/persona-frontend" class="btn btn-secondary">Volver al listado</a>`;
            return;
        }

        document.getElementById('persona-nombre').textContent = persona.nombre;
        document.getElementById('persona-cedula').textContent = persona.cedula;
        document.getElementById('persona-edad').textContent = persona.edad;
        document.getElementById('persona-email').textContent = persona.email || 'No registrado';
        document.getElementById('detalle-card').classList.remove('d-none');

    } catch (error) {
        loading.classList.add('d-none');
        container.innerHTML = `
        <div class="alert alert-danger">Error al cargar el detalle.</div>
        <a href="/persona-frontend" class="btn btn-secondary">Volver al listado</a>`;
    }
});
