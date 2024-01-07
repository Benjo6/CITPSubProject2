import { useEffect, useState } from "react";
import BookmarksDataService from "../dataservices/BookmarksDataService";
import SessionManager from "../components/Auth/SessionManager";
import UsersDataService from "../dataservices/UsersDataService";
import BasicExample from "../components/Picture/card";

function Bookmarkpage(){
  const [bookmark, setbookmark] = useState([]);
  const usn = SessionManager.getUserName();

  const fetchBookmarks = async (userID) => {
      console.log(userID);
      const bookmarkedMovies = await BookmarksDataService.getMovies(userID, 1, 12)
      const bookies = bookmarkedMovies.result;
      setbookmark(bookies);
  }

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
        <div className='d-flex flex-wrap'>
          {bookmark.map((movie) => {
            return <BasicExample {...movie} className='m-auto' />
          })} 
        </div>
        
    </>
);
}

export default Bookmarkpage;