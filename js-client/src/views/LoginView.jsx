import { Container, CssBaseline } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import PropTypes from "prop-types";
import React from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";
import { login as loginAction } from "../actions/authActions";
import logo from "../assets/images/logo.png";
import styles from "../assets/jss/reboard/views/login";
import LoginForm from "../components/LoginForm/LoginForm";
import PageLoader from "../components/PageLoader";
import { toast } from "react-toastify";
import { getErrorMessage } from "../infrastructure/error";
import { useEffect } from "react";

const useStyles = makeStyles(styles);

const handleErrors = (errors) => {
  useEffect(() => {
    if (errors.code) {
      var toastId = toast.error(getErrorMessage(errors));
      return () => { toast.dismiss(toastId) }
    }
  }, [errors.code]);
};

const useLoader = (isLoading) => {
  return isLoading ? <PageLoader /> : "";
};

const LoginView = (props) => {
  handleErrors(props.error);
  const classes = useStyles();
  const loader = useLoader(props.loading);

  return props.isAuthenticated ? (
    <Redirect to={props.location.state} />
  ) : (
    <Container component="main" maxWidth="xs">
      {loader}
      <CssBaseline />
      <div className={classes.wrapper}>
        <img src={logo} className={classes.logo} />
        <LoginForm login={props.login} />
      </div>
    </Container>
  );
};

LoginView.propTypes = {
  location: PropTypes.object.isRequired,
  login: PropTypes.func.isRequired,
  isAuthenticated: PropTypes.bool.isRequired,
  loading: PropTypes.bool,
  error: PropTypes.object,
};

function mapStateToProps(state) {
  return {
    isAuthenticated: state.auth.isAuthenticated,
    loading: state.auth.loading,
    error: state.auth.error,
  };
}

function mapDispatchToProps(dispatch) {
  return {
    login: (login, password) => dispatch(loginAction(login, password)),
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(LoginView);
