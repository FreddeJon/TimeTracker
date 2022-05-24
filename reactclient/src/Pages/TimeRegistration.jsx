import React, { useEffect, useState } from "react";
import { useAuth } from "react-oidc-context";
import TimeRegistrationTable from "../Components/TimeRegistrationTable";
import { fetchTimeRegistrationsForProject } from "../Services/ApiService";

const TimeRegistration = ({ selectedCustomer, selectedProject }) => {
  const auth = useAuth();
  const [registrations, setRegistrations] = useState([]);
  useEffect(() => {
    var token = auth.user?.access_token;
    if (selectedProject) {
      fetchTimeRegistrationsForProject(
        selectedCustomer.id,
        selectedProject.id,
        token
      ).then((result) => {
        setRegistrations(result.data);
      });
    } else {
      setRegistrations([]);
    }
  }, [auth.user?.access_token, selectedCustomer.id, selectedProject]);
  return <TimeRegistrationTable registrations={registrations} />;
};

export default TimeRegistration;
