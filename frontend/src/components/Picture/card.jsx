import React from 'react';
import Card from 'react-bootstrap/Card';
import {Link} from 'react-router-dom';
import { AiFillStar, AiOutlineStar} from 'react-icons/ai';
import styles from './card.module.css';
import  { useState } from 'react';
import { toast } from 'react-toastify';
import SessionManager from '../Auth/SessionManager';


function BasicExample(props) {
  const placeholderImage = 'https://via.placeholder.com/400';
  const [userId, setUserId] = useState([]);
  const [isBookmarked, setIsBookmarked] = useState(false);
  //const user = localStorage.getItem(currentUser);

  //if (SessionManager.getToken() === true) {
  //  const userResponse = fetch(`https://localhost:7098/Users/ByUsername/${user}`)
  //      const user = userResponse.json();
 // }
  
  const BookmarkMovie =() => {
    if (!userId) {toast.info("To bookmark this movie, please log in.");}
    else {
      if (isBookmarked === true){
        setIsBookmarked(false);
      }else { setIsBookmarked(true)}
    }
  }
  

  return (
    <div className='m-auto'>
      
      <Card style={{ width: '15rem', height: '20rem', background: '#050505'}} key={props.Id} className='m-3'><Link to={`/movie/${props.id}`} className='h-full w-full position-absolute'>
      <button className={styles.bookmark}><AiOutlineStar/></button>
      <Card.Img variant="top" src={props.Poster !== "N/A" ? props.Poster : placeholderImage} alt={props.title} style={{ width: '15rem' , height: '20rem'}} className='img object-cover' />
      <div className={styles.playground}>
      <h1 className={styles.title}>{props.title}</h1>

                {(props.rating||0) > 7 ? <h1 className={styles.bigger7}>{(props.rating||0).toFixed(1)}</h1> : (props.rating||0) > 5.5 ? <h1 className={styles.bigger55}>{(props.rating||0).toFixed(1)}</h1> : <h1 className={styles.bigger1}>{(props.rating||0).toFixed(1)}</h1>}
      </div>
      </Link>
    </Card>
    </div>
    
  );
}

export default BasicExample;
