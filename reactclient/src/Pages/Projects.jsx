import { Typography } from "@mui/material";
import { Container } from "@mui/system";
import React, { useState, useEffect } from "react";
import { useAuth } from "react-oidc-context";
import ProjectsTable from "../Components/ProjectsTable";
import { fetchProjectsForCustomer } from "../Services/ApiService";

const Projects = ({ selectedCustomer }) => {
  const auth = useAuth();
  const [projects, setProjects] = useState([]);

  useEffect(() => {
    var token = auth.user?.access_token;
    fetchProjectsForCustomer(selectedCustomer.id, token).then((result) => {
      setProjects(result.data);
    });
  }, [auth.user?.access_token, selectedCustomer.id]);

  return (
    <Container fixed>
      <Typography variant="h2" color="text.secondary" sx={{ mb: 2 }}>
        Projects
      </Typography>

      <ProjectsTable projects={projects} />
    </Container>
  );
};

export default Projects;
