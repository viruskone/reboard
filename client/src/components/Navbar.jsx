import React from 'react'
import { Typography, AppBar, Toolbar, IconButton } from '@material-ui/core'
import MenuIcon from '@material-ui/icons/Menu';
import styles from '../assets/jss/reboard/components/navbar';
import { makeStyles } from "@material-ui/core/styles";
import PropTypes from "prop-types";

const useStyles = makeStyles(styles);

const Navbar = (props) => {

    const classes = useStyles()
    const { title, toggleSidebar } = props

    return (
        <AppBar color="inherit" className={classes.root}>
            <Toolbar className={classes.toolbar}>
                <Typography variant="subtitle1" color="textPrimary" component="h1" className={classes.title}>
                    {title}
                </Typography>
                <IconButton edge="start" aria-label="menu" onClick={toggleSidebar}>
                    <MenuIcon />
                </IconButton>
            </Toolbar>
        </AppBar>
    )
}

Navbar.propTypes = {
    title: PropTypes.string,
    toggleSidebar: PropTypes.func.isRequired
  };
  
export default Navbar