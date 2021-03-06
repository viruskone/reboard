import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import reportsReducer from './reports';
import authReducer from './auth';
import ui from './ui';

const rootReducer = history => combineReducers({
  router: connectRouter(history),
  reports: reportsReducer,
  auth: authReducer,
  ui
});

export default rootReducer;