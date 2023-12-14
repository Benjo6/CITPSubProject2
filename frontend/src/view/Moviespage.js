import React, { useState, useEffect } from 'react';
import Container from 'react-bootstrap/esm/Container';
import BasicExample from '../components/Picture/card';
import SessionManager from "../Auth/SessionManager";
import { Button } from 'react-bootstrap';

function MoviesPage() {
  const pageSize = 12;
  const [movies, setMovies] = useState([]);
  const [page, setPage] = useState(1);
  const [isLoading, setIsLoading] = React.useState(false);
  

      const fetchMovies = React.useCallback(async () => {
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
          setIsLoading(true);
          const response = await fetch(`https://localhost:7098/Movies?page=${page}&pageSize=${pageSize}`, payload);
          const moviesData = await response.json();
          setIsLoading(false);
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
      }, [page]
      );
  
  useEffect(() => {  
      fetchMovies();
    }, [fetchMovies]);


return (
<>
    <Container className='m-auto'>
        <div className='d-flex'>
          
          <Button variant='dark' className='btnhover'
    onClick={() => setPage(page - 1)}
    disabled={isLoading || page === 0}
  >
    Previous
  </Button>
  <span className='m-auto'>{page}</span>
  <Button variant='dark' className='btnhover'
    onClick={() => setPage(page + 1)}
    disabled={isLoading || movies.length < pageSize}
  >
    Next
  </Button> 
        </div>
        <div className='d-flex flex-wrap'>
           {movies.map((movie) => {
        return <BasicExample key={movie.id} {...movie} className='m-auto' />
        })} 
        </div>
    </Container>
</>
);
}

export default MoviesPage;