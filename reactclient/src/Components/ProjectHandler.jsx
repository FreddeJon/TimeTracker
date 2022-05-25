import React, { useContext, useEffect } from "react";
import { useAuth } from "react-oidc-context";
import { DataContext } from "../Contexts/DataContext";
import { fetchProjectsForCustomer } from "../Services/ApiService";
import ProjectsTable from "./ProjectsTable";

const ProjectHandler = () => {
  const { activeCustomer, setProjects } = useContext(DataContext);
  const auth = useAuth();

  useEffect(() => {
    var token = auth.user?.access_token;
    fetchProjectsForCustomer(activeCustomer.id, token).then((result) => {
      setProjects(result.data);
    });
  }, [auth.user?.access_token, activeCustomer.id, setProjects]);
  return <ProjectsTable />;
};

export default ProjectHandler;
