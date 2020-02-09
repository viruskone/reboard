import { Container, CssBaseline } from '@material-ui/core';
import { makeStyles } from "@material-ui/core/styles";
import { useSnackbar } from 'notistack';
import PropTypes from "prop-types";
import React from 'react';
import { connect } from "react-redux";
import { Redirect } from 'react-router-dom';
import { login as loginAction } from '../actions/authActions';
import logo from '../assets/images/logo.png';
import styles from '../assets/jss/reboard/views/login';
import LoginForm from '../components/LoginForm/LoginForm'
import PageLoader from "../components/PageLoader";

const useStyles = makeStyles(styles);

const showErrorSnackbarIfOccur = error => {
    const { enqueueSnackbar } = useSnackbar();
    if (error) {
        enqueueSnackbar("Login failed. Check typed credentials", {
            variant: 'error'
        })
    }
}

const useLoader = isLoading => {
    return isLoading ? <PageLoader /> : "";
  };

const LoginView = (props) => {

    const classes = useStyles()
    const loader = useLoader(props.loading)

    showErrorSnackbarIfOccur(props.wrongCredentials)

    return props.isAuthenticated ?
        <Redirect to={props.location.state} /> :
        <Container component="main" maxWidth="xs">
            {loader}
            <CssBaseline />
            <div className={classes.wrapper}>
                <img src={logo} className={classes.logo} />
                <LoginForm login={props.login} />
            </div>
        </Container>
}

LoginView.propTypes = {
    location: PropTypes.object.isRequired,
    login: PropTypes.func.isRequired,
    isAuthenticated: PropTypes.bool.isRequired,
    loading: PropTypes.bool,
    wrongCredentials: PropTypes.bool
};

function mapStateToProps(state) {
    return {
        isAuthenticated: state.auth.isAuthenticated,
        loading: state.auth.loading,
        wrongCredentials: state.auth.wrongCredentials
    };
}

function mapDispatchToProps(dispatch) {
    return {
        login: (login, password) => dispatch(loginAction(login, password))
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(LoginView);
