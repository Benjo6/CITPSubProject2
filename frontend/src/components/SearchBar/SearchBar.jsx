import React, { useState } from "react";
import { FaSearch } from "react-icons/fa";
import Select from 'react-select';
import "../SearchBar/SearchBar.css";
import SearchDataService from "../../dataservices/SearchDataService";
import SearchCard from "../Picture/searchCard";

export const SearchBar = ({ setResults, setPerson, results, person }) => {
  const [input, setInput] = useState("");
  const [loading, setLoading] = useState(false);
  const [selectedOption, setSelectedOption] = useState({ value: 'movies', label: 'Movies' });
  const [isOpen, setIsOpen] = useState(false); // Add state for dropdown visibility

  const options = [
    { value: 'movies', label: 'Movies' },
    { value: 'person', label: 'Person' }
  ];

  const fetchData = async (value) => {
    setLoading(true);
    setIsOpen(true); // Open the dropdown when search starts
    if (selectedOption.value === 'movies') {
      const response = await SearchDataService.movieSearch(value, 5)
      setResults(response.result);
    } else if (selectedOption.value === 'person') {
      const response = await SearchDataService.personSearch(value, 5)
      console.log(response.result);
      setPerson(response.result);
    }
    setLoading(false);
  };

  const handleChange = (value) => {
    setInput(value);
    fetchData(value);
  };

  return (
    <div>
    <div className="input-wrapper">
      <div className="input-line">
        <div className="select-wrapper">
          <Select
            value={selectedOption}
            onChange={setSelectedOption}
            options={options}
          />
        </div>
        <input
          placeholder="Type to search..."
          value={input}
          onChange={(e) => handleChange(e.target.value)}
        />
        <FaSearch id="search-icon" onClick={() => setIsOpen(!isOpen)} />
      </div>
      </div>
      {isOpen && (
        <div className="dropdown">
          {loading ? (
            <div>Loading...</div>
          ) : (
            <>
              {selectedOption.value === 'movies' && results && results.map((result, index) => (
                <SearchCard key={index} id={result.id} title={result.title} rating={result.rating} /> // Use the SearchCard component
              ))}
              {selectedOption.value === 'person' && person && person.map((person, index) => (
                <SearchCard key={index} id={person.id} title={person.name} rating={person.rating} /> // Use the SearchCard component
              ))}
            </>
          )}
        </div>
      )}
    </div>
  );
};  
