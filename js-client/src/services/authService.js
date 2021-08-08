export const isAuthenticated = () => localStorage.getItem('auth') !== null
export const setAuthenticationToken = token => localStorage.setItem('auth', token)
export const getAuthenticationToken = () => localStorage.getItem('auth')
export const burnToken = () => localStorage.removeItem('auth')