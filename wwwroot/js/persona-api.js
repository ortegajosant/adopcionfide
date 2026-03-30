const personaApi = {
    baseUrl: '/api/personas',

    obtenerTodas: async function () {
        const response = await fetch(this.baseUrl);

        if (!response.ok)
            throw new Error('Error al obtener personas');

        return await response.json();
    },

    obtenerPorCedula: async function (cedula) {
        const response = await fetch(`${this.baseUrl}/${cedula}`);

        if (response.status === 404)
            return null;

        if (!response.ok)
            throw new Error('Error al obtener persona');

        return await response.json();
    },

    crear: async function (persona) {
        const response = await fetch(this.baseUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(persona)
        });

        const data = await response.json();

        if (!response.ok)
            return { success: false, status: response.status, data };

        return { success: true, data };
    }
};
