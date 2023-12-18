import SessionManager from "../components/Auth/SessionManager";

const UsersDataService = {
    baseUrl: 'https://localhost:7098/Users',

    getUsers: async (page = 1, pageSize = 10, conditions = null, sortBy = 'Id', asc = true) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${UsersDataService.baseUrl}?page=${page}&pageSize=${pageSize}&conditions=${conditions}&sortBy=${sortBy}&asc=${asc}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    getUser: async (id) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${UsersDataService.baseUrl}/${id}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    getUserByUsername: async (username) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${UsersDataService.baseUrl}/ByUsername/${username}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        const res = response.json();
        
        if (!res.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await res;
    },

    putUser: async (id, user) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${UsersDataService.baseUrl}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(user)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    deleteUser: async (id) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${UsersDataService.baseUrl}/${id}`, {
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

export default UsersDataService;
