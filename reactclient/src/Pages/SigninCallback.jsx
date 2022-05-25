import { Typography } from "@mui/material";
import { Container } from "@mui/system";
import React from "react";

const SigninCallback = () => {
  return (
    <Container>
      <Typography variant="h3" color="text" sx={{ mb: 2, textAlign: "center" }}>
        Welcome you are now logged in
      </Typography>
    </Container>
  );
};

export default SigninCallback;
