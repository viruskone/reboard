import React, { useState } from 'react';
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { makeStyles } from "@material-ui/core/styles";
import { Redirect } from 'react-router-dom';
import styles from '../assets/jss/reboard/views/login';
import { Container, CssBaseline, TextField, Button } from '@material-ui/core';
import { login as loginAction } from '../actions/authActions';
import logo from '../assets/images/logo.png';
import PageLoader from '../components/PageLoader';

const useStyles = makeStyles(styles);

const LoginView = (props) => {

    const [login, setLogin] = useState('')
    const [password, setPassword] = useState('')

    const [loginError, setLoginError] = useState('')
    const [passwordError, setPasswordError] = useState('')

    const validateEmail = email => {
        // eslint-disable-next-line no-useless-escape
        var re = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        return re.test(String(email).toLowerCase());
    }

    const emailValidators = [
        { testInvalid: value => value === '', message: "You must type your e-mail" },
        { testInvalid: value => !validateEmail(value), message: "Type correct e-mail address" }
    ]

    const passwordValidators = [
        { testInvalid: value => value === '', message: "You must type password" }
    ]

    const validate = (validators, value, setErrorFunction) => {
        var invalid = validators.find(element => element.testInvalid(value))
        if (invalid) {
            setErrorFunction(invalid.message)
            return false
        }
        setErrorFunction('')
        return true
    }

    const handleSubmit = (event) => {
        event.preventDefault();
        const emailIsValid = validate(emailValidators, login, setLoginError)
        const passwordIsValid = validate(passwordValidators, password, setPasswordError)

        if(emailIsValid && passwordIsValid)
            props.login(login, password)
        else
            return false

    }

    const classes = useStyles()
    
    let loader = ""
    if(props.loading)
        loader = <PageLoader />
    
    if (props.isAuthenticated) {
        return (<Redirect to={props.location.state} />)
    } else {
        return (
            <Container component="main" maxWidth="xs">
                {loader}
                <CssBaseline />
                <div className={classes.wrapper}>
                    <img src={logo} className={classes.logo} />
                    <form className={classes.form} noValidate onSubmit={handleSubmit}>
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="email"
                            label="Email Address"
                            name="email"
                            autoComplete="email"
                            autoFocus
                            value={login}
                            onChange={e => setLogin(e.target.value)}
                            error={loginError !== ''}
                            helperText={loginError}
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                            value={password}
                            onChange={e => setPassword(e.target.value)}
                            error={passwordError !== ''}
                            helperText={passwordError}
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            size="large"
                            className={classes.submit}
                        >
                            Sign In
                        </Button>
                    </form>
                </div>
            </Container>
        )
    }
}
LoginView.propTypes = {
    location: PropTypes.object.isRequired,
    login: PropTypes.func.isRequired,
    isAuthenticated: PropTypes.bool.isRequired,
    loading: PropTypes.bool
};

function mapStateToProps(state) {
    return {
        isAuthenticated: state.auth.isAuthenticated,
        loading: state.auth.loading
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
