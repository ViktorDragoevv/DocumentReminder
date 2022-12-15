import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import Category from './components/Category';
import Company from './components/Company';
import Contacts from './components/Contacts';
import { Counter } from "./components/Counter";
import CustomCalendar from './components/CustomCalendar';
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import Location from './components/Locations';

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
    {
        path: '/locations',
        requireAuth: true,
        element: <Location />
    },
    {
        path: '/company',
        requireAuth: true,
        element: <Company />
    },
  ...ApiAuthorzationRoutes
];

export default AppRoutes;
