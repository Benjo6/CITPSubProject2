import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import SessionManager from "../components/Auth/SessionManager";
import AuthenticationDataService from "../dataservices/AuthenticationDataService";
import 'bootstrap/dist/css/bootstrap.min.css';

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();
  
  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const data = await AuthenticationDataService.login({ username, password });
      SessionManager.setUserSession(username, data.token);
      alert('You have successfully logged in.');
      navigate('/');
    } catch (error) {
      setError(error.message);
    }
  };

  return (
    <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-md-6">
          <div className="card">
            <h5 className="card-header">Login</h5>
            <div className="card-body">
              <form onSubmit={handleLogin}>
                <div className="mb-3">
                  <label htmlFor="username" className="form-label">Username</label>
                  <input
                    type="text"
                    className="form-control"
                    id="username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    placeholder="Enter username"
                    required
                  />
                </div>
                <div className="mb-3">
                  <label htmlFor="password" className="form-label">Password</label>
                  <input
                    type="password"
                    className="form-control"
                    id="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    placeholder="Enter password"
                    required
                  />
                </div>
                <button type="submit" className="btn btn-success">Login</button>
                {error && <div className="alert alert-danger mt-2">{error}</div>}
              </form>
              <h5>You do not have an account, yet?<Link to={'/register'}>Register HERE!</Link></h5>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;