import React from 'react';
import PropTypes from 'prop-types'

import Drawer from "@material-ui/core/Drawer";
import AppsIcon from '@material-ui/icons/Apps';
import styles from '../assets/jss/reboard/components/sidebar'
import { makeStyles } from '@material-ui/core/styles'
import { Typography } from '@material-ui/core';

const useStyles = makeStyles(styles);

const makeMenu = (classes) => {
    return (
        <nav className={classes.nav}>
            {makeMenuItem(AppsIcon, "Raporty", classes)}
        </nav>
    )
}

const makeMenuItem = (Icon, title, classes) => {
    return (
        <div className={classes.navItem}>
            <Typography className={classes.navText}>
                <Icon className={classes.navIcon} />
                {title}
            </Typography>
        </div>
    )
}

const Sidebar = ({ sidebarOpened, toggleSidebar }) => {
    const classes = useStyles()
    return (
        <Drawer
            variant="temporary"
            anchor="right"
            classes={{ paper: classes.root }}
            open={sidebarOpened}
            onClose={toggleSidebar}
        >
            <div className={classes.content}>
                <div className={classes.logoWrapper}>
                    <a className={classes.logo} href="/">Go back to home page</a>
                </div>
                {makeMenu(classes)}
            </div>
            <div className={classes.backdrop} />
        </Drawer>
    )
}

Sidebar.propTypes = {
    sidebarOpened: PropTypes.bool.isRequired,
    toggleSidebar: PropTypes.func.isRequired
}

export default Sidebar;