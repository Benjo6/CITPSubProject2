const SessionManager = {

    getToken() {
        const token = sessionStorage.getItem('token');
        if (token) return token;
        else return null;
    },

    getUserId() {
        const id = sessionStorage.getItem('id');
        if (id) return id;
        else return null;
    },

    getUserName() {
        const userName = sessionStorage.getItem('username');
        if (userName) return userName;
        else return null;
    },

    setUserSession(userName, token, id) {
        sessionStorage.setItem('username', userName);
        sessionStorage.setItem('token', token);
        sessionStorage.setItem('id', id);
    },

    removeUserSession(){
        sessionStorage.clear();
    }
}

export default SessionManager;