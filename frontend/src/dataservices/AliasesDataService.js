import SessionManager from "../components/Auth/SessionManager";

const AliasesDataService = {
    baseUrl: 'https://localhost:7098/Aliases',

    getAliases: async (page = 1, pageSize = 10, conditions = null, sortBy = 'Id', asc = true) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${AliasesDataService.baseUrl}?page=${page}&pageSize=${pageSize}&conditions=${conditions}&sortBy=${sortBy}&asc=${asc}`, {
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

    getAlias: async (id) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${AliasesDataService.baseUrl}/${id}`, {
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

    putAlias: async (id, alias) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${AliasesDataService.baseUrl}/${id}`, {
            method: 'PUT',
            headers: {
                'X-Api-Key': apiKey,
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(alias)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },
    
    postAlias: async (alias) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${AliasesDataService.baseUrl}`, {
            method: 'POST',
            headers: {
                'X-Api-Key': apiKey,
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(alias)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },
    
    deleteAlias: async (id) => {
        const token = SessionManager.getToken();
        const apiKey = SessionManager.getApiKey();
        const response = await fetch(`${AliasesDataService.baseUrl}/${id}`, {
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

export default AliasesDataService;
