import { FaStar } from 'react-icons/fa';
import React, { useState } from 'react';
import './rating.module.css'


const Rating= ({setVote}) => {
    const [rating, setRating] = useState();

return(
    <>
    <div className='d-flex'>
        {[...Array(10)].map((star, index) => {
            const currentRating = index+1;
           return (
                <FaStar className='star' size={20} color={currentRating<= ( rating) ? "#ffc107" : "#e4e5e"} onClick={()=> {setRating(currentRating)
                setVote(currentRating)}} />
                )
        })} 
    </div>
    </>
    
);
}

export default Rating;