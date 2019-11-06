import * as types from "../constants/actions";

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
        .then(response =>
          dispatch({
            type: types.LOGIN_SUCCESS,
            payload: response
          })
        )
        .catch(err =>
          dispatch({
            type: types.LOGIN_FAILURE,
            error: err
          })
        );
    };
  }