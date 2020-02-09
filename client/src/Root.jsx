import React from "react";
import PropTypes from 'prop-types';
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";

function Root(props) {
  const { store, history, layout: Layout } = props;

  return (
    <Provider store={store}>
      <ConnectedRouter history={history}>
        <Layout />
      </ConnectedRouter>
    </Provider>
  );
}

Root.propTypes = {
  store: PropTypes.object.isRequired,
  history: PropTypes.object.isRequired,
  layout: PropTypes.elementType.isRequired
}

export default Root;