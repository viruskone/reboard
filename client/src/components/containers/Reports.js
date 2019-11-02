import React, { useEffect } from "react";
import { connect } from "react-redux";
import { fetchReports } from '../../actions/reportActions';
import PropTypes from "prop-types";
import ReportsList from "../ReportsList";

function Reports(props) {
  
  useEffect(() => {
    props.fetchReports()
  }, props.reports.length)

  return (
    <ReportsList reports={props.reports} />
  );
}

Reports.propTypes = {
  reports: PropTypes.array.isRequired,
  fetchReports: PropTypes.func.isRequired
};

function mapStateToProps(state) {
    return {
      reports: state.reports.list
    };
  }
  
  function mapDispatchToProps(dispatch) {
    return {
      fetchReports: () => dispatch(fetchReports())
    };
  }
  
  export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(Reports);
