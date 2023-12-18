import SessionManager from "../components/Auth/SessionManager";

const SearchDataService = {
    baseUrl: 'https://localhost:7098/Search',

    getSearchHistories: async (page = 1, pageSize = 10, conditions = null, sortBy = 'Id', asc = true) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${SearchDataService.baseUrl}/History?page=${page}&pageSize=${pageSize}&conditions=${conditions}&sortBy=${sortBy}&asc=${asc}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },
    
    getOneSearchHistory: async (id) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${SearchDataService.baseUrl}/History/${id}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    wordToWordsQuery: async (keywords) => {
        const response = await fetch(`${SearchDataService.baseUrl}/WordToWords?keywords=${keywords.join('&keywords=')}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    bestMatchQuery: async (keywords) => {
        const response = await fetch(`${SearchDataService.baseUrl}/BestMatchQuery?keywords=${keywords.join('&keywords=')}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    exactMatchQuery: async (keywords) => {
        const response = await fetch(`${SearchDataService.baseUrl}/ExactMatch?keywords=${keywords.join('&keywords=')}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    personWords: async (word, frequency) => {
        const response = await fetch(`${SearchDataService.baseUrl}/ActorWords?word=${word}&frequency=${frequency}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    movieSearch: async (searchString, resultCount = 10) => {
        const response = await fetch(`${SearchDataService.baseUrl}/Movie?searchString=${searchString}&resultCount=${resultCount}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    loggedInMovieSearch: async (userId, searchString, resultCount = 10) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${SearchDataService.baseUrl}/Movie/LoggedIn?userId=${userId}&searchString=${searchString}&resultCount=${resultCount}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    personSearch: async (searchString, resultCount = 10) => {
        const response = await fetch(`${SearchDataService.baseUrl}/Person?searchString=${searchString}&resultCount=${resultCount}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    loggedInPersonSearch: async (userId, searchString, resultCount = 10) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${SearchDataService.baseUrl}/Person/LoggedIn?userId=${userId}&searchString=${searchString}&resultCount=${resultCount}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },
    structuredSearch: async (userId, title, personName, resultCount = 10) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${SearchDataService.baseUrl}/Structured?userId=${userId}&title=${title}&personName=${personName}&resultCount=${resultCount}`, {
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

export default SearchDataService;
