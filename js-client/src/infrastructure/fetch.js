import {
  isAuthenticated,
  getAuthenticationToken,
  burnToken
} from "../services/authService";
import { history } from "../store/configureStore";

function addHeaders(options) {
  const headers = {};
  headers["Accept"] = "application/json";
  headers["Content-Type"] = "application/json";
  if (isAuthenticated())
    headers["Authorization"] = "Bearer " + getAuthenticationToken();
  return Object.assign({}, options, { headers });
}

export function fetchQuery(url, options) {
  return fetch(url, addHeaders(options)).then(response => {
    if (response.status === 401) {
      burnToken()
      history.push('/login')
    }
    return response
  });
}

export function fetchCommand(url, options) {
  return new Promise((resolve, reject) => {
    fetch(url, addHeaders(options)).then(response => {
      if(response.status === 200){
        resolve(response);
        return;
      }
      if (response.status !== 202 || !response.headers.has("Location")) {
        reject(response);
        return;
      }
      // websocket notification to implement
    }, reject);
  });
}
