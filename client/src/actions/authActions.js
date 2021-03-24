import * as types from "../constants/actions";
import { setAuthenticationToken } from "../services/authService";
import { fetchCommand } from "../infrastructure/fetch";
import { failurePayload } from "../infrastructure/error"
import { LOGIN_WRONG_CREDENTIALS, FETCH_ERROR } from "../constants/errors"

export function login(login, password) {
  return function(dispatch) {
    dispatch({ type: types.LOGIN_REQUEST });
    fetchCommand("http://localhost:5000/api/auth", {
      method: "POST",
      body: JSON.stringify({ login, password }),
      headers: {
        "Content-Type": "application/json"
      }
    })
    .then(response => response.json())
    .then(response => {
      if (response.status === "Failed") {
        dispatch({
          type: types.LOGIN_FAILURE,
          payload: failurePayload(LOGIN_WRONG_CREDENTIALS)
        });
      } else {
        setAuthenticationToken(response.token);
        dispatch({
          type: types.LOGIN_SUCCESS
        });
      }
    })
    .catch(err =>
      dispatch({
        type: types.LOGIN_FAILURE,
        payload: failurePayload(FETCH_ERROR, err)
      })
    );
  };
}
