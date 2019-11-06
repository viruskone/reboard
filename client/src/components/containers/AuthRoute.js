import React from "react";
import PropTypes from 'prop-types';
import { Route, Redirect } from 'react-router-dom';
import { isAuthenticated } from '../../services/authService';

const AuthRoute = ({ component: Component, ...rest }) => (
  <Route {...rest} render={props => (
      isAuthenticated()
          ? <Component {...props} />
          : <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
  )} />
)

AuthRoute.propTypes = {
  component: PropTypes.elementType,
  location: PropTypes.object
}

export default AuthRoute