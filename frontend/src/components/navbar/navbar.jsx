import React from 'react';
import { Navbar, Nav, Container } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import styles from './NavBar.module.css';
import SessionManager from '../Auth/SessionManager';

const NavBar = () => {
  const userToken = SessionManager.getToken();
  const navigate = useNavigate();

  const logout = () => {
    SessionManager.removeUserSession();
    navigate('/');
  }

  return (
    <Navbar bg="black" expand="lg" className={styles.navbar} sticky='top'>
      <Container className={styles.navbarContainer}>
        <Navbar.Brand as={NavLink} to="/" style={{color: '#fff'}}>RMDb</Navbar.Brand>
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
            <Nav.Link as={NavLink} to="/search" className={styles.navLink}>
            <span >
              Search
              </span>
            </Nav.Link>
            {userToken ?<>
           
            <Nav.Link as={NavLink} to="/bookmarks" className={styles.navLink}>
            <span >
              Bookmarks
              </span>
            </Nav.Link>
          
            <Nav.Link as={NavLink} to="/" className={styles.navLink} onClick={logout}>
            <span >
              Logout
              </span>
            </Nav.Link>  </>: <Nav.Link as={NavLink} to="/login" className={styles.navLink} >
            <span >
              Login
              </span>
            </Nav.Link>
            }
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default NavBar;
