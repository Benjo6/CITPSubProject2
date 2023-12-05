import Card from 'react-bootstrap/Card';


function BasicExample(props){
  return (
    <Card style={{ width: '15rem', margin:'0.5rem'}} key={props.id}>
      <Card.Img variant="top" src={props.img} />
      <Card.Body>
        <Card.Title>{props.name}</Card.Title>
        <Card.Text>Duration:{props.duration}</Card.Text>
        <Card.Text>Genre:{props.genre}</Card.Text>
      </Card.Body>
    </Card>
  );
}

export default BasicExample;