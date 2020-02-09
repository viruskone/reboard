import React from "react";
import { makeStyles } from "@material-ui/styles";
import PropTypes from "prop-types";
import Card from "@material-ui/core/Card";
import CardHeader from "@material-ui/core/CardHeader";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import Typography from "@material-ui/core/Typography";
import { Avatar, Chip, Button } from "@material-ui/core";
import { formatTimeSpan } from '../utils/humanDate';
import GetApp from "@material-ui/icons/GetApp";
import AccessTime from "@material-ui/icons/AccessTime";
import styles from '../assets/jss/reboard/components/reportCard';

const useStyles = makeStyles(styles);


const chipColorStyles = makeStyles(() => ({
  chipColor: {
    backgroundColor: (color) => `rgb(${color.red}, ${color.green}, ${color.blue})`
  }
}))

function ReportCard(props) {
  const { color, title, description, createDate, tags, downloads, duration, shortcut } = props;

  const classes = useStyles();
  const chipColorClasses = chipColorStyles(color)

  return (
    <Card elevation={1}>
      <CardHeader
        avatar={<Avatar className={`${classes.avatar} ${chipColorClasses.chipColor}`}>{shortcut}</Avatar>}
        title={title}
        titleTypographyProps={{
          variant: "h6",
          classes: { root: classes.title }
        }}
        classes={{ content: classes.header }}
      />
      <CardContent className={classes.content}>
        <Typography className={classes.limitDescription} paragraph color="textSecondary">
          {description}
        </Typography>
        <div className={classes.tags}>
          {tags || tags.map(tag =>
            <Chip
              key={tag}
              label={tag}
              className={classes.chip}
              color="primary"
              size="small"
              variant="outlined"
            />)}
        </div>

      </CardContent>
      <CardActions className={classes.actions}>
        <div>
          <GetApp fontSize="inherit" className={classes.actionIcon} /> 
          <Typography className={classes.actionValue}>
            {downloads}
          </Typography>
        </div>
        <div>
          <AccessTime fontSize="inherit" className={classes.actionIcon} /> 
          <Typography className={classes.actionValue}>
            {formatTimeSpan(duration)}
          </Typography>
        </div>
        <div className={classes.date}>
          <Typography className={classes.actionValue}>
            {createDate.toLocaleString(undefined, { day: 'numeric', month: 'long', year: 'numeric' })}
          </Typography>
        </div>
      </CardActions>
    </Card>
  );
}

ReportCard.propTypes = {
  color: PropTypes.object.isRequired,
  title: PropTypes.string.isRequired,
  description: PropTypes.string.isRequired,
  createDate: PropTypes.instanceOf(Date).isRequired,
  tags: PropTypes.arrayOf(PropTypes.string),
  downloads: PropTypes.number.isRequired,
  duration: PropTypes.number.isRequired,
  shortcut: PropTypes.string.isRequired
};

export default ReportCard;
