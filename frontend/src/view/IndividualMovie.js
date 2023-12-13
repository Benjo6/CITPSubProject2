import React, { useState, useEffect } from 'react';
import { Image, Col, Container, Row } from "react-bootstrap";
import styles from "./viewCss/individualMovie.module.css";


export default function IndividualMovie(){
  const placeholderImage = 'https://via.placeholder.com/400';
const fetchPosterAndPlot = async (title) => {
  const response = await fetch(`http://www.omdbapi.com/?apikey=b6003d8a&t=${encodeURIComponent(title)}`);
  const data = await response.json();
  return { Poster: data.Poster, Plot: data.Plot };
};

const id = 'tt0088634';
const [movie, setMovie] = useState([]);
useEffect(() => {
const fetchMovie =async () => {
  try {
    const response = await fetch(`https://localhost:7098/Movies/${id}`);
    const moviesData = await response.json();
    const posterAndPlot = await fetchPosterAndPlot(moviesData.originalTitle);
    setMovie({ ...moviesData, ...posterAndPlot });
  } catch (error) {
    console.error('Error fetching data:', error);
  }
};


  fetchMovie();
});
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
                        <p className='ms-4'>Episodes: {movie.episodesCount}</p>
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
                </div>
            </Col>
            </Row>
        </Container>
    </>
    
    )
}