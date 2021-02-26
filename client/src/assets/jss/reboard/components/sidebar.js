import { sidebarWidth, logoWidth } from "../variables";
import image from "../../../images/sidebar-river.jpg";
import logoImage from "../../../images/logo.png";
//import { flexbox } from "@material-ui/system";

const orgLogoSize = {w: 218, h: 68};
const logoSize = {w: logoWidth, h: Math.floor(orgLogoSize.h * logoWidth / orgLogoSize.w)};

export default theme => ({
  root: {
    width: sidebarWidth
  },
  content: {
    zIndex: 3
  },
  logoWrapper: {
    borderBottom: '1px solid rgba(180, 180, 200, 0.3)',
    padding: `${theme.spacing(4)}px ${theme.spacing(1)}px`,
    margin: `0 ${theme.spacing(3)}px`,
  },
  logo: {
    height: logoSize.h + 'px',
    display: "block",
    backgroundImage: `url(${logoImage})`,
    backgroundSize: `${logoSize.w}px ${logoSize.h}px`,
    backgroundPosition: 'left center',
    backgroundRepeat: 'no-repeat',
    color: 'transparent',
    position: "relative"
  },
  backdrop: {
    backgroundImage: `url(${image})`,
    backgroundSize: "cover",
    backgroundPosition: "50% 50%",
    width: "100%",
    height: "100%",
    position: "absolute",
    top: 0,
    left: 0,
    zIndex: 1,
    "&:after": {
      content: '""',
      width: "100%",
      height: "100%",
      position: "absolute",
      top: 0,
      left: 0,
      backgroundColor: "#000",
      opacity: 0.8,
      zIndex: 2
    }
  },
  nav: {
    padding: `${theme.spacing(2)}px ${theme.spacing(3)}px`
  },
  navText: {
    color: 'rgba(255, 255, 255, 0.8)',
    textTransform: 'uppercase'
  },
  navIcon: {
    position: 'relative',
    top: '6px',
    marginRight: '16px'
  },
  navItem: {
    padding: `${theme.spacing(1)}px ${theme.spacing(2)}px`,
    margin: `${theme.spacing(1)}px 0`
  }
});
