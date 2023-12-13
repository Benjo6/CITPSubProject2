import React from 'react';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import {Link} from 'react-router-dom';
import { AiFillStar, AiOutlineStar} from 'react-icons/ai';
import styles from './card.module.css'

function BasicExample(props) {
  const placeholderImage = 'https://via.placeholder.com/400';
  function truncate(str) {
    return str.length > 20 ? str.substring(0, 16)+ "..." : str;
  }
  return (
    <div className='m-auto'>
      
      <Card style={{ width: '10rem', height: '25rem', background: '#050505'}} key={props.Id} className='m-3'><Link to={`/movie/${props.id}`} className='h-full w-full position-absolute'>
      <button className={styles.bookmark}><AiOutlineStar/></button>
      <Card.Img variant="top" src={props.Poster !== "N/A" ? props.Poster : placeholderImage} alt={props.title} style={{ width: '10rem' , height: '12rem'}} className='img object-cover' />
      <Card.Body>
        <Card.Title><span>{truncate(props.title)}</span></Card.Title>
        <Card.Text><span>Runtime: {props.runtime ? `${props.runtime} min` : 'N/A'}</span></Card.Text>
        <Card.Text><span>Adult: {props.isAdult ? 'Yes' : 'No'}</span></Card.Text>
        <Card.Text><span>Rating: {props.rating ? props.rating : 'N/A'}</span></Card.Text>
        
      </Card.Body></Link>
    </Card>
    </div>
    
  );
}

export default BasicExample;
