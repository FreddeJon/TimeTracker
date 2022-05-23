import * as React from "react";
import Typography from "@mui/material/Typography";
import { Container } from "@mui/system";
import Header from "../Components/Header";

import { useAuth } from "react-oidc-context";
import { Divider } from "@mui/material";
const Token = () => {
  const auth = useAuth();
  return (
    <>
      <Header />
      <main>
        <Container sx={{ maxWidth: 500, wordWrap: "break-word" }}>
          <Typography variant="h2" color="text.secondary">
            Access Token
          </Typography>
          <Divider />
          <Typography variant="body2" color="text.secondary" sx={{ mt: 2 }}>
            {auth.user?.access_token}
          </Typography>
        </Container>
      </main>
    </>
  );
};

export default Token;
