import ReportsView from "./views/ReportsView";
import LoginView from "./views/LoginView";

const defaultType = {
  needsAuth: false,
  showTitle: false
};

const type = {
  loginView: Object.assign({}, defaultType, { showTitle: true }),
  authenticatedView: Object.assign({}, defaultType, {
    needsAuth: true,
    showTitle: true
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
  }
];

export const getActiveRoute = () =>
  routes.find(route => window.location.href.indexOf(route.path) !== -1);

export default routes;
