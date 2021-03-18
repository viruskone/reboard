import * as types from "../constants/actions";
import { setAuthenticationToken } from "../services/authService";
import { fetchCommand } from "../utils/fetch";

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
          payload: {
            reason: 'WrongCredential'
          }
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
        payload: {
          reason: 'InternalError',
          error: err
        }
      })
    );
  };
}
