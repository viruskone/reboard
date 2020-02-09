export const isAuthenticated = () => localStorage.getItem('auth') !== null
export const setAuthenticationToken = token => localStorage.setItem('auth', token)
export const getAuthenticationToken = () => localStorage.getItem('auth')