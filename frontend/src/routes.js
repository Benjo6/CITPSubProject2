import HomePage from "./view/HomePage";
import IndividualMovie from "./view/IndividualMovie";
import LoginPage from "./view/LoginPage";
import MoviesPage from "./view/MoviesPage";


const routes = [
    {path: '/', component: <HomePage />, exact: true},
    {path: '/movies', component: <MoviesPage />},
    {path: '/movie', component: <IndividualMovie />},
    {path: '/login', component: <LoginPage/>}
];

export default routes;