document.addEventListener('DOMContentLoaded', cargarPersonas);

async function cargarPersonas() {
    const tbody = document.getElementById('personas-tbody');
    const loading = document.getElementById('personas-loading');

    try {
        const personas = await personaApi.obtenerTodas();

        loading.classList.add('d-none');

        if (personas.length === 0) {
            tbody.innerHTML = `
                <tr>
                    <td colspan="4" class="text-center text-muted">No hay personas registradas.</td>
                </tr>`;
            return;
        }

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

        document.querySelectorAll('.btn-detalle').forEach(btn => {
            btn.addEventListener('click', function () {
                const cedula = this.dataset.cedula;
                window.location.href = `/persona-frontend/detalle/${cedula}`;
            });
        });

    } catch (error) {
        loading.classList.add('d-none');
        tbody.innerHTML = `
            <tr>
                <td colspan="4" class="text-center text-danger">Error al cargar personas.</td>
            </tr>`;
    }
}
