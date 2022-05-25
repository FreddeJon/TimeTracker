import React, { useContext, useEffect } from "react";
import { useAuth } from "react-oidc-context";
import { DataContext } from "../Contexts/DataContext";
import { fetchCustomers } from "../Services/ApiService";

import CustomerTable from "./CustomerTable";

const CustomerHandler = () => {
  const auth = useAuth();
  const { setCustomers } = useContext(DataContext);

  useEffect(() => {
    var token = auth.user?.access_token;

    if (token) {
      fetchCustomers(token).then((result) => {
        setCustomers(result.data);
      });
    }
  }, [auth.user?.access_token, setCustomers]);

  return <CustomerTable />;
};

export default CustomerHandler;
