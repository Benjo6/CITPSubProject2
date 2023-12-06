import React from "react"
import { Card, Col, Container, Row } from "react-bootstrap"
import Image from "react-bootstrap/Image"
import styles from "./viewCss/individualMovie.module.css"

export default function IndividualMovie(){
    return(
    <>
        <Container >
            <div className='text-center'>
                <Image src='https://deadline.com/wp-content/uploads/2022/10/creed-iii-michael-b-jordan-canelo-alvarez.jpg?w=1000' rounded className="img-fluid"/>
                <div className='d-flex'>
                    <div className={styles.poster}>
                        <Card style={{ width: '13rem'}}>
                        <Card.Img variant="top" src="https://m.media-amazon.com/images/M/MV5BYzIxOTk1NDQtMzJlOC00ODZlLWE1YTAtNTA5ODZlZmZmMDBhXkEyXkFqcGdeQXVyMjkwOTAyMDU@._V1_.jpg"/>
                        </Card>
                    </div> 
                    <div className={styles.info}>
                        <h1>Creed</h1>
                        <div className='d-flex'>
                        <p className='ms-1'>Duration: 98mins</p>
                        <p className='ms-4'>Actors: Michael B Jordan</p>
                        <p className='ms-4'>Genre: Action</p>
                        </div>
                        
                    </div>
                </div>
            </div>
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
                </div>
            </Col>
            </Row>
        </Container>
        
    </>
    
    )
}