import * as actions from "../constants/actions";
import initialState from "./initialState";
import { isAuthenticated } from '../services/authService'

export default function reportsReducer(state = initialState.auth, action) {
    switch (action.type) {
        case '@@INIT':
            return Object.assign({}, state, { isAuthenticated: isAuthenticated() })
        case actions.LOGIN_SUCCESS:
            return Object.assign({}, state, {
                isAuthenticated: action.payload.status
            });
        default:
        return state;
  }
}
