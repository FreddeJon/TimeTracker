import React from "react";
import { NavLink } from "react-router-dom";

import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import { useAuth } from "react-oidc-context";
import { Button } from "@mui/material";

const Header = () => {
  const auth = useAuth();

  return (
    <Box sx={{ flexGrow: 1, mb: 3 }}>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            TimeTracker
            <NavLink
              to="/"
              style={({ isActive }) => {
                return {
                  justifySelf: "start",
                  margin: "1rem 0 1rem 1rem",
                  color: isActive ? "orange" : "inherit",
                  textDecoration: "none",
                };
              }}
            >
              Home
            </NavLink>
            <NavLink
              to="/token"
              style={({ isActive }) => {
                return {
                  justifySelf: "start",
                  margin: "1rem 0 1rem 1rem",
                  color: isActive ? "orange" : "inherit",
                  textDecoration: "none",
                };
              }}
            >
              Token
            </NavLink>
          </Typography>

          {auth.isAuthenticated && (
            <Button
              color="inherit"
              onClick={auth.signoutRedirect}
              variant="text"
            >
              <p>Logout</p>
            </Button>
          )}
          {!auth.isAuthenticated && (
            <Button
              color="inherit"
              onClick={auth.signinRedirect}
              variant="text"
            >
              <p>Login</p>
            </Button>
          )}
        </Toolbar>
      </AppBar>
    </Box>
  );
};

export default Header;
