const reportCard = theme => ({
  header: {
    overflow: "hidden",
  },
  title: {
    display: '-webkit-box',
    '-webkit-line-clamp': 2,
    '-webkit-box-orient': 'vertical',
    overflow: 'hidden'  
  },
  content: {
    paddingTop: '0px'
  },
  limitDescription: {
    height: '3rem',
    display: '-webkit-box',
    '-webkit-line-clamp': 2,
    '-webkit-box-orient': 'vertical',
    overflow: 'hidden'  
  },
  avatar: {
      width: theme.spacing(8),
      height: theme.spacing(8)
  },
  chip: {
    marginRight: theme.spacing(0.5),
    marginBottom: theme.spacing(1),
  },
  chipDate: {
    color: theme.palette.secondary.dark,
    borderColor: theme.palette.secondary.dark
  },
  actions: {
    borderTop: 'solid 1px #d7d7d7',
    display: "flex",
    justifyContent: "space-between",
    padding: `${theme.spacing(2)}px ${theme.spacing(3)}px`,
    "& > div": {
      fontSize: '0.8rem',
    }
  },
  actionIcon: {
    fontSize: '1rem',
    verticalAlign: 'middle',
    color: theme.palette.grey[500],
    marginRight: theme.spacing(1),
  },
  actionValue: {
    fontSize: "inherit",
    display: "inline-block",
    color: theme.palette.grey[500]
  },
  date: {
    width: '45%',
    textAlign: 'right'
  }
});

export default reportCard;
