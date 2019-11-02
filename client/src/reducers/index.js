import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router'
import reportsReducer from './reports'

const rootReducer = history => combineReducers({
  router: connectRouter(history),
  reports: reportsReducer
});

export default rootReducer;