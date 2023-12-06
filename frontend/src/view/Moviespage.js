import React, { useState, useEffect } from 'react';
import Container from 'react-bootstrap/esm/Container';
import BasicExample from '../components/Picture/card';

export default function MoviesPage() {
    const [movies, setMovies] = useState([]);
  
    useEffect(() => {
        const fetchMovies = async () => {
          try {
            const response = await fetch('https://localhost:7098/Movies?pageSize=24');
            const moviesData = await response.json();
            const fetchPosterPromises = moviesData.map(async (movie) => {
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
return (
<>
    <Container className='m-auto'>
        <div className='d-flex flex-wrap'>
           {movies.map((movie) => {
        return <BasicExample {...movie} />;
        })} 
        </div>
        
    </Container>
</>
);
}
