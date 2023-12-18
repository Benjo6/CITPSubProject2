import SessionManager from "../components/Auth/SessionManager";

const AuthenticationDataService = {
    baseUrl: 'https://localhost:7098/Authentication',

    login: async (model) => {
        const response = await fetch(`${AuthenticationDataService.baseUrl}/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(model)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    register: async (model) => {
        const response = await fetch(`${AuthenticationDataService.baseUrl}/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(model)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    makeAdmin: async (username) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${AuthenticationDataService.baseUrl}/makeAdmin`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(username)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    }    
};

export default AuthenticationDataService;
