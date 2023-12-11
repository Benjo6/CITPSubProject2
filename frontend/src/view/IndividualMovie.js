import React, { useState, useEffect } from 'react';
import IndividualMovie1 from "../components/Picture/individual"

export default function IndividualMovie(){
    const [movies, setMovies] = useState([]);
  
    useEffect(() => {
      const fetchMovies = async () => {
        try {
          const response = await fetch('https://localhost:7098/Movies?pageSize=5');
          const moviesData = await response.json();
          const moviesArray = moviesData.movies;
          const fetchPosterPromises = moviesArray.map(async (movie) => {
            const posterResponse = await fetch(`http://www.omdbapi.com/?apikey=b6003d8a&t=${encodeURIComponent(movie.title)}`);
            const posterData = await posterResponse.json();
            return { ...movie, Poster: posterData.Poster };
          });
          const moviesWithPosters = await Promise.all(fetchPosterPromises);
          setMovies(moviesWithPosters);
        } catch (error) {
          console.error('Error fetching data:', error);
        }
      };
      
        fetchMovies();
      }, []);
    return(
    <>
        <IndividualMovie1 {...movies}/>
    </>
    
    )
}