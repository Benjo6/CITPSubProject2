import HomePage from "./view/Homepage";
import IndividualMovie from "./view/IndividualMovie";
import LoginPage from "./view/LoginPage";
import MoviesPage from "./view/Moviespage";
import RegisterPage from "./view/RegisterPage";
import SearchPage from "./view/SearchPage";
import Bookmarkpage from "./view/Bookmarkpage";


const routes = [
    {path: '/', component: <HomePage />, exact: true},
    {path: '/movies', component: <MoviesPage />},
    {path: '/movie/:id', component: <IndividualMovie />},
    {path: '/login', component: <LoginPage/>},
    {path: '/register', component: <RegisterPage/>},
    {path: '/search', component: <SearchPage/>},
    {path: '/bookmarks', component: <Bookmarkpage/>}

];

export default routes;