import React from "react";
import PropTypes from 'prop-types';
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";
import ThemeApp from "./ThemeApp";

function Root(props) {
  const { store, history } = props;

  return (
    <Provider store={store}>
      <ConnectedRouter history={history}>
        <ThemeApp />
      </ConnectedRouter>
    </Provider>
  );
}

Root.propTypes = {
  store: PropTypes.object.isRequired,
  history: PropTypes.object.isRequired
}

export default Root;