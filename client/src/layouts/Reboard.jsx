import React from 'react';
import { connect } from "react-redux";
import { bindActionCreators } from 'redux';
import PropTypes from "prop-types";
import { Switch, Route, Redirect } from 'react-router-dom';
import { ThemeProvider } from "@material-ui/styles";
import { createMuiTheme, makeStyles } from "@material-ui/core/styles";
import shadows from "../infrastructure/shadows";
import "../assets/css/reboard-layout.css"
import styles from "../assets/jss/reboard/layout"
import Sidebar from '../components/Sidebar';
import Navbar from '../components/Navbar';
import AuthRoute from '../components/AuthRoute';
import routes from '../routes';
import * as actions from '../actions/uiActions';
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const theme = createMuiTheme({
    palette: {
        primary: {
            main: "#1da1f2",
            light: "#6cd2ff",
            dark: "#0073bf",
            contrastText: "#FFF"
        },
        secondary: {
            main: "#cbe86b",
            light: "#ffff9c",
            dark: "#98b63b",
            contrastText: "#4a4c4d"
        },
        text: {
            primary: "rgba(69, 90, 100, 1)",
            secondary: "rgba(69, 90, 100, 0.87)",
            disabled: "rgba(69, 90, 100, 0.54)"
        }
    },
    typography: {
        useNextVariants: true,
        fontFamily: "Poppins, Helvetica, Arial, sans-serif"
    },
    shadows,
    props: {
        MuiPaper: {
            square: false
        }
    },
    overrides: {
    }
});
window.theme = theme;
const useStyles = makeStyles(styles);

const makeRoute = (route, key) => {
    const ComponentRoute = route.needsAuth ? AuthRoute : Route;
    return (<ComponentRoute path={route.path} component={route.component} key={key} />)
}

const makeViewPlaceholder = () => (
    <Switch>
        {routes.map(makeRoute)}
        <Redirect from="/" to="/reports" />
    </Switch>
)

const getActiveRoute = routeLocation => (
    routes.find(route => routeLocation.indexOf(route.path) !== -1)
)

const renderNavbar = (ui, actions, activeRoute) => {
    return (
        <React.Fragment>
            <Sidebar
                open
                {...actions}
                {...ui}
            />
            <Navbar
                {...actions}
                {...ui}
                title={activeRoute.showTitle ? activeRoute.title : ""} />
        </React.Fragment>
    )
}

const Reboard = ({ ui, routeLocation, actions }) => {

    const classes = useStyles();
    const activeRoute = getActiveRoute(routeLocation) || {};

    let navbar = ""
    if (activeRoute.showNavbar)
        navbar = renderNavbar(ui, actions, activeRoute)

        return (
            <ThemeProvider theme={theme}>
                    <div className={`${classes.wrapper} ${activeRoute.showNavbar ? '' : classes.wrapperNoSidebar}`}>
                        {navbar}
                        <div className={classes.content}>
                            {makeViewPlaceholder()}
                        </div>
                    </div>
                    <ToastContainer autoClose={10000} position={toast.POSITION.BOTTOM_RIGHT} />
            </ThemeProvider>
    )
}

Reboard.propTypes = {
    ui: PropTypes.object.isRequired,
    routeLocation: PropTypes.string,
    actions: PropTypes.object.isRequired
};

function mapStateToProps(state) {
    return {
        ui: state.ui,
        routeLocation: state.router.location.pathname
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(actions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(Reboard)