import React, { useEffect } from "react";
import { connect } from "react-redux";
import { fetchReports } from '../actions/reportActions';
import PropTypes from "prop-types";
import { Container, Grid } from "@material-ui/core";
import ReportCard from "../components/ReportCard";
import styles from '../assets/jss/reboard/views/reports';
import { makeStyles } from "@material-ui/styles";

const useStyles = makeStyles(styles);

const reportCard = report => (
    <Grid item xs={12} lg={6} xl={4} key={report.id}>
        <ReportCard
            title={report.title}
            description={report.description}
            createDate={new Date(report.createTime)}
            tags={report.tags}
            downloads={report.downloads}
            duration={report.averageDuration}
            color={report.color}
            shortcut={report.shortcut}
        />
    </Grid>
);

function ReportsView(props) {

    useEffect(() => {
        props.fetchReports()
    }, props.reports.length)

    const classes = useStyles();

    return (
        <Container className={classes.content} maxWidth="xl">
            <Grid container spacing={6}>
                {props.reports.map(d => reportCard(d))}
            </Grid>
        </Container>
    );
}

ReportsView.propTypes = {
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
)(ReportsView);
