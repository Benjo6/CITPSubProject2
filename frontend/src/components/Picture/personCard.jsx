import React from 'react';
import Card from 'react-bootstrap/Card';
import {Link} from 'react-router-dom';
import { AiFillStar, AiOutlineStar} from 'react-icons/ai';
import styles from './card.module.css';
import  { useState, useEffect } from 'react';
import SessionManager from '../Auth/SessionManager';


function SearchCard(props) {
  const placeholderImage = 'https://via.placeholder.com/400';
  const username = SessionManager.getUserName();
  const [isBookmarked, setIsBookmarked] = useState(false);
  const [user, setUser] = useState([])
  
  useEffect(() => {
    if(username){
      fetch(`https://localhost:7098/Users/ByUsername/${username}`)
      .then(response => response.json())
      .then(json => setUser(json))
    }
  }, [])
  
  const BookmarkMovie = async () => {
      if (isBookmarked === false){
        const response = await fetch(`https://localhost:7098/Bookmarks/Movie?userId=${user.id}&movieId=${props.id}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
      });
        if (response.ok){setIsBookmarked(true)}
      }
  }
  

  return (
    <div className='m-auto'>
      
      <Card style={{ width: '15rem', height: '20rem', background: '#050505'}} key={props.Id} className='m-3'>
      <button className={styles.bookmark} onClick={BookmarkMovie}>{isBookmarked ? <AiFillStar /> : <AiOutlineStar/>}</button>
      <Link to={`/movie/${props.id}`} className='h-full w-full position-absolute'>
      <div className={styles.playground}>
      <h1 className={styles.title}>{props.title}</h1>

                {(props.rating||0) > 7 ? <h1 className={styles.bigger7}>{(props.rating||0).toFixed(1)}</h1> : (props.rating||0) > 5.5 ? <h1 className={styles.bigger55}>{(props.rating||0).toFixed(1)}</h1> : <h1 className={styles.bigger1}>{(props.rating||0).toFixed(1)}</h1>}
      </div>
      </Link>
    </Card>
    </div>
    
  );
}

export default SearchCard;