import React, { useContext, useEffect } from "react";
import { useAuth } from "react-oidc-context";
import { DataContext } from "../Contexts/DataContext";
import { fetchTimeRegistrationsForProject } from "../Services/ApiService";
import TimeRegistrationTable from "./TimeRegistrationTable";

const TimeRegisterHandler = () => {
  const auth = useAuth();
  const { activeProject, activeCustomer, setTimeRegisters } =
    useContext(DataContext);

  useEffect(() => {
    var token = auth.user?.access_token;
    if (activeProject) {
      fetchTimeRegistrationsForProject(
        activeCustomer.id,
        activeProject.id,
        token
      ).then((result) => {
        setTimeRegisters(result.data);
      });
    }
  }, [
    activeCustomer.id,
    activeProject,
    auth.user?.access_token,
    setTimeRegisters,
  ]);
  return <TimeRegistrationTable />;
};

export default TimeRegisterHandler;
