import BasicExample from "../components/Picture/card";
import { SearchBar } from "../components/SearchBar/SearchBar";
import { useState } from 'react';


function SearchPage() {
    const [results, setResults] = useState([]);

    return(
    <>
        <SearchBar setResults={setResults}/>
        <div className='d-flex flex-wrap'>
        {results.map((movie) => {
        return <BasicExample key={movie.id} {...movie} className='m-auto' />
        })} 
        </div>
        
    </>
    );
}

export default SearchPage;