const layoutStyle = () => ({
  wrapper: {
    position: "relative",
    top: "0",
    "&:before": {
      content: '""',
      display: 'table',
      height: '0px'
    }
  },
  content: {
    marginTop: '70px'
  }
});

export default layoutStyle;