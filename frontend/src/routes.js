import HomePage from "./view/Homepage";
import IndividualMovie from "./view/IndividualMovie";
import MoviesPage from "./view/Moviespage";


const routes = [
    {path: '/', component: <HomePage />, exact: true},
    {path: '/movies', component: <MoviesPage />},
    {path: '/movie', component: <IndividualMovie />}
];

export default routes;