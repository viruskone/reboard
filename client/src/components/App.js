import React from "react";
import { Switch, Route } from 'react-router-dom';
import TopBar from "./TopBar";
import LoginPage from "./containers/LoginPage";
import ReportsPage from "./containers/ReportsPage";
import AuthRoute from "./containers/AuthRoute";

function App() {
  return (
    <React.Fragment>
      <TopBar />
      <Switch>
        <AuthRoute exact path="/" component={ReportsPage} />
        <Route path="/login" component={LoginPage} />
      </Switch>
    </React.Fragment>
  );
}

export default App;
