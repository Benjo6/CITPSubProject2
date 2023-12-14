import HomePage from "./view/Homepage";
import IndividualMovie from "./view/IndividualMovie";
import LoginPage from "./view/LoginPage";
import MoviesPage from "./view/Moviespage";
import RegisterPage from "./view/RegisterPage";
import SearchPage from "./view/SearchPage";


const routes = [
    {path: '/', component: <HomePage />, exact: true},
    {path: '/movies', component: <MoviesPage />},
    {path: '/movie/:id', component: <IndividualMovie />},
    {path: '/login', component: <LoginPage/>},
    {path: '/register', component: <RegisterPage/>},
    {path: '/search', component: <SearchPage/>}

];

export default routes;