import { padding } from '../utils'

const navbarStyle = theme => ({
    root: {
        backgroundColor: 'transparent',
        boxShadow: 'none',
    },
    toolbar: {
        padding: padding(theme.spacing, 1, 2)
    },
    title: {
        flex: 1,
        padding: padding(theme.spacing, 1, 3)
    }
  });
  
  export default navbarStyle;