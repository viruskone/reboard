import React, { useState } from 'react';
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { makeStyles } from "@material-ui/core/styles";
import { Redirect } from 'react-router-dom';
import styles from '../assets/jss/reboard/views/login';
import { Container, CssBaseline, TextField, Button } from '@material-ui/core';
import { login as loginAction } from '../actions/authActions';

const useStyles = makeStyles(styles);

const LoginView = (props) => {

    const [login, setLogin] = useState('')
    const [password, setPassword] = useState('')

    const [loginError, setLoginError] = useState('')
    const [passwordError, setPasswordError] = useState('')

    const handleSubmit = (event) => {
        event.preventDefault();
        if (login === '')
            setLoginError('Type your e-mail')
        if (password === '')
            setPasswordError('Type your password')
        if (loginError + passwordError === '')
            props.login(login, password)
        else
            return false
    }

    const classes = useStyles()
    if (props.isAuthenticated) {
        return (<Redirect to={props.location.state} />)
    } else {
        return (
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <div className={classes.wrapper}>
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
    isAuthenticated: PropTypes.bool.isRequired
};

function mapStateToProps(state) {
    return {
        isAuthenticated: state.auth.isAuthenticated
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
