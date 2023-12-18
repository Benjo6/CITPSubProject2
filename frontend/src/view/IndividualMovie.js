import React, { useState, useEffect } from 'react';
import { Image, Col, Container, Row } from "react-bootstrap";
import styles from "./viewCss/individualMovie.module.css";
import { useParams } from 'react-router-dom';
import MoviesDataService from '../dataservices/MoviesDataService';
import Rating from '../components/rating/rating';
import { Button } from 'react-bootstrap';
import SessionManager from '../components/Auth/SessionManager';

export default function IndividualMovie(){
  const [rating, setRating] = useState([]);
  const [loggedIn, setLoggedIn] = useState();

    
  const placeholderImage = 'https://via.placeholder.com/400';
  const fetchPosterAndPlot = async (title) => {
    const response = await fetch(`http://www.omdbapi.com/?apikey=b6003d8a&t=${encodeURIComponent(title)}`);
    const data = await response.json();
    return { Poster: data.Poster, Plot: data.Plot };
  };

  const { id } = useParams();
  const [movie, setMovie] = useState([]);
  const useridd="7e5b9b8e-f35c-4801-951a-7c0951965e6f";
  
  useEffect(() => {
    const fetchMovie = async () => {
      try {
        const movie = await MoviesDataService.getMovie(id);
        console.log(movie.uri);
        const moviesData = movie.movie;
        const posterAndPlot = await fetchPosterAndPlot(moviesData.originalTitle);
        setMovie({ ...moviesData, ...posterAndPlot });
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
    setLoggedIn(SessionManager.getToken());
    fetchMovie();
  }, [id]); // Adding id as a dependency

  const Rate = () => {
   MoviesDataService.rate(useridd, movie.id, 8.6);

  }
  console.log(useridd);
  console.log(movie.id);
  console.log(rating)
  return(
    <> 
      <Container >
        <Row>
          <Col sm={6}>
            <div className='text-center'>
              <Image src={movie.Poster !== "N/A" ? movie.Poster : placeholderImage} alt={movie.title} width={224} height={320} rounded/>
            </div> 
          </Col>
          <Col sm={6}>
            <div>
              <span>
                <h1 className="text-center">{movie.originalTitle}</h1>
                <p className='ms-4'>Runtime: {movie.runtime ? `${movie.runtime} minutes` : 'N/A'}</p>
                <p className='ms-4'>Genre: {movie.genres || 'N/A'}</p>
                <p className='ms-4'>Date: {movie.startYear || 'N/A'}</p>
                <p className='ms-4'>Adult: {movie.isAdult ? 'Yes' : 'No'}</p>
                <p className='ms-4'>Episodes: {movie.episodesCount  || 'N/A'}</p>
              </span>
            </div>
          </Col>
        </Row>
      </Container>
      <Container>
        <Row>
          <Col sm={8}>
            <div className={styles.desc}>
              <h5>Description:</h5>
              <p>{movie.Plot}</p>
            </div>
          </Col>
          <Col sm={4}>
            <div className={styles.grad}>
              <h5>Grading system</h5>
              <p>Rating: {movie.rating ? movie.rating : 'N/A'}</p>
              {loggedIn ? <>
              <Rating setVote={setRating}/>
              <Button variant='dark' className='btnhover mt-3'
              onClick={Rate}
          >
            Vote
          </Button> 
              </>: " "}
              
            </div>
          </Col>
        </Row>
      </Container>
    </>
  )
}
