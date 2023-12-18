import React, { useState, useEffect } from 'react';
import Container from 'react-bootstrap/esm/Container';
import BasicExample from '../components/Picture/card';
import { Button, Form } from 'react-bootstrap';
import MoviesDataService from '../dataservices/MoviesDataService';

function MoviesPage() {
  const pageSize = 12;
  const [movies, setMovies] = useState([]);
  const [page, setPage] = useState(1);
  const [isLoading, setIsLoading] = React.useState(false);
  const [sortBy, setSortBy] = useState('Id');
  const [asc, setAsc] = useState(true);
  const [filterCriteria, setFilterCriteria] = useState({});

  const fetchMovies = React.useCallback(async () => {
    try {
      setIsLoading(true);
      const movie = await MoviesDataService.getMovies(page, pageSize, filterCriteria, sortBy, asc);
      console.log(movie.listUri);
      const moviesData = movie.movies;
      setIsLoading(false);
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
  }, [page, sortBy, asc, filterCriteria]);

  useEffect(() => {  
    fetchMovies();
  }, [fetchMovies]);

  const handleFilterChange = (key, value) => {
    setFilterCriteria(prevState => ({ ...prevState, [key]: value }));
  };

  return (
    <>
      <Container className='m-auto'>
        <Form>
          <Form.Group controlId="sortBy">
            <Form.Label>Sort By</Form.Label>
            <Form.Control as="select" value={sortBy} onChange={(e) => setSortBy(e.target.value)}>
              <option value="Id">Id</option>
              <option value="Title">Title</option>
            </Form.Control>
          </Form.Group>
          <Form.Group controlId="asc">
            <Form.Check type="checkbox" label="Ascending" checked={asc} onChange={(e) => setAsc(e.target.checked)} />
          </Form.Group>
          <Form.Group controlId="filterCriteria">
            <Form.Label>Filter Criteria</Form.Label>
            <Form.Control type="text" placeholder="Enter filter criteria" onChange={(e) => handleFilterChange(sortBy, e.target.value)} />
          </Form.Group>
        </Form>
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
