import React, { useState } from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import Grid from "@material-ui/core/Grid";
import { TextField, SvgIcon, Button } from "@material-ui/core";
import { login as loginAction } from '../../actions/authActions';
import { Redirect } from 'react-router-dom';

const LoginPage = (props) => {

  const handleSubmit = (event) => {
    event.preventDefault();
    if(login === '')
      setLoginError('Podaj swój login')
    if(password === '')
      setPasswordError('Podaj hasło')
    if(loginError + passwordError === '')
      props.login(login, password)
    else 
      return false
  }

  const [login, setLogin] = useState('')
  const [password, setPassword] = useState('')

  const [loginError, setLoginError] = useState('')
  const [passwordError, setPasswordError] = useState('')

    if(props.isAuthenticated) {
      return(<Redirect to={props.location.state} />)
    } else {
    return(
    <Grid
      container
      spacing={0}
      alignItems="center"
      justify="center"
      style={{ minHeight: "100vh" }}
    >
      <Grid item xs={6}>
          <SvgIcon viewBox="0 0 24 24">
            <path fill="#000000" d="M22 4L20 2C18.85 2.64 17.4 3 16 3C14.6 3 13.14 2.63 12 2C10.86 2.63 9.4 3 8 3C6.6 3 5.15 2.64 4 2L2 4C2 4 4 6 4 8S2 14 2 16C2 20 12 22 12 22S22 20 22 16C22 14 20 10 20 8S22 4 22 4M15.05 16.45L11.97 14.59L8.9 16.45L9.72 12.95L7 10.61L10.58 10.3L11.97 7L13.37 10.29L16.95 10.6L14.23 12.94L15.05 16.45Z" />
          </SvgIcon>
          {props.location.pathname}
          <form noValidate autoComplete="off" onSubmit={handleSubmit}>
            <Grid
              container
              direction="column"
              spacing={2}
              alignItems="center"
              justify="center">
            <Grid item xs={6}>
          <TextField 
                id="login"
                label="Login"
                required
                autoFocus
                value={login}
                onChange={e => setLogin(e.target.value)}
                error={loginError !== ''}
                helperText={loginError}
                />
            </Grid>
            <Grid item xs={6}>
          <TextField 
                id="password"
                label="Password"
                type="password"
                required
                value={password}
                onChange={e => setPassword(e.target.value)}
                error={passwordError !== ''}
                helperText={passwordError}
                />
                </Grid>
                <Grid item xs={6}>
                  <Button type="submit">Wyślij</Button>
                </Grid>
                </Grid>
          </form>
      </Grid>
    </Grid>
  )}
};

LoginPage.propTypes = {
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
)(LoginPage);
