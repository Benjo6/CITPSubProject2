import { useState } from "react";
import { FaSearch } from "react-icons/fa";
import "../SearchBar/SearchBar.css";

export const SearchBar = ({ setResults }) => {
  const [input, setInput] = useState("");

  const fetchData = async (value) => {
    const response = await fetch(`https://localhost:7098/Search/Movie?searchString=${value}&resultCount=5`);
    const movieSearch = await response.json();
    const fetchPosterPromises = movieSearch.map(async (movie) => {
      const posterResponse = await fetch(`http://www.omdbapi.com/?apikey=b6003d8a&t=${encodeURIComponent(movie.title)}`);
      const posterData = await posterResponse.json();
      return { ...movie, Poster: posterData.Poster };
    });
    const moviesWithPosters =  Promise.all(fetchPosterPromises);
    setResults(moviesWithPosters);
  }
  const fetchPerson = (person) => {
    fetch(`https://localhost:7098/Search/Person?searchString=${person}&resultCount=5`)
      .then((response) => response.json())
      .then((json) => {
        const res = json;
        setResults(res);
      });
  };

  const handleChange = (value) => {
    setInput(value);
    fetchData(value);
    fetchPerson(value);
  };

  const handleKeyPress = (e) => {
    if (e.key === "Enter") {
      fetchData(input);
      fetchPerson(input);
    }
  };

  return (
    <div className="input-wrapper">
      <FaSearch id="search-icon" />
      <input
        placeholder="Type to search..."
        value={input}
        onChange={(e) => handleChange(e.target.value)}
        onKeyUp={handleKeyPress}
      />
    </div>
  );
};