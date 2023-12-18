import React from 'react';
import Card from 'react-bootstrap/Card';

function IndCard(props) {

  return (
    <div className='m-auto'>

      <Card style={{ width: '15rem', height: '5rem', background: '#050505', marginTop: '0.5rem'}} key={props.Id}>
      <Card.Title style={{ margin: 'auto'}}><span>{props.name}</span></Card.Title>
    </Card>
    </div>
    
  );
}

export default IndCard;