import SessionManager from "../components/Auth/SessionManager";

const MoviesDataService = {
    baseUrl: 'https://localhost:7098/Movies',

    getMovies: async (page = 1, pageSize = 10, filterCriteria = null, sortBy = 'Id', asc = true) => {
        const response = await fetch(`${MoviesDataService.baseUrl}?page=${page}&pageSize=${pageSize}&filterCriteria=${filterCriteria}&sortBy=${sortBy}&asc=${asc}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        const moviesData = await response.json();
        const fetchPosterPromises = moviesData.movies.map(async (movie) => {
            const posterResponse = await fetch(`http://www.omdbapi.com/?apikey=b6003d8a&t=${encodeURIComponent(movie.movie.title)}`);
            const posterData = await posterResponse.json();
            return { ...movie.movie, Poster: posterData.Poster };
        });
        const moviesWithPosters = await Promise.all(fetchPosterPromises);
        return moviesWithPosters;
    },

    getMovie: async (id) => {
        const response = await fetch(`${MoviesDataService.baseUrl}/${id}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    getPopularActorsInMovie: async (movieId) => {
        const response = await fetch(`${MoviesDataService.baseUrl}/PopularActor?movieId=${movieId}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    findSimilarMovies: async (movieId) => {
        const response = await fetch(`${MoviesDataService.baseUrl}/FindSimilarMovies?movieId=${movieId}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },

    putMovie: async (id, movie) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${MoviesDataService.baseUrl}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(movie)
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    },
    
    rate: async (userId, movieId, rating) => {
        const token = SessionManager.getToken();
        const response = await fetch(`${MoviesDataService.baseUrl}/Rate?userId=${userId}&movieId=${movieId}&rating=${rating}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status} message: ${response.message}`);
        }
        return await response.json();
    }    
};

export default MoviesDataService;
