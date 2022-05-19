import React, { useState, useEffect } from "react";
import {
  fetchCustomers,
  fetchProjectsForCustomer,
  fetchTimeRegistrationsForProject,
} from "../Services/ApiService";

const CreateTimeRegistration = () => {
  const [customers, setCustomers] = useState([]);
  const [currentCustomer, setCurrentCustomer] = useState(null);
  const [projects, setProjects] = useState([]);
  const [currentProject, setCurrentProject] = useState(null);
  const [timeRegisters, setTimeRegisters] = useState([]);

  useEffect(() => {
    async function fetch() {
      await fetchCustomers().then((res) => {
        setCustomers(res.data);
        var hmm = res.data[0].id ? res.data[0].id : null;
        setCurrentCustomer(hmm);
      });
    }
    fetch();
  }, []);

  useEffect(() => {
    debugger;
    if (!currentCustomer) {
      return;
    }
    async function fetch() {
      await fetchProjectsForCustomer(currentCustomer).then((res) => {
        setProjects(res.data);
        setCurrentProject(res.data[0].id ? res.data[0].id : null);
      });
    }
    fetch();
  }, [currentCustomer]);

  useEffect(() => {
    if (!currentProject) {
      return;
    }
    fetchTimeRegistrationsForProject(currentCustomer, currentProject).then(
      (res) => {
        setTimeRegisters(res.data);
      }
    );
  }, [currentCustomer, currentProject]);

  return (
    <div>
      <label>Customer: </label>
      <select onChange={(e) => setCurrentCustomer(e.target.value)}>
        {customers.map((comp) => (
          <option value={comp.id}>{comp.name}</option>
        ))}
      </select>
      <label>Project: </label>
      <select onChange={(e) => setCurrentProject(e.target.value)}>
        {projects.map((project) => (
          <option value={project.id}>{project.projectName}</option>
        ))}
      </select>

      <ul>
        {timeRegisters.map((x) => (
          <li>{x.description}</li>
        ))}
      </ul>
    </div>
  );
};

export default CreateTimeRegistration;
