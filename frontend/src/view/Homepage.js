import React, { useState, useEffect } from 'react';
import Container from 'react-bootstrap/esm/Container';
import ControlledCarousel from '../components/carousel/carousel';
import BasicExample from '../components/Picture/card';

export default function HomePage() {
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

        return (
        <>
            <Container className='my-auto'>
                <ControlledCarousel></ControlledCarousel>
            </Container>
            <Container className='my-auto pt-5'>
                <h3><span>Top Picks </span></h3>
                <div className='d-flex flex-wrap'>
                {movies.map((movie) => {
                        return <BasicExample {...movie} />;
                    })}
                </div>
            </Container>
            <Container className='my-auto pt-5'>
                <h3><span>Wishlist</span></h3>
                <div className='d-flex flex-wrap'>
                {movies.map((movie) => {
                        return <BasicExample {...movie} />;
                    })}
                </div>
            </Container>
        </>
    )
}
