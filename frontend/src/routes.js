import HomePage from "./view/HomePage";
import IndividualMovie from "./view/IndividualMovie";
import LoginPage from "./view/LoginPage";
import MoviesPage from "./view/MoviesPage";
import RegisterPage from "./view/RegisterPage";


const routes = [
    {path: '/', component: <HomePage />, exact: true},
    {path: '/movies', component: <MoviesPage />},
    {path: '/movie/:id', component: <IndividualMovie />},
    {path: '/login', component: <LoginPage/>},
    {path: '/register', component: <RegisterPage/>}

];

export default routes;