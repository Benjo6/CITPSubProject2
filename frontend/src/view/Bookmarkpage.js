import { useEffect, useState } from "react";
import BookmarksDataService from "../dataservices/BookmarksDataService";
import SessionManager from "../components/Auth/SessionManager";
import UsersDataService from "../dataservices/UsersDataService";
import BasicExample from "../components/Picture/card";

function Bookmarkpage(){
    SessionManager.getToken()
    const [bookmark, setbookmark] = useState([]);
    const usn = SessionManager.getUserName();
    const userID = UsersDataService.getUserByUsername(usn)
    const fetchBookmarks = async () => {
        const bookmarkedMovies = await BookmarksDataService.getMovies(userID, 1, 12)
        setbookmark(bookmarkedMovies);
    }
    useEffect(()=>{
        fetchBookmarks();
    }, [fetchBookmarks])

    console.log(SessionManager.getToken())
    return(
    <>
        <div className='d-flex flex-wrap'>
          {bookmark.map((movie) => {
            return <BasicExample key={movie.id} {...movie} className='m-auto' />
          })} 
        </div>
        
    </>
);
}

export default Bookmarkpage;