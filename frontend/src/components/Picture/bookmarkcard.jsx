import React from 'react';
import Card from 'react-bootstrap/Card';
import {Link} from 'react-router-dom';
import styles from './card.module.css';


function BookmarkCard(props) {
  const placeholderImage = 'https://via.placeholder.com/400';

  return (
    <div className='m-auto'>
      
      <Card style={{ width: '15rem', height: '20rem', background: '#050505'}} key={props.movieId} className='m-3'>
      <Link to={`/movie/${props.movieId}`} className='h-full w-full position-absolute'>
      <Card.Img variant="top" src={props.Poster !== "N/A" ? props.Poster : placeholderImage} alt={props.movieTitle} style={{ width: '15rem' , height: '20rem'}} className='img object-cover' />
      <div className={styles.playground}>
      <h1 className={styles.title}>{props.movieTitle}</h1>

                {(props.movieRating||0) > 7 ? <h1 className={styles.bigger7}>{(props.movieRating||0)}</h1> : (props.movieRating||0) > 5.5 ? <h1 className={styles.bigger55}>{(props.movieRating||0)}</h1> : <h1 className={styles.bigger1}>{(props.movieRating||0)}</h1>}
      </div>
      </Link>
    </Card>
    </div>
    
  );
}

export default BookmarkCard;
