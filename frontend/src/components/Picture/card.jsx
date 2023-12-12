import React from 'react';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';

function BasicExample(props) {
  const placeholderImage = 'https://via.placeholder.com/400';
  function truncate(str) {
    return str.length > 20 ? str.substring(0, 16)+ "..." : str;
  }
  return (
    <div className='m-auto'>
      <Card style={{ width: '10rem', height: '25rem', background: '#050505'}} key={props.Id} className='m-3'>
      <Card.Img variant="top" src={props.Poster !== "N/A" ? props.Poster : placeholderImage} alt={props.title} style={{ width: '10rem' , height: '12rem'}} />
      <Card.Body>
        <Card.Title><span>{truncate(props.title)}</span></Card.Title>
        <Card.Text><span>Runtime: {props.runtime ? `${props.runtime} min` : 'N/A'}</span></Card.Text>
        <Card.Text><span>Adult: {props.isAdult ? 'Yes' : 'No'}</span></Card.Text>
        <Card.Text><span>Rating: {props.rating ? props.rating : 'N/A'}</span></Card.Text>
        <Button><i class="bi bi-star"></i></Button>
      </Card.Body>
    </Card>
    </div>
    
  );
}

export default BasicExample;
