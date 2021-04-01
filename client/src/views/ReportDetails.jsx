import React, { useEffect } from "react";
import { connect } from "react-redux";
import { useParams } from "react-router";
import PropTypes from "prop-types";
import { Container } from "@material-ui/core";
import { setNavbarTitle } from "../actions/uiActions";

function ReportDetails(props) {
  const { reportId } = useParams();
  useEffect(() => {
    props.setTitle(reportId);
  }, [reportId]);
  return (
    <Container maxWidth="xl">
      <div>{reportId}</div>
    </Container>
  );
}

ReportDetails.propTypes = {
  //reports: PropTypes.array.isRequired,
  setTitle: PropTypes.func.isRequired,
};

function mapStateToProps(state) {
  return {
    //reports: state.reports.list
  };
}

function mapDispatchToProps(dispatch) {
  return {
    setTitle: (title) => dispatch(setNavbarTitle(title)),
    //fetchReports: () => dispatch(fetchReports())
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(ReportDetails);
