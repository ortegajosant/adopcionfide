// Módulo que encapsula todas las llamadas HTTP al API de Personas.
// Utiliza la Fetch API nativa del navegador para comunicarse con el backend.
// Cada método es async, lo que permite usar await al consumirlo.
const personaApi = {

    // URL base del API REST de personas (definido en PersonaApiController.cs)
    baseUrl: '/api/personas',

    // Obtiene la lista completa de personas desde el API.
    // Retorna: un array de objetos { cedula, nombre, edad, email }
    // Lanza un error si la respuesta no es exitosa (status != 2xx).
    obtenerTodas: async function () {
        const response = await fetch(this.baseUrl); // GET /api/personas

        if (!response.ok)
            throw new Error('Error al obtener personas');

        return await response.json(); // Convierte la respuesta JSON a un objeto JavaScript
    },

    // Obtiene una persona específica por su cédula.
    // Parámetro: cedula (string) - la cédula de la persona a buscar.
    // Retorna: un objeto { cedula, nombre, edad, email } o null si no existe (404).
    obtenerPorCedula: async function (cedula) {
        const response = await fetch(`${this.baseUrl}/${cedula}`); // GET /api/personas/{cedula}

        if (response.status === 404)
            return null; // Si el API responde 404, retornamos null en lugar de lanzar error

        if (!response.ok)
            throw new Error('Error al obtener persona');

        return await response.json();
    },

    // Crea una nueva persona enviando los datos en formato JSON.
    // Parámetro: persona (object) - objeto con { cedula, nombre, edad }.
    // Retorna: { success: true, data } si se creó correctamente,
    //          { success: false, status, data } si hubo un error (409 = duplicado, 400 = validación).
    crear: async function (persona) {
        const response = await fetch(this.baseUrl, { // POST /api/personas
            method: 'POST',
            headers: { 'Content-Type': 'application/json' }, // Indica al servidor que el cuerpo es JSON
            body: JSON.stringify(persona) // Convierte el objeto JavaScript a string JSON
        });

        const data = await response.json(); // Leemos la respuesta del servidor (éxito o error)

        // Retornamos un objeto uniforme que indica si fue exitoso o no,
        // para que el código que consume este método pueda manejar ambos casos fácilmente.
        if (!response.ok)
            return { success: false, status: response.status, data };

        return { success: true, data };
    }
};
