import { sidebarWidth } from "./variables";


const layoutStyle = (theme) => ({
  wrapper: {
    position: "relative",
    top: "0",
    "&:before": {
      content: '""',
      display: 'table',
      height: '0px'
    },
    [theme.breakpoints.up("md")]: {
      width: `calc(100% - ${sidebarWidth}px)`
    },
  },
  content: {
    marginTop: '70px'
  }
});

export default layoutStyle;