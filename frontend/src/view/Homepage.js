import React from 'react';
import Container from 'react-bootstrap/esm/Container';
import ControlledCarousel from '../components/carousel/carousel';
import BasicExample from '../components/Picture/card';
import { data } from "../utils/card";

export default function HomePage() {
    return (
        <>
            <Container className='my-auto'>
                <ControlledCarousel></ControlledCarousel>
            </Container>
            <Container className='my-auto pt-5'>
                <h3>Top Picks</h3>
                <div className='d-flex'>
                    {data.map((movie) => {
                        return <BasicExample {...movie} />;
                    })}
                </div>
            </Container>
            <Container className='my-auto pt-5'>
                <h3>Wishlist</h3>
                <div className='d-flex'>
                    {data.map((movie) => {
                        return <BasicExample {...movie} />;
                    })}
                </div>
            </Container>
        </>
    )
}
