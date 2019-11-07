export const isAuthenticated = () => localStorage.getItem('auth') !== null
export const setAuthenticationToken = token => localStorage.setItem('auth', token)