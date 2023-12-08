import React from 'react';
import { Navbar, Nav, Container } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';
import styles from './NavBar.module.css';

const NavBar = () => {
  return (
    <Navbar bg="light" expand="lg" className={styles.navbar} sticky='top'>
      <Container className={styles.navbarContainer}>
        <Navbar.Brand as={NavLink} to="/">YourBrand</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link as={NavLink} to="/" className={styles.navLink}>
              Home
            </Nav.Link>
            <Nav.Link as={NavLink} to="/movies" className={styles.navLink}>
              Movies
            </Nav.Link>
            <Nav.Link as={NavLink} to="/movie" className={styles.navLink}>
              Movie
            </Nav.Link>
            <Nav.Link as={NavLink} to="/login" className={styles.navLink}>
              Login
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default NavBar;
