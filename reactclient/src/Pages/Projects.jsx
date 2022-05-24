import React, { useState, useEffect } from "react";
import { useAuth } from "react-oidc-context";

import ProjectsTable from "../Components/ProjectsTable";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import Paper from "@mui/material/Paper";
import { Button, Grid, Typography } from "@mui/material";
import { Container } from "@mui/system";
import { Divider } from "@mui/material";

import BasicModal from "../Components/CreateNewTimeRegisterModal";
import { fetchProjectsForCustomer } from "../Services/ApiService";
import TimeRegistration from "../Pages/TimeRegistration";
import useActiveProject from "../Hooks/useActiveProject";

const Projects = ({ selectedCustomer, setSelectedCustomer }) => {
  const auth = useAuth();
  const { activeProject, setActiveProject } = useActiveProject();
  const [projects, setProjects] = useState([]);
  const [selectedProject, setSelectedProject] = useState(null);
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  useEffect(() => {
    var token = auth.user?.access_token;
    fetchProjectsForCustomer(selectedCustomer.id, token).then((result) => {
      setProjects(result.data);
    });
  }, [auth.user?.access_token, selectedCustomer.id]);
  return (
    <Container component={Paper} sx={{ p: 3 }} fixed>
      <Grid container direction="row" spacing={2}>
        <Grid item xs>
          <Typography variant="h4" color="text.secondary" sx={{ mb: 2 }}>
            {selectedCustomer.name}
          </Typography>
        </Grid>
        <Grid align="end" item xs={2}>
          <Button
            sx={{ color: "inherit" }}
            onClick={() => {
              setSelectedCustomer(null);
              setSelectedProject(null);
            }}
          >
            <ArrowBackIcon />
          </Button>
        </Grid>
      </Grid>

      <Divider sx={{ mb: 2 }} />

      <Grid container alignItems="center" spacing={2}>
        <Grid item xs={12}>
          <Typography variant="h5" color="text.secondary" sx={{ mb: 2 }}>
            Projects
          </Typography>
        </Grid>
      </Grid>
      <Grid container alignItems="start" spacing={2}>
        <Grid item xs={4}>
          <ProjectsTable
            projects={projects}
            selectedProject={selectedProject}
            setSelectedProject={setSelectedProject}
          />
        </Grid>
        <Grid item xs={8}>
          <TimeRegistration
            selectedCustomer={selectedCustomer}
            selectedProject={selectedProject}
          />
        </Grid>
      </Grid>
      {selectedProject && (
        <>
          <Button onClick={handleOpen} sx={{ mt: 2 }}>
            New register for {selectedProject.projectName}
          </Button>
          <BasicModal
            handleClose={handleClose}
            open={open}
            selectedProject={selectedProject}
            selectedCustomer={selectedCustomer}
          />
        </>
      )}
    </Container>
  );
};

export default Projects;
