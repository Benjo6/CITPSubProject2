import { useState } from "react";
import { FaSearch } from "react-icons/fa";
import "../SearchBar/SearchBar.css";
import SearchDataService from "../../dataservices/SearchDataService";

export const SearchBar = ({ setResults, setPerson }) => {
  const [input, setInput] = useState("");

  const fetchData = async (value) => {
    const response = await SearchDataService.movieSearch(value, 5)
    setResults(response.result);
  }
  const fetchPerson = async (person) => {
    const response = await SearchDataService.personSearch(person, 5)
    setPerson(response.result);
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