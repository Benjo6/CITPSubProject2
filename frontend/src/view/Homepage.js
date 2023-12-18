import React, { useState, useEffect } from 'react';
import Container from 'react-bootstrap/esm/Container';
import ControlledCarousel from '../components/carousel/carousel';
import SessionManager from "../components/Auth/SessionManager";
import BasicExample from '../components/Picture/card';
import MoviesDataService from '../dataservices/MoviesDataService';

export default function HomePage() {
    const [movies, setMovies] = useState([]);
  
    useEffect(() => {
      const fetchMovies = async () => {
        try {
          let token=SessionManager.getToken();
    let payload = {
      method: 'GET',
      headers: {   
          "access-control-allow-origin" : "*", 
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + token
      }
  }
  let conditions = { "Votes": ">1000" };
  const movie = await MoviesDataService.getMovies(1,4, conditions,"Id",false);
  console.log(movie.listUri);
  const moviesData = movie.movies;
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