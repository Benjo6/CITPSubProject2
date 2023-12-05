import HomePage from "./view/Homepage";
import MoviesPage from "./view/Moviespage";


const routes = [
    {path: '/', component: <HomePage />, exact: true},
    {path: '/movies', component: <MoviesPage />}
];

export default routes;