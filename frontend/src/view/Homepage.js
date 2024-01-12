import React, { useState, useEffect } from 'react';
import Container from 'react-bootstrap/esm/Container';
import BasicExample from '../components/Picture/card';
import MoviesDataService from '../dataservices/MoviesDataService';
import { SearchBar } from "../components/SearchBar/SearchBar"; 



export default function HomePage() {
  const [movies, setMovies] = useState([]);
  const [results, setResults] = useState([]); // Add state for search results
  const [person, setPerson] = useState([]); // Add state for person results

  useEffect(() => {
    const fetchMovies = async () => {
      try {
        const movies = await MoviesDataService.getMovies(1,4, null,"Id",false);
        setMovies(movies);

      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchMovies();
  }, []);

return (
<>
    <Container className='my-auto pt-5'>
        <h3><span>Top Picks </span></h3>
        <div className='d-flex flex-wrap'>
        {movies.map((movie) => {
                return <BasicExample {...movie} />;
            })}
        </div>
    </Container>
</>
)
}