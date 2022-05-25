import { createContext, useState } from "react";
import useActiveCustomer from "../Hooks/useActiveCustomer";
import useActiveProject from "../Hooks/useActiveProject";

const DataContext = createContext();

const DataProvider = ({ children }) => {
  const { activeCustomer, setActiveCustomer } = useActiveCustomer();
  const { activeProject, setActiveProject } = useActiveProject();

  const [projects, setProjects] = useState([]);
  const [customers, setCustomers] = useState([]);
  const [timeRegisters, setTimeRegisters] = useState([]);

  return (
    <DataContext.Provider
      value={{
        activeCustomer,
        setActiveCustomer,
        activeProject,
        setActiveProject,
        projects,
        setProjects,
        customers,
        setCustomers,
        timeRegisters,
        setTimeRegisters,
      }}
    >
      {children}
    </DataContext.Provider>
  );
};

export { DataContext, DataProvider };
