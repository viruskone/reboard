import React from 'react';
import styles from "../assets/jss/reboard/components/pageLoader"
import { makeStyles } from "@material-ui/core/styles";
import { LinearProgress, Fade } from '@material-ui/core';

const useStyles = makeStyles(styles);

const PageLoader = () => {
    const classes = useStyles();

    return (
        <Fade
            in
            style={{
                transitionDelay: '800ms',
              }}
            unmountOnExit
        >
            <div className={classes.wrapper}>
                <LinearProgress />
            </div>
        </Fade>
    )
}

export default PageLoader