import React from "react";
import { NavLink } from "react-router-dom";

import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import { useAuth } from "react-oidc-context";
import { Button } from "@mui/material";
import LogoutIcon from "@mui/icons-material/Logout";
import LoginIcon from "@mui/icons-material/Login";

const Header = () => {
  const auth = useAuth();

  return (
    <Box sx={{ flexGrow: 1, mb: 4 }}>
      <AppBar
        position="static"
        sx={{ backgroundColor: "#F4F4F6", color: "#161925" }}
      >
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            TimeTracker
            <NavLink
              to="/"
              style={({ isActive }) => {
                return {
                  justifySelf: "start",
                  margin: "1rem 0 1rem 1rem",
                  padding: "2px 0 2px 0",
                  borderBottom: isActive ? "2px solid" : "none",
                  color: isActive ? "#4B4237" : "#161925",
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
                  padding: "2px 0 2px 0",
                  borderBottom: isActive ? "2px solid" : "none",
                  color: isActive ? "#4B4237" : "#161925",
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
              Logout
              <LogoutIcon sx={{ ml: 2 }} />
            </Button>
          )}
          {!auth.isAuthenticated && (
            <Button
              color="inherit"
              onClick={auth.signinRedirect}
              variant="text"
            >
              Login
              <LoginIcon sx={{ ml: 2 }} />
            </Button>
          )}
        </Toolbar>
      </AppBar>
    </Box>
  );
};

export default Header;
