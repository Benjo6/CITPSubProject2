const SessionManager = {

    getApiKey() {
        return 'ForTheLoveOfGodSaveKeySecurely';
    },

    getToken() {
        const token = sessionStorage.getItem('token');
        
        // Check if token is not null or undefined
        if (!token) {
            console.log("Token is undefined");
            return null;
        }
    
        const isExpired = this.isTokenExpired(token);
        
        if (isExpired) {
            console.log("Token is expired");
            return null;
        }
    
        return token;
    },
    
    isTokenExpired(token) {
        // Parse the JWT string and decode it to a JSON object
        let tokenPayload = JSON.parse(atob(token.split('.')[1]));
    
        // Get the current time
        let dateNow = new Date();
    
        // jwt 'exp' is in seconds
        if (tokenPayload.exp < dateNow.getTime() / 1000) {
            return true;
        } else {
            return false;
        }
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