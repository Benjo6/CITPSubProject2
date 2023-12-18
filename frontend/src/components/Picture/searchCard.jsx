import React from 'react';
import Card from 'react-bootstrap/Card';
import {Link} from 'react-router-dom';
import styles from './searchCard.module.css';

function SearchCard(props) {

  return (
    <div className='m-auto'>

      <Card style={{ width: '15rem', height: '10rem', background: '#050505'}} key={props.Id}>
      <Card.Body>
      <Link to={`/movie/${props.id}`} className='h-full w-full position-absolute'>
      <div className={styles.playground}>
      <h1 className={styles.title}>{props.title}</h1>

                {(props.rating||0) > 7 ? <h1 className={styles.bigger7}>{(props.rating||0).toFixed(1)}</h1> : (props.rating||0) > 5.5 ? <h1 className={styles.bigger55}>{(props.rating||0).toFixed(1)}</h1> : <h1 className={styles.bigger1}>{(props.rating||0).toFixed(1)}</h1>}
      </div>
      </Link>
      </Card.Body>
    </Card>
    </div>
    
  );
}

export default SearchCard;
