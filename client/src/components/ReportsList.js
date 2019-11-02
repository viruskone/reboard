import React from "react";
import PropTypes from 'prop-types';
import { makeStyles } from "@material-ui/styles";
import Grid from "@material-ui/core/Grid";
import Container from "@material-ui/core/Container";
import ReportCard from "./ReportCard";

const useStyles = makeStyles(theme => ({
  content: {
    marginTop: theme.spacing(10)
  },
  boardTitle: {
    color: theme.palette.primary.dark,
    fontSize: theme.typography.pxToRem(theme.typography.htmlFontSize * 2.5),
    marginBottom: theme.spacing(4)
  }
}));

const jsxReportCard = report => (
  <Grid item xs={6} key={report.id}>
    <ReportCard
      title={report.title}
      description={report.description}
      createDate={new Date(report.createTime)}
      tags={report.tags}
      downloads={report.downloads}
      duration={report.averageDuration}
      rating={report.rating}
    />
  </Grid>
);

function ReportsList(props) {

  const { reports } = props

  const classes = useStyles();
  return (
    <Container className={classes.content}>
      <Grid container spacing={6}>
        {reports.map(d => jsxReportCard(d))}
      </Grid>
    </Container>
  );
}

ReportsList.propTypes = {
  reports: PropTypes.array.isRequired,
}

export default ReportsList