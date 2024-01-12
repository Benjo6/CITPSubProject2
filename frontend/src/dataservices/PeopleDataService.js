import SessionManager from "../components/Auth/SessionManager";

const PeopleDataService = {
    baseUrl: 'https://localhost:7098/People',

    getPeople: async (page = 1, pageSize = 10, conditions = null, sortBy = 'Id', asc = true) => {
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${PeopleDataService.baseUrl}?page=${page}&pageSize=${pageSize}&conditions=${conditions}&sortBy=${sortBy}&asc=${asc}`, {
            headers: {
                'X-Api-Key': apiKey
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    getPerson: async (id) => {
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${PeopleDataService.baseUrl}/${id}`, {
            headers: {
                'X-Api-Key': apiKey
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    findActorsByName: async (name) => {
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${PeopleDataService.baseUrl}/ActorsByName?name=${name}`, {
            headers: {
                'X-Api-Key': apiKey
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    findActorsByMovie: async (movieId) => {
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${PeopleDataService.baseUrl}/ActorsByMovie?movieId=${movieId}`, {
            headers: {
                'X-Api-Key': apiKey
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    getPopularCoPlayers: async (actorName) => {
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${PeopleDataService.baseUrl}/CoPopularActor?actorName=${actorName}`, {
            headers: {
                'X-Api-Key': apiKey
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    putPerson: async (id, person) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${PeopleDataService.baseUrl}/${id}`, {
            method: 'PUT',
            headers: {
                'X-Api-Key': apiKey,
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(person)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },
    
    postPerson: async (person) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${PeopleDataService.baseUrl}`, {
            method: 'POST',
            headers: {
                'X-Api-Key': apiKey,
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(person)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },
    
    deletePerson: async (id) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${PeopleDataService.baseUrl}/${id}`, {
            method: 'DELETE',
            headers: {
                'X-Api-Key': apiKey,
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    }
};

export default PeopleDataService;
