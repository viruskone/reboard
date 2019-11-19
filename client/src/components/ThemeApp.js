import React from "react";
import { ThemeProvider } from "@material-ui/styles";
import { createMuiTheme } from "@material-ui/core/styles";
import shadows from "../utils/shadows";
import App from "./App";

const theme = createMuiTheme({
  palette: {
    primary: {
      main: "#1da1f2",
      light: "#6cd2ff",
      dark: "#0073bf",
      contrastText: "#FFF"
    },
    secondary: {
      main: "#cbe86b",
      light: "#ffff9c",
      dark: "#98b63b",
      contrastText: "#4a4c4d"
    },
    text: {
      primary: "rgba(69, 90, 100, 1)",
      secondary: "rgba(69, 90, 100, 0.87)",
      disabled: "rgba(69, 90, 100, 0.54)"
    }
  },
  typography: {
    useNextVariants: true,
    fontFamily: "Poppins, Helvetica, Arial, sans-serif"
  },
  shadows,
  props: {
    MuiPaper: {
      square: false
    }
  },
  overrides: {
  }
});
export default function ThemeApp() {
  return (
    <ThemeProvider theme={theme}>
      <App />
    </ThemeProvider>
  );
}
