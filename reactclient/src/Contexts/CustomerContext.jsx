import { createContext } from "react";
import useActiveCustomer from "../Hooks/useActiveCustomer";

const CustomerContext = createContext();

const CustomerProvider = ({ children }) => {
  const { activeCustomer, setActiveCustomer } = useActiveCustomer();

  return (
    <CustomerContext.Provider value={{ activeCustomer, setActiveCustomer }}>
      {children}
    </CustomerContext.Provider>
  );
};

export { CustomerContext, CustomerProvider };
