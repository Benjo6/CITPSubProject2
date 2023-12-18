import { useEffect, useState } from "react";
import BookmarksDataService from "../dataservices/BookmarksDataService";
import SessionManager from "../components/Auth/SessionManager";
import UsersDataService from "../dataservices/UsersDataService";
import BasicExample from "../components/Picture/card";

function Bookmarkpage(){
  const [bookmark, setbookmark] = useState([]);
  const usn = SessionManager.getUserName();

  UsersDataService.getUserByUsername(usn)
      .then(response => {
          const userID = response.user.id;
          fetchBookmarks(userID);
      })
      .catch(error => console.error(error));

  const fetchBookmarks = async (userID) => {
      const bookmarkedMovies = await BookmarksDataService.getMovies(userID, 1, 12)
      const bookies = bookmarkedMovies.result;
      setbookmark(bookies);
  }

  useEffect(()=>{
      if(usn){
          fetchBookmarks();
      }
  }, [usn])


    return(
    <>
        <div className='d-flex flex-wrap'>
          {bookmark.map((movie) => {
            return <BasicExample {...movie} className='m-auto' />
          })} 
        </div>
        
    </>
);
}

export default Bookmarkpage;