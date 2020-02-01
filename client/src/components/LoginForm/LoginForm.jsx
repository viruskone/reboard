import React from 'react';
import PropTypes from "prop-types";
import { makeStyles } from "@material-ui/core/styles";
import styles from '../../assets/jss/reboard/views/login';
import { TextField, Button } from '@material-ui/core';
import build from './localState'

const useStyles = makeStyles(styles);

const LoginForm = (props) => {
    const { login, password, handleSubmit } = build(props.login)

    const classes = useStyles()

    return (
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
                value={login.get()}
                onChange={e => login.set(e.target.value)}
                error={login.haveError}
                helperText={login.getError()}
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
                value={password.get()}
                onChange={e => password.set(e.target.value)}
                error={password.haveError}
                helperText={password.getError()}
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
    )
}
LoginForm.propTypes = {
    login: PropTypes.func.isRequired,
};

export default LoginForm
