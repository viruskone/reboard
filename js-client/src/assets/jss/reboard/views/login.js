export default theme => ({
    wrapper: {
        paddingTop: theme.spacing(1),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
      },
      logo: {
        width: "50%",
        marginBottom: theme.spacing(6)
      },
      form: {
        width: '100%',
      },
      submit: {
        margin: theme.spacing(5, 0, 2),
      },
  });
  