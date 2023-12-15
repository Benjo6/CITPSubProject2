import { createContext, useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from 'react-toastify';

const Contextpage = createContext();
export function MovieProvider({ children }) {

    const [header, setHeader] = useState("Trending");
    const [movies, setMovies] = useState([]);
    const [searchedMovies, setSearchedMovies] = useState([]);
    const [pageSize, setPageSize] = useState(10);
    const [page, setPage] = useState(1);
    const [loader, setLoader] = useState(true);
  
    let token=SessionManager.getToken();
    let userID = SessionManager.getUserId();
    let payload = {
      method: 'GET',
      headers: {   
          "access-control-allow-origin" : "*", 
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + token
          }
      }
  
    useEffect(() => {
      if (page < 1) {
        setPage(1)  // Increment page to 1 if it is less than 1.
      }
    }, [page]);
  
    const APIURL = 'https://localhost:7098'
  
    const getAllMovies = async (filterCriteria, sortBy, asc) => {
      try {
        let query= "";
        filterCriteria.forEach(element => {
          query += `&${element.key}=${element.value}`
        });
        const response = await fetch(`${apiUrl}/Movies?page=${page}&pageSize=${pageSize}&sortBy=${sortBy}&asc=${asc}` + query, payload);
        if (response.ok) {
          const data = await response.json();
          setMovies(data);
          setLoader(false);
          setHeader("Movies");
        } else {
          console.log('Error fetching data');
        }
        setLoader(false);
        setHeader("Movies");
      } catch (error) {
        throw error;
      }
    };
  
    const search = async (query) => {
      const data = await fetch(
        `${APIURL}/search/?query=${query}`,
        payload
      );
      const searchmovies = await data.json();
      setSearchedMovies(searchmovies.results); 
      setLoader(false);
      setHeader(`Results for "${query}"`);
    }
  
  
    const login = async (username, password) => {
      try {
        const result = await fetch(`${APIURL}/Authentication/login`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ username, password }),
        });
        if (result.ok) {
          const token = await result.json();
          const userReq =  await fetch(`${APIURL}/ByUsername/${username}`, {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
            },
          });
          if(!userReq.ok) {
            toast.error("User not found");
            return;
          }
          const user = await userReq.json();
          SessionManager.setUserSession(user.UserName, data.token, user.Id);
          toast.success("Login successfully");
          navigate("/");
        } else {
          const errorData = await result.json();
          toast.error(errorData.message);
        }
      } catch (err) {
        toast.error(err);
        navigate("/")
      }
    }

    const getBookmarks = async () => {
      try {
        const response = await fetch(`${APIURL}/Bookmarks/Movie?userId=${userID}&page=${page}&pageSize=${pageSize}`, payload);
        if (response.ok) {
          const data = await response.json();
          setMovies(data);
          setLoader(false);
          setHeader("Bookmarks");
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const getBookmarkPerson = async () => {
      try {
        const response = await fetch(`${APIURL}/Bookmarks/Person?userId=${userID}&page=${page}&pageSize=${pageSize}`, payload);
        if (response.ok) {
          const data = await response.json();
          setMovies(data);
          setLoader(false);
          setHeader(data.name);
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const createBookmarkMovie = async (aliasId) => {
      try {
        const response = await fetch(`${APIURL}/Bookmarks/Movie`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ UserId: userID, MovieId: aliasId }),
        });
        if (response.ok) {
          const data = await response.json();
          toast.success("Bookmark added successfully");
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const createBookmarkPerson = async (personId) => {
      try {
        const response = await fetch(`${APIURL}/Bookmarks/Person`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ UserId: userID, PersonId: personId }),
        });
        if (response.ok) {
          const data = await response.json();
          toast.success("Bookmark added successfully");
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const deleteBookmarkMovie = async (aliasId) => {
      try {
        const response = await fetch(`${APIURL}/Bookmarks/Movie?userId=${userID}&aliasId=${aliasId}`, 
        {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
          },
        });
        if (response.ok) {
          const data = await response.json();
          toast.success("Bookmark deleted successfully");
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const deleteBookmarkPerson = async (personId) => {
      try {
        const response = await fetch(`${APIURL}/Bookmarks/Person?userId=${userID}&personId=${personId}`, 
        {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
          },
        });
        if (response.ok) {
          const data = await response.json();
          toast.success("Bookmark deleted successfully");
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const addNote = async (aliasId, note) => {
      try {
        const response = await fetch(`${APIURL}/Bookmarks/Movie?aliasId=${aliasId}&userId=${userID}&note=${note}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
          },
        });
        if (response.ok) {
          const data = await response.json();
          toast.success("Note added successfully");
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const getAlias = async (aliasId) => {
      try {
        const response = await fetch(`${APIURL}/Aliases/${aliasId}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const getEpisodes = async (filterCriteria, sortBy, asc) => {
      try {
        let query= "";
        filterCriteria.forEach(element => {
          query += `&${element.key}=${element.value}`
        });
        const response = await fetch(`${APIURL}/Episodes?page=${page}&pageSize=${pageSize}&sortBy=${sortBy}&asc=${asc}` + query, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const getEpisode = async (episodeId) => {
      try {
        const response = await fetch(`${APIURL}/Episodes/${episodeId}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const getPerson = async (personId) => {
      try {
        const response = await fetch(`${APIURL}/People/${personId}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const findActorsByName = async (name) => {
      try {
        const response = await fetch(`${APIURL}/People/ActorsByName?name=${name}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const findActorsByMovie = async (movieId) => {
      try {
        const response = await fetch(`${APIURL}/People/ActorsByMovie?movieId=${movieId}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const getPopularCoPlayers = async (personId) => {
      try {
        const response = await fetch(`${APIURL}/People/PopularCoPlayers?personId=${personId}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const getSeachHistory =  async (filterCriteria, sortBy, asc) => {
      try {
        let query= "";
        filterCriteria.forEach(element => {
          query += `&${element.key}=${element.value}`
        });
        const response = await fetch(`${APIURL}/History?page=${page}&pageSize=${pageSize}&sortBy=${sortBy}&asc=${asc}` + query, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      }
      catch (error) {
        throw error;
      }
    }

    const getOneSearchHistory =  async (id) => {
      try {
        const response = await fetch(`${APIURL}/History/${id}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      }
      catch (error) {
        throw error;
      }
    }

    const WordToWordsQuery = async (stringArray) => {
      try {
        let query= "";
        stringArray.forEach(element => {
          query += `keywords=${element.value}&`;
        });
        query = query.substring(0, query.length - 1);
        const response = await fetch(`${APIURL}/Search/WordToWords?${query}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      }
      catch (error) {
        throw error;
      }
    }

    const BestMatchQuery = async (stringArray) => {
      try {
        let query= "";
        stringArray.forEach(element => {
          query += `keywords=${element.value}&`;
        });
        query = query.substring(0, query.length - 1);
        const response = await fetch(`${APIURL}/Search/BestMatchQuery?${query}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      }
      catch (error) {
        throw error;
      }
    }

    const exactMatchQuery = async (stringArray) => {
      try {
        let query= "";
        stringArray.forEach(element => {
          query += `keywords=${element.value}&`;
        });
        query = query.substring(0, query.length - 1);
        const response = await fetch(`${APIURL}/Search/ExactMatch?${query}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      }
      catch (error) {
        throw error;
      }
    }

    const personWords = async (word, frequency) => {
      try {
        const response = await fetch(`${APIURL}/Search/PersonWords?word=${word}&frequency=${frequency}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
          return [];
        }
      }
      catch (error) {
        throw error;
      }
    }

    const searchByString = async(searchString, resultCount) => {
      try {
        const response = await fetch(`${APIURL}/Search?searchString=${searchString}&resultCount=${resultCount}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
          return [];
        }
      }
      catch (error) {
        throw error;
      }
    }

    const loggedInSearch = async(searchString, resultCount) => {
      try {
        const response = await fetch(`${APIURL}/Search/LoggedIn?searchString=${searchString}&resultCount=${resultCount}&userId=${userID}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
          return [];
        }
      }
      catch (error) {
        throw error;
      }
    }

    const structuredSearch = async(title, personName, resultCount) => {
      try {
        const response = await fetch(`${APIURL}/Search/Structured?title=${title}&personName=${personName}&resultCount=${resultCount}}&userId=${userID}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
          return [];
        }
      }
      catch (error) {
        throw error;
      }
    }

    const getUsers =  async (filterCriteria, sortBy, asc) => {
      try {
        let query= "";
        filterCriteria.forEach(element => {
          query += `&${element.key}=${element.value}`
        });
        const response = await fetch(`${APIURL}/Users?page=${page}&pageSize=${pageSize}&sortBy=${sortBy}&asc=${asc}` + query, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
        }
      }
      catch (error) {
        throw error;
      }
    }

    const getUser =  async (id) => {
      try {
        const response = await fetch(`${APIURL}/Users/${id}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
          return [];
        }
      }
      catch (error) {
        throw error;
      }
    }

    const modifyUser = async (id, username, password, email, registrationDate) => {
      try {
        const response = await fetch(`${APIURL}/Users/${id}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
          },
          body: JSON.stringify({id, password, email, registrationDate }),
        });
        if (response.ok) {
          const data = await response.json();
          toast.success("User created successfully");
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const deleteUser = async (id) => {
      try {
        const response = await fetch(`${APIURL}/Users/${id}`, {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
          },
        });
        if (response.ok) {
          const data = await response.json();
          toast.success("User deleted successfully");
        } else {
          console.log('Error fetching data');
        }
      } catch (error) {
        throw error;
      }
    }

    const getUserByUsername =  async (username) => {
      try {
        const response = await fetch(`${APIURL}/ByUsername/${username}`, payload);
        if (response.ok) {
          const data = await response.json();
          return data;
        } else {
          console.log('Error fetching data');
          return [];
        }
      }
      catch (error) {
        throw error;
      }
    }

    return (
      <Contextpage.Provider
        value={{
          header,
          setHeader,
          movies,
          setMovies,
          pageSize,
          setPageSize,
          loader,
          setLoader,
          getAllMovies,
          search,
          searchedMovies,
          setSearchedMovies,
          login,
          getBookmarks,
          getBookmarkPerson,
          createBookmarkMovie,
          createBookmarkPerson,
          deleteBookmarkMovie,
          deleteBookmarkPerson,
          addNote,
          getAlias,
          getEpisodes,
          getEpisode,
          getPerson,
          findActorsByName,
          findActorsByMovie,
          getPopularCoPlayers,
          getSeachHistory,
          getOneSearchHistory,
          WordToWordsQuery,
          BestMatchQuery,
          exactMatchQuery,
          personWords,
          searchByString,
          loggedInSearch,
          structuredSearch,
          getUsers,
          getUser,
          modifyUser,
          deleteUser,
          getUserByUsername,
        }}
      >
        {children}
      </Contextpage.Provider>
    );
  
  }
  
  export default Contextpage