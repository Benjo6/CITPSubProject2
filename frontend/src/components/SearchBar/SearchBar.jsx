import { useState } from "react";
import { FaSearch } from "react-icons/fa";
import "../SearchBar/SearchBar.css";

export const SearchBar = ({ setResults, setPerson }) => {
  const [input, setInput] = useState("");

  const fetchData = async (value) => {
    const response = await fetch(`https://localhost:7098/Search/Movie?searchString=${value}&resultCount=5`);
    const movieSearch = await response.json(); 
    setResults(movieSearch.result);
  }
  const fetchPerson = (person) => {
    fetch(`https://localhost:7098/Search/Person?searchString=${person}&resultCount=5`)
      .then((response) => response.json())
      .then((json) => {
        const res = json;
        setPerson(res.result);
      });
  };

  const handleChange = (value) => {
    setInput(value);
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