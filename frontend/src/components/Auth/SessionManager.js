const SessionManager = {

    getToken() {
        const token = sessionStorage.getItem('token');
        if (token) return token;
        else return null;
    },

    setUserSession(userName, token) {
        sessionStorage.setItem('userName', userName);
        sessionStorage.setItem('token', token);
    },

    removeUserSession(){
        sessionStorage.removeItem('userName');
        sessionStorage.removeItem('token');
    }
}

export default SessionManager;