import * as React from "react";
import Typography from "@mui/material/Typography";
import { Container } from "@mui/system";
import Paper from "@mui/material/Paper";

import { useAuth } from "react-oidc-context";
import { Divider } from "@mui/material";
const Token = () => {
  const auth = useAuth();
  return (
    <>
      <Container
        component={Paper}
        sx={{ maxWidth: 500, wordWrap: "break-word", p: 3 }}
      >
        <Container sx={{ maxWidth: 500, wordWrap: "break-word", p: 3 }}>
          <Typography variant="h2" color="text.secondary">
            Access Token
          </Typography>
          <Divider />
          <Typography
            variant="body2"
            color="text.secondary"
            sx={{
              mt: 2,
            }}
          >
            {auth.user?.access_token}
          </Typography>
        </Container>
      </Container>
    </>
  );
};

export default Token;
