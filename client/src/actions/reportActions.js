import { fetchQuery } from "../infrastructure/fetch";
import * as types from "../constants/actions";

export function fetchReports() {
  return function(dispatch) {
    dispatch({ type: types.FETCH_REPORTS_RETRIEVE });
    fetchQuery("http://localhost:5000/api/reports")
      .then(response => response.json())
      .then(response =>
        dispatch({
          type: types.FETCH_REPORTS_SUCCESS,
          payload: response
        })
      )
      .catch(err => {
        dispatch({
          type: types.FETCH_REPORTS_FAILURE,
          error: err
        });
      });
  };
}
