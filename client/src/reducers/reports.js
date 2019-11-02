import initialState from "./initialState";
import * as actions from "../constants/actions";
import { toSeconds } from '../utils/time'

export default function reportsReducer(state = initialState.reports, action) {
  switch (action.type) {
    case actions.FETCH_REPORTS_SUCCESS:
      return Object.assign({}, state, {
        list: action.payload.map(report => Object.assign({}, report, {
          key: report.id,
          averageDuration: toSeconds(report.averageDuration) * 1000,
          tags: []
        }))
      });
    default:
      return state;
  }
}
