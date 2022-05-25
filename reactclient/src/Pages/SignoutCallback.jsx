import { Typography } from "@mui/material";
import { Container } from "@mui/system";
import React from "react";
import { useAuth } from "react-oidc-context";

const SignoutCallback = () => {
  const auth = useAuth();

  if (auth.isAuthenticated) {
    auth.removeUser();
  }

  return (
    <Container>
      <Typography variant="h3" color="text" sx={{ mb: 2, textAlign: "center" }}>
        Logged out
      </Typography>
    </Container>
  );
};

export default SignoutCallback;
