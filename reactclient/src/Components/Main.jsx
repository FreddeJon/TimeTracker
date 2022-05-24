import React, { useState } from "react";
import Projects from "../Pages/Projects";
import Customers from "../Pages/Customers";

const Main = () => {
  const [selectedCustomer, setSelectedCustomer] = useState(null);

  if (selectedCustomer)
    return (
      <Projects
        selectedCustomer={selectedCustomer}
        setSelectedCustomer={setSelectedCustomer}
      />
    );

  return <Customers setSelectedCustomer={setSelectedCustomer} />;
};

export default Main;
