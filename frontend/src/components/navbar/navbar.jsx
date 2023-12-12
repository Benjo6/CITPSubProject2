import React from 'react';
import { Navbar, Nav, Container } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';
import styles from './NavBar.module.css';

const NavBar = () => {
  return (
    <Navbar bg="black" expand="lg" className={styles.navbar} sticky='top'>
      <Container className={styles.navbarContainer}>
        <Navbar.Brand as={NavLink} to="/" style={{color: '#fff'}}>YourBrand</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link as={NavLink} to="/" className={styles.navLink}>
              <span>
                Home
              </span>
            </Nav.Link>
            <Nav.Link as={NavLink} to="/movies" className={styles.navLink}>
            <span >
              Movies
              </span>
            </Nav.Link>
            <Nav.Link as={NavLink} to="/movie" className={styles.navLink}>
            <span >
              Movie
              </span>
            </Nav.Link>
            <Nav.Link as={NavLink} to="/login" className={styles.navLink}>
            <span >
              Login
              </span>
            </Nav.Link>
            <Nav.Link as={NavLink} to="/register" className={styles.navLink}>
            <span >
              Register
              </span>
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default NavBar;
