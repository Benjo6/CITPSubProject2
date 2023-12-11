import { Image, Col, Container, Row } from "react-bootstrap";
import styles from "./../../view/viewCss/individualMovie.module.css";
import React from 'react';

function IndividualMovie1(props) {
    const placeholderImage = 'https://via.placeholder.com/400';
    return(
        <>
        <Container >
            <Row>
                <Col sm={6}>
                    <div className='text-center'>
                        <Image src={props.Poster !== "N/A" ? props.Poster : placeholderImage} alt={props.title} width={224} height={320} rounded/>
                    </div> 
                </Col>
                <Col sm={6}>
                    <div>
                        <h1 className="text-center">Creed</h1>
                        <p className='ms-4'>Runtime: {props.runtime ? `${props.runtime} minutes` : 'N/A'}</p>
                        <p className='ms-4'>Actors: {props.Actors}</p>
                        <p className='ms-4'>Genre: {props.genres || 'N/A'}</p>
                        <p className='ms-4'>Date: {props.endYear || 'N/A'}</p>
                        <p className='ms-4'>Adult: {props.isAdult ? 'Yes' : 'No'}</p>
                        <p className='ms-4'>Episodes: {props.episodesCount}</p>
                    </div>
                </Col>
            </Row>
        </Container>
        <Container>
            <Row>
            <Col sm={8}>
                <div className={styles.desc}>
                    <h5>Description:</h5>
                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>
                </div>
            </Col>
            <Col sm={4}>
                <div className={styles.grad}>
                    <h5>Grading system</h5>
                    <p>Rating: {props.rating ? props.rating : 'N/A'}</p>
                </div>
            </Col>
            </Row>
        </Container>
        </>
        
    );
}

export default IndividualMovie1;