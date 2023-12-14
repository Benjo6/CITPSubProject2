import { useState } from "react";
import { FaSearch } from "react-icons/fa";
import "../SearchBar/SearchBar.css";

export const SearchBar = ({ setResults }) => {
  const [input, setInput] = useState("");

  const fetchData = (value) => {
    fetch(`https://localhost:7098/Search?searchString=${value}&resultCount=10`)
      .then((response) => response.json())
      .then((json) => {
        const results = json;
        setResults(results);
      });
  };

  const handleChange = (value) => {
    setInput(value);
    fetchData(value);
  };

  return (
    <div className="input-wrapper">
      <FaSearch id="search-icon" />
      <input
        placeholder="Type to search..."
        value={input}
        onChange={(e) => handleChange(e.target.value)}
      />
    </div>
  );
};