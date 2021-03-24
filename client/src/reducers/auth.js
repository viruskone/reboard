import * as actions from "../constants/actions";
import initialState from "./initialState";
import { isAuthenticated } from '../services/authService'

export default function reportsReducer(state = initialState.auth, action) {
    switch (action.type) {
        case '@@INIT':
            return Object.assign({}, state, { isAuthenticated: isAuthenticated() })
        case actions.LOGIN_REQUEST:
            return Object.assign({}, state, {
                isAuthenticated: false,
                loading: true
            })
        case actions.LOGIN_SUCCESS:
            return Object.assign({}, state, {
                isAuthenticated: true,
                loading: false
            });
        case actions.LOGIN_FAILURE:
            return Object.assign({}, state, {
                isAuthenticated: false,
                loading: false,
                error: action.payload
            })
        default:
        return state;
  }
}
