import {
  isAuthenticated,
  getAuthenticationToken
} from "../services/authService";

function fetchStatus(url) {
  return new Promise((resolve, reject) => {
    const fetchStatusInternal = () => {
      fetch(
        url,
        addHeaders({
          method: "GET"
        })
      )
        .then(response => {
          if (response.status === 202) {
            setTimeout(fetchStatusInternal, 100);
          } else if (response.status >= 200 && response.status < 300) {
            resolve(response);
          } else {
            reject(response);
          }
        })
        .catch(reject);
    };
    fetchStatusInternal();
  });
}

function addHeaders(options) {
  const headers = {};
  headers["Accept"] = "application/json";
  headers["Content-Type"] = "application/json";
  if (isAuthenticated())
    headers["Authorization"] = "Bearer " + getAuthenticationToken();
  return Object.assign({}, options, { headers });
}

export function fetchQuery(url, options) {
  return fetch(url, addHeaders(options))
}

export function fetchCommand(url, options) {
  return new Promise((resolve, reject) => {
    fetch(url, addHeaders(options)).then(response => {
      if (response.status !== 202 || !response.headers.has("Location")) {
        reject(response);
      }

      fetchStatus(response.headers.get("Location")).then(statusResponse => {
        fetch(
          statusResponse.headers.get("Location"),
          addHeaders({
            method: "GET"
          })
        ).then(resolve, reject);
      }, reject);
    });
  });
}
