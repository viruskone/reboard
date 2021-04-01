import ReportsView from "./views/ReportsView";
import LoginView from "./views/LoginView";
import ReportDetails from "./views/ReportDetails";
import { matchPath } from "react-router"

const defaultType = {
  needsAuth: false,
  showTitle: true,
  showNavbar: true
};

const type = {
  loginView: Object.assign({}, defaultType, { showNavbar: false }),
  authenticatedView: Object.assign({}, defaultType, {
    needsAuth: true
  })
};

const routes = [
  {
    path: "/reports",
    title: "Reports",
    component: ReportsView,
    ...type.authenticatedView
  },
  {
    path: "/login",
    title: "Sign In",
    component: LoginView,
    ...type.loginView
  },
  {
    path: "/report/:reportId",
    component: ReportDetails,
    showTitle: true,
    showNavbar: true
  }
];

export const getActiveRoute = () =>
  //routes.find(route => window.location.href.indexOf(route.path) !== -1);
  routes.find(route => matchPath(window.location.pathname, route))

export default routes;
