import React, { useContext } from "react";

import { DataContext } from "../Contexts/DataContext";

// MUI
import { Button, Divider, Typography } from "@mui/material";
import { Container } from "@mui/system";
import Paper from "@mui/material/Paper";
import { Grid } from "@mui/material";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";

import ProjectHandler from "./ProjectHandler";
import TimeRegisterHandler from "./TimeRegisterHandler";
import CustomerHandler from "./CustomerHandler";
import NewRegisterForm from "./Form/NewRegisterForm";

const Main = () => {
  const { activeCustomer, setActiveCustomer, activeProject, setActiveProject } =
    useContext(DataContext);

  return (
    <Container component={Paper} sx={{ p: 3 }} fixed>
      <Grid container direction="row" spacing={2}>
        <Grid item xs>
          <Typography variant="h4" color="text.secondary" sx={{ mb: 2 }}>
            Time Tracker
          </Typography>
        </Grid>
        <Grid align="end" item xs={2}>
          <Button
            sx={{ color: "inherit" }}
            onClick={() => {
              setActiveCustomer(null);
              setActiveProject(null);
            }}
          >
            <ArrowBackIcon />
          </Button>
        </Grid>
      </Grid>
      <Divider sx={{ mb: 2 }} />

      <Grid container alignItems="center" spacing={2}>
        <Grid item xs={12}>
          <Typography
            variant="h5"
            color="text.secondary"
            sx={{ mb: 2 }}
          ></Typography>
        </Grid>
      </Grid>
      <Grid container alignItems="start" spacing={2}>
        <Grid item xs={activeCustomer === null ? 12 : 4}>
          <Typography variant="h5" color="text.secondary" sx={{ mb: 2 }}>
            Customers
          </Typography>
          <CustomerHandler />
        </Grid>
        {activeCustomer && (
          <Grid item xs={8}>
            <Typography variant="h5" color="text.secondary" sx={{ mb: 2 }}>
              Projects for {activeCustomer.name}
            </Typography>
            <ProjectHandler />
          </Grid>
        )}
      </Grid>

      {activeProject && (
        <Grid container alignItems="start" spacing={2} sx={{ mt: 3 }}>
          <Grid item xs={7}>
            <TimeRegisterHandler />
          </Grid>
          <Grid item xs={5}>
            <NewRegisterForm />
          </Grid>
        </Grid>
      )}
    </Container>
  );
  // return <CustomerHandler />;
};

export default Main;
