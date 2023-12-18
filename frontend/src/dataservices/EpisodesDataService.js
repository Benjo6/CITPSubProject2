import SessionManager from "../components/Auth/SessionManager";

const EpisodesDataService = {
    baseUrl: 'https://localhost:7098/Episodes',

    getEpisodes: async (page = 1, pageSize = 10, conditions = null, sortBy = 'Id', asc = true) => {
        const response = await fetch(`${EpisodesDataService.baseUrl}?page=${page}&pageSize=${pageSize}&conditions=${conditions}&sortBy=${sortBy}&asc=${asc}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    getEpisode: async (id) => {
        const response = await fetch(`${EpisodesDataService.baseUrl}/${id}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    putEpisode: async (id, episode) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${EpisodesDataService.baseUrl}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(episode)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },
    
    postEpisode: async (episode) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${EpisodesDataService.baseUrl}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(episode)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },
    
    deleteEpisode: async (id) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${EpisodesDataService.baseUrl}/${id}`, {
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

export default EpisodesDataService;
