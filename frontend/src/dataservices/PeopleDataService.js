import SessionManager from "../components/Auth/SessionManager";

const PeopleDataService = {
    baseUrl: 'https://localhost:7098/People',

    getPeople: async (page = 1, pageSize = 10, conditions = null, sortBy = 'Id', asc = true) => {
        const response = await fetch(`${PeopleDataService.baseUrl}?page=${page}&pageSize=${pageSize}&conditions=${conditions}&sortBy=${sortBy}&asc=${asc}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    getPerson: async (id) => {
        const response = await fetch(`${PeopleDataService.baseUrl}/${id}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    findActorsByName: async (name) => {
        const response = await fetch(`${PeopleDataService.baseUrl}/ActorsByName?name=${name}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    findActorsByMovie: async (movieId) => {
        const response = await fetch(`${PeopleDataService.baseUrl}/ActorsByMovie?movieId=${movieId}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    getPopularCoPlayers: async (actorName) => {
        const response = await fetch(`${PeopleDataService.baseUrl}/CoPopularActor?actorName=${actorName}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    putPerson: async (id, person) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${PeopleDataService.baseUrl}/${id}`, {
            method: 'PUT',
            headers: {
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
        const response = await fetch(`${PeopleDataService.baseUrl}`, {
            method: 'POST',
            headers: {
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
        const response = await fetch(`${PeopleDataService.baseUrl}/${id}`, {
            method: 'DELETE',
            headers: {
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
