import React from 'react';
import Card from 'react-bootstrap/Card';
import {Link} from 'react-router-dom';
import { AiFillStar, AiOutlineStar} from 'react-icons/ai';
import styles from './card.module.css';
import  { useState, useEffect } from 'react';
import SessionManager from '../Auth/SessionManager';
import UsersDataService from "../../dataservices/UsersDataService";
import BookmarksDataService from "../../dataservices/BookmarksDataService";

function BasicExample(props) {
  const placeholderImage = 'https://via.placeholder.com/400';
  const username = SessionManager.getUserName();
  const [isBookmarked, setIsBookmarked] = useState(false);
  const [user, setUser] = useState([])
  
  useEffect(() => {
    if(username){
        UsersDataService.getUserByUsername(username)
            .then(response => setUser(response.user))
            .catch(error => console.error(error));
    }
  }, [username])
  
  const BookmarkMovie = async () => {
      if (isBookmarked === false){
        const response = await BookmarksDataService.createBMMovie(user.id, props.id);
        if (response.ok){setIsBookmarked(true)}
      }else{
        const response = await BookmarksDataService.deleteBookmarkMovie(user.id, props.id);
        if (response.ok){setIsBookmarked(false)}
      }
  }
  

  return (
    <div className='m-auto'>
      
      <Card style={{ width: '15rem', height: '20rem', background: '#050505'}} key={props.Id} className='m-3'>
      <button className={styles.bookmark} onClick={BookmarkMovie}>{isBookmarked ? <AiFillStar /> : <AiOutlineStar/>}</button>
      <Link to={`/movie/${props.id}`} className='h-full w-full position-absolute'>
      <Card.Img variant="top" src={props.Poster !== "N/A" ? props.Poster : placeholderImage} alt={props.title} style={{ width: '15rem' , height: '20rem'}} className='img object-cover' />
      <div className={styles.playground}>
      <h1 className={styles.title}>{props.title || props.name}</h1>

                {(props.rating||0) > 7 ? <h1 className={styles.bigger7}>{(props.rating||0).toFixed(1)}</h1> : (props.rating||0) > 5.5 ? <h1 className={styles.bigger55}>{(props.rating||0).toFixed(1)}</h1> : <h1 className={styles.bigger1}>{(props.rating||0).toFixed(1)}</h1>}
      </div>
      </Link>
    </Card>
    </div>
    
  );
}

export default BasicExample;
