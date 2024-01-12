import React, { useEffect, useState } from "react";
import { Button } from 'react-bootstrap';
import BookmarksDataService from "../dataservices/BookmarksDataService";
import SessionManager from "../components/Auth/SessionManager";
import UsersDataService from "../dataservices/UsersDataService";
import BookmarkCard from "../components/Picture/bookmarkcard";

function Bookmarkpage(){
  const pageSize = 12;
  const [page, setPage] = useState(1);
  const [isLoading, setIsLoading] = React.useState(false);
  const [bookmark, setbookmark] = useState([]);
  const usn = SessionManager.getUserName();

  const fetchBookmarks = React.useCallback( async (userID) => {
    try {
      setIsLoading(true);
      const bookmarkedMovies = await BookmarksDataService.getMovies(userID, page, pageSize);
      setIsLoading(false);
      setbookmark(bookmarkedMovies);
    } catch (error) {
      console.error('Error fetching data:', error);
    } 
  }, [page])

  useEffect(()=>{
    if(usn){
        UsersDataService.getUserByUsername(usn)
            .then(response => {
                const userID = response.user.id;
                console.log(userID);
                fetchBookmarks(userID);
            })
            .catch(error => console.error(error));
    }
}, [usn])



    return(
    <>
    <div className='d-flex mt-5'>
          <Button variant='dark' className='btnhover'
            onClick={() => setPage(page - 1)}
            disabled={isLoading || page === 0}
          >
            Previous
          </Button>
          <span className='m-auto'>{page}</span>
          <Button variant='dark' className='btnhover'
            onClick={() => setPage(page + 1)}
            disabled={isLoading || bookmark.length < pageSize}
          >
            Next
          </Button> 
        </div>
        <div className='d-flex flex-wrap'>
          {bookmark.map((movie) => {
            return <BookmarkCard key={movie.movieId} {...movie}/>
          })} 
        </div>
        
    </>
);
}

export default Bookmarkpage;