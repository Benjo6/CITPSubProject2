import React, { useState, useEffect } from 'react';
import Container from 'react-bootstrap/esm/Container';
import BasicExample from '../components/Picture/card';
import { Button, Form, Row, Col } from 'react-bootstrap';
import MoviesDataService from '../dataservices/MoviesDataService';

function MoviesPage() {
  const pageSize = 12;
  const [movies, setMovies] = useState([]);
  const [page, setPage] = useState(1);
  const [isLoading, setIsLoading] = React.useState(false);
  const [sortBy, setSortBy] = useState('Votes'); // Default sort by 'Votes'
  const [asc, setAsc] = useState(false); // Default ascending to false
  const [filterCriteria, setFilterCriteria] = useState({});

  const fetchMovies = React.useCallback(async () => {
    try {
      setIsLoading(true);
      const moviesWithPosters = await MoviesDataService.getMovies(page, pageSize, filterCriteria, sortBy, asc);
      setIsLoading(false);
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
          <Row>
            <Col>
            <Form.Group controlId="sortBy">
            <Form.Label><span>Sort By</span></Form.Label>
            <Form.Control as="select" value={sortBy} onChange={(e) => setSortBy(e.target.value)}>
              <option value="Id">Id</option>
              <option value="Title">Title</option>
              <option value="StartYear">Start Year</option>
              <option value="Runtime">Runtime</option>
              <option value="Genres">Genres</option>
              <option value="Rating">Rating</option>
              <option value="Votes">Votes</option>
            </Form.Control>
          </Form.Group>
          <Form.Group controlId="asc">
            <span><Form.Check type="checkbox" label="Ascending" checked={asc} onChange={(e) => setAsc(e.target.checked)} /></span>
          </Form.Group>
            </Col>
            <Col>
            <Form.Group controlId="filterCriteria">
            <Form.Label><span>Filter Criteria</span></Form.Label>
            <Form.Control type="text" placeholder="Enter filter criteria" onChange={(e) => handleFilterChange(sortBy, e.target.value)} />
          </Form.Group>
            </Col>
          
          </Row>
          
        </Form>
        <div className='d-flex mt-5'>
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
