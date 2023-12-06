import React, { useState, useEffect } from 'react';
import Container from 'react-bootstrap/esm/Container';
import ControlledCarousel from '../components/carousel/carousel';
import BasicExample from '../components/Picture/card';
import { data } from "../utils/card";

export default function HomePage() {
    const [movies, setMovies] = useState([]);
  
    useEffect(() => {
        const fetchMovies = async () => {
          try {
            const response = await fetch('https://localhost:7098/Movies?pageSize=5');
            const moviesData = await response.json();
            const fetchPosterPromises = moviesData.map(async (movie) => {
              const posterResponse = await fetch(`http://www.omdbapi.com/?apikey=b6003d8a&t=${encodeURIComponent(movie.title)}`);
              const posterData = await posterResponse.json();
              return { ...movie, Poster: posterData.Poster };
            });
            const moviesWithPosters = await Promise.all(fetchPosterPromises);
            console.log('Movies with posters:', moviesWithPosters); // Add this line to log the data
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
                <h3>Top Picks</h3>
                <div className='d-flex'>
                {movies.map((movie) => {
                        return <BasicExample {...movie} />;
                    })}
                </div>
            </Container>
            <Container className='my-auto pt-5'>
                <h3>Wishlist</h3>
                <div className='d-flex'>
                {movies.map((movie) => {
                        return <BasicExample {...movie} />;
                    })}
                </div>
            </Container>
        </>
    )
}
