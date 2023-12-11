import React from 'react';
import Card from 'react-bootstrap/Card';

function BasicExample(props) {
  const placeholderImage = 'https://via.placeholder.com/400';
  function truncate(str) {
    return str.length > 20 ? str.substring(0, 18)+ "..." : str;
  }
  return (
    <div className='m-auto'>
      <Card style={{ width: '14rem', height: 'auto'}} key={props.Id}>
      <Card.Img variant="top" src={props.Poster !== "N/A" ? props.Poster : placeholderImage} alt={props.title} style={{ width: '14rem' , height: '20rem'}} />
      <Card.Body>
        <Card.Title>{truncate(props.title)}</Card.Title>
        <Card.Text>Runtime: {props.runtime ? `${props.runtime} minutes` : 'N/A'}</Card.Text>
        <Card.Text>Adult: {props.isAdult ? 'Yes' : 'No'}</Card.Text>
        <Card.Text>Rating: {props.rating ? props.rating : 'N/A'}</Card.Text>
      </Card.Body>
    </Card>
    </div>
    
  );
}

export default BasicExample;
