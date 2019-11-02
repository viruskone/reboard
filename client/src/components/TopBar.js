import React from "react";
import { makeStyles } from "@material-ui/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Timeline from "@material-ui/icons/Timeline";
import Button from "@material-ui/core/Button";
import AccountCircle from "@material-ui/icons/AccountCircle";
import Typography from "@material-ui/core/Typography";

const useStyles = makeStyles(theme => ({
  iconNextText: {
    marginRight: theme.spacing(1)
  },
  name: {
    textTransform: "uppercase",
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2)
  }
}));

export default function TopBar() {
  const classes = useStyles();
  return (
    <AppBar position="static">
      <Toolbar>
        <Timeline className={classes.iconNextText} />
        <Typography component="h2" className={classes.name}>
          reboard
        </Typography>
        <div>
          <Button className={classes.menuButton} color="inherit">
            <AccountCircle className={classes.iconNextText} />
            login
          </Button>
        </div>
      </Toolbar>
    </AppBar>
  );
}
