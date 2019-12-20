function fetchStatus(url) {
  return new Promise((resolve, reject) => {
    const fetchStatusInternal = () => {
      fetch(url, {
        method: "GET"
      })
        .then(response => {
          if (response.status === 202) {
            setTimeout(fetchStatusInternal, 100);
          } else if (response.status >= 200 && response.status < 300) {
            resolve(response);
          }
          // probably get what they want
          else {
            reject(response);
          }
        })
        .catch(reject);
    };
    fetchStatusInternal();
  });
}

export default function fetch202(url, options) {
  return new Promise((resolve, reject) => {
    fetch(url, options).then(response => {
      if (response.status !== 202 || !response.headers.has("Location")) {
        reject(response);
      }

      fetchStatus(response.headers.get("Location")).then(resolve, reject);
    });
  });
}
