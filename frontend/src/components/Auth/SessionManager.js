const SessionManager = {

    getToken() {
        const token = sessionStorage.getItem('token');
        if (token) return token;
        else return null;
    },

    setUserSession(userName, token, id) {
        sessionStorage.setItem('username', userName);
        sessionStorage.setItem('token', token);
    },

    removeUserSession(){
        sessionStorage.clear();
    }
}

export default SessionManager;