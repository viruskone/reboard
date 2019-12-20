import * as actions from "../constants/actions";
import initialState from "./initialState";
import { isAuthenticated } from '../services/authService'

export default function reportsReducer(state = initialState.auth, action) {
    switch (action.type) {
        case '@@INIT':
            return Object.assign({}, state, { isAuthenticated: isAuthenticated() })
        case actions.LOGIN_REQUEST:
            return Object.assign({}, state, {
                loading: true
            })
        case actions.LOGIN_SUCCESS:
            return Object.assign({}, state, {
                isAuthenticated: action.payload.status,
                loading: false
            });
        case actions.LOGIN_FAILURE:
            return Object.assign({}, state, {
                loading: false
            })
        default:
        return state;
  }
}
