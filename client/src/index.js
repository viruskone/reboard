import './logger-init'
import React from "react";
import { render } from "react-dom";
//import { AppContainer } from "react-hot-loader";
import Root from "./Root";
import configureStore, { history } from "./store/configureStore";
import Reboard from "./layouts/Reboard";

const store = configureStore();

render(
    <Root store={store} history={history} layout={Reboard} />,
  document.getElementById("app")
);