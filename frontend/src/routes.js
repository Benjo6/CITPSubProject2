import HomePage from "./view/Homepage";
import IndividualMovie from "./view/IndividualMovie";
import LoginPage from "./view/LoginPage";
import MoviesPage from "./view/Moviespage";
import RegisterPage from "./view/RegisterPage";


const routes = [
    {path: '/', component: <HomePage />, exact: true},
    {path: '/movies', component: <MoviesPage />},
    {path: '/movie/:id', component: <IndividualMovie />},
    {path: '/login', component: <LoginPage/>},
    {path: '/register', component: <RegisterPage/>}

];

export default routes;