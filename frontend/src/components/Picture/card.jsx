import React from 'react';
import Card from 'react-bootstrap/Card';

function BasicExample(props) {
  const placeholderImage = 'https://via.placeholder.com/400';

  return (
    <Card style={{ width: '14rem', margin: '0.5rem' }} key={props.Id}>
      <Card.Img variant="top" src={props.Poster !== "N/A" ? props.Poster : placeholderImage} alt={props.title} />
      <Card.Body>
        <Card.Title>{props.title}</Card.Title>
        <Card.Text>Type: {props.type}</Card.Text>
        <Card.Text>Runtime: {props.runtime ? `${props.runtime} minutes` : 'N/A'}</Card.Text>
        <Card.Text>Genres: {props.genres || 'N/A'}</Card.Text>
        <Card.Text>End Year: {props.endYear || 'N/A'}</Card.Text>
        <Card.Text>Adult: {props.isAdult ? 'Yes' : 'No'}</Card.Text>
        <Card.Text>Rating: {props.rating ? props.rating : 'N/A'}</Card.Text>
        <Card.Text>Episodes: {props.episodesCount}</Card.Text>
      </Card.Body>
    </Card>
  );
}

export default BasicExample;
