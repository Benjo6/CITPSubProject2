import { Container } from "react-bootstrap";
import SearchCard from "../components/Picture/searchCard";
import { SearchBar } from "../components/SearchBar/SearchBar";
import { useState } from 'react';


function SearchPage() {
    const [results, setResults] = useState([]);
    const [person, setPerson] = useState([]);

    return(
    <>
        <Container>
            <SearchBar setResults={setResults} setPerson={setPerson}/>
        </Container>
        
        <Container className='mt-5'>
            <div className='d-flex flex-wrap mt-5'>
        {results.map((movie) => (
        <SearchCard key={movie.id} {...movie} className='m-auto' />
        ))} 
        </div>
        <div className='d-flex flex-wrap mt-5'>
        {person.map((person) => (
        <SearchCard key={person.id} {...person} className='m-auto' />
        ))} 
        </div>
        </Container>
        
        
    </>
    );
}

export default SearchPage;