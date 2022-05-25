import { useState } from "react";

const useActiveCustomer = () => {
  const [activeCustomer, setActiveCustomer] = useState(null);

  return { activeCustomer, setActiveCustomer };
};

export default useActiveCustomer;
