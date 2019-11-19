import initialState from "./initialState";
import * as actions from "../constants/actions";

export default function reportsReducer(state = initialState.ui, action) {
  switch (action.type) {
    case actions.TOGGLE_SIDEBAR:
      return Object.assign({}, state, {
        sidebarOpened: !state.sidebarOpened
      });
    default:
      return state;
  }
}
