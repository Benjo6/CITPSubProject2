import SessionManager from "../components/Auth/SessionManager";

const BookmarksDataService = {
    baseUrl: 'https://localhost:7098/Bookmarks',

    getMovies: async (userId, page = 1, pageSize = 10) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        console.log(token);
        const response = await fetch(`${BookmarksDataService.baseUrl}/Movie?userId=${userId}&page=${page}&pageSize=${pageSize}`, {
            headers: {
                'X-Api-Key': apiKey,
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    getPerson: async (userId, page = 1, pageSize = 10) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${BookmarksDataService.baseUrl}/Personality?userId=${userId}&page=${page}&pageSize=${pageSize}`, {
            headers: {
                'X-Api-Key': apiKey,
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    createBMMovie: async (userId, movieId) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${BookmarksDataService.baseUrl}/Movie`, {
            method: 'POST',
            headers: {
                'X-Api-Key': apiKey,
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ userId, movieId })
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    createBMPerson: async (userId, personId) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${BookmarksDataService.baseUrl}/Personality`, {
            method: 'POST',
            headers: {
                'X-Api-Key': apiKey,
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ userId, personId })
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    isPersonBookmarked: async (userId, personId) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${BookmarksDataService.baseUrl}/IsPersonalityBookmarked`, {
            headers: {
                'X-Api-Key': apiKey,
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ userId, personId })
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    isMovieBookmarked: async (userId, movieId) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${BookmarksDataService.baseUrl}/IsMovieBookmarked`, {
            headers: {
                'X-Api-Key': apiKey,
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ userId, movieId })
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    addNote: async (userId, movieId, note) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${BookmarksDataService.baseUrl}/Movie?userId=${userId}&movieId=${movieId}&note=${note}`, {
            method: 'PUT',
            headers: {
                'X-Api-Key': apiKey,
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    deleteBookmarkPersonality: async (userId, personId) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${BookmarksDataService.baseUrl}/Personality`, {
            method: 'DELETE',
            headers: {
                'X-Api-Key': apiKey,
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ userId, personId })
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    deleteBookmarkMovie: async (userId, movieId) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${BookmarksDataService.baseUrl}/Movie`, {
            method: 'DELETE',
            headers: {
                'X-Api-Key': apiKey,
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ userId, movieId })
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    }
};

export default BookmarksDataService;
