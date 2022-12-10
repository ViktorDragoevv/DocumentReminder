import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import Category from './components/Category';
import Contacts from './components/Contacts';
import { Counter } from "./components/Counter";
import CustomCalendar from './components/CustomCalendar';
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <CustomCalendar />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    requireAuth: true,
    element: <FetchData />
  },
  {
        path: '/category',
        requireAuth: true,
        element: <Category />
  },
  {
        path: '/contacts',
        requireAuth: true,
        element: <Contacts />
  },
  ...ApiAuthorzationRoutes
];

export default AppRoutes;
