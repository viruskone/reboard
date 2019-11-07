import * as types from "../constants/actions";
import { setAuthenticationToken } from "../services/authService";

export function login(login, password) {
    return function(dispatch) {
      dispatch({ type: types.LOGIN_REQUEST });
      fetch("http://localhost:5000/api/auth/login", { 
        method: 'POST', 
        body: JSON.stringify({login, password}),
        headers: {
          'Content-Type': 'application/json'
        }
      })
        .then(response => response.json())
        .then(response => {
          setAuthenticationToken(response.token)
          dispatch({
            type: types.LOGIN_SUCCESS,
            payload: { status: true }
          })}
        )
        .catch(err =>
          dispatch({
            type: types.LOGIN_FAILURE,
            error: err
          })
        );
    };
  }