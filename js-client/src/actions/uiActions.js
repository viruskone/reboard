import * as types from "../constants/actions";

export const toggleSidebar = () => ({
    type: types.TOGGLE_SIDEBAR
});

export const setNavbarTitle = title => ({
    type: types.NAVBAR_TITLE,
    payload: { title }
})
