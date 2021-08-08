import initialState from "./initialState";
import * as actions from "../constants/actions";
import { toSeconds } from '../infrastructure/time'

export default function reportsReducer(state = initialState.reports, action) {
  switch (action.type) {
    case actions.FETCH_REPORTS_SUCCESS:
      return Object.assign({}, state, {
        list: action.payload.map(report => Object.assign({}, report, {
          key: report.id,
          averageDuration: toSeconds(report.averageGenerationTime) * 1000,
          tags: []
        }))
      });
    default:
      return state;
  }
}
