import SearchCard from "../components/Picture/personCard";
import { SearchBar } from "../components/SearchBar/SearchBar";
import { useState } from 'react';


function SearchPage() {
    const [results, setResults] = useState([]);

    return(
    <>
        <SearchBar setResults={setResults}/>
        <div className='d-flex flex-wrap'>
        {results.map((movie) => {
        return <SearchCard key={movie.id} {...movie} className='m-auto' />
        })} 
        </div>
        
    </>
    );
}

export default SearchPage;