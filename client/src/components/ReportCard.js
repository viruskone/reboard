import React from "react";
import { makeStyles } from "@material-ui/styles";
import PropTypes from "prop-types";
import Card from "@material-ui/core/Card";
import CardHeader from "@material-ui/core/CardHeader";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import Typography from "@material-ui/core/Typography";
import { Avatar, Chip, Button } from "@material-ui/core";
import { red, purple, blue, teal, brown, blueGrey } from "@material-ui/core/colors";
import { formatTimeSpan } from '../utils/humanDate';

const mainColors = {
  0: blueGrey,
  1: brown,
  2: teal,
  3: blue,
  4: purple,
  5: red
}

const useStyles = makeStyles(theme => ({
  header: {
    overflow: "hidden"
  },
  limitDescription: {
    height: '3rem',
    display: 'block',
    overflow: 'hidden',
    wordWrap: 'break-word',
    textOverflow: 'ellipsis'
  },
  chip: {
    marginRight: theme.spacing(0.5),
    marginBottom: theme.spacing(1)
  },
  chipDate: {
    color: theme.palette.secondary.dark,
    borderColor: theme.palette.secondary.dark
  },
  actions: {
    display: 'flex',
    justifyContent: 'space-between',
    paddingLeft: `0 ${theme.spacing(2)}px`,
    paddingRight: `0 ${theme.spacing(2)}px`,
    '& > div': { 
      padding: `0 ${theme.spacing(1)}px`,
      '& > span': {
        display: 'block',
      }
    }
  },
  actionValue: Object.assign({}, theme.typography.h6, { fontSize: '1rem', color: theme.palette.primary.main }),
  actionLabel: Object.assign({}, theme.typography.body2, { fontSize: '0.8rem', color: theme.palette.grey[500] }),
}));

const ratingColor = rating => {
  const mainColorPalette = Math.floor(rating)
  const variantPalette = Math.round((rating - mainColorPalette) * 10)
  return mainColors[mainColorPalette][(variantPalette > 0) ? (variantPalette * 100) : 50]
}

const chipColorStyles = makeStyles(() => ({
  chipColor: {
    backgroundColor: rating => ratingColor(rating)
  }
}))

function ReportCard(props) {
  const { rating, title, description, createDate, tags, downloads, duration } = props;

  const classes = useStyles();
  const chipColorClasses = chipColorStyles(rating)

  return (
    <Card elevation={1}>
      <CardHeader
        avatar={<Avatar className={chipColorClasses.chipColor}>{rating.toFixed(1)}</Avatar>}
        title={title}
        titleTypographyProps={{
          variant: "h6",
          noWrap: true
        }}
        subheader={
          <React.Fragment>
            <Chip
              label={createDate.toLocaleString(undefined, {day: 'numeric', month: 'long', year: 'numeric'})}
              classes={{
                root: classes.chip,
                outlinedSecondary: classes.chipDate
              }}
              color="secondary"
              size="small"
              variant="outlined"
            />
            {tags || tags.map(tag=>
              <Chip
                key={tag}
                label={tag}
                className={classes.chip}
                color="primary"
                size="small"
                variant="outlined"
              />)}
          </React.Fragment>
        }
        classes={{ content: classes.header }}
      />
      <CardContent>
        <Typography className={classes.limitDescription} paragraph color="textSecondary">
          {description}
        </Typography>
      </CardContent>
      <CardActions className={classes.actions}>
        <div>
          <Typography className={classes.actionValue}>{downloads}</Typography>
          <Typography className={classes.actionLabel}>pobrano</Typography>
        </div>
        <div>
          <span className={classes.actionValue}>{formatTimeSpan(duration)}</span>
          <span className={classes.actionLabel}>średni czas</span>
        </div>
        <div>
          <Button variant="contained" color="primary">Pobierz</Button>
        </div>
      </CardActions>
    </Card>
  );
}

ReportCard.propTypes = {
  rating: PropTypes.number.isRequired,
  title: PropTypes.string.isRequired,
  description: PropTypes.string.isRequired,
  createDate: PropTypes.instanceOf(Date).isRequired,
  tags: PropTypes.arrayOf(PropTypes.string),
  downloads: PropTypes.number.isRequired,
  duration: PropTypes.number.isRequired
};

export default ReportCard;
