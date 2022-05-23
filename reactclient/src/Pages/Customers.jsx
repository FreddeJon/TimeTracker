import { Typography } from "@mui/material";
import { Container } from "@mui/system";
import React, { useState, useEffect } from "react";
import { useAuth } from "react-oidc-context";
import CustomerTable from "../Components/CustomerTable";
import { fetchCustomers } from "../Services/ApiService";

const Customers = ({ setSelectedCustomer }) => {
  const [customers, setCustomers] = useState([]);
  const auth = useAuth();
  const clickSetCustomer = (customer) => {
    setSelectedCustomer(customer);
  };
  useEffect(() => {
    var token = auth.user?.access_token;

    fetchCustomers(token).then((result) => {
      setCustomers(result.data);
    });
  }, [auth.user?.access_token]);

  return (
    <>
      <Container fixed>
        <Typography variant="h2" color="text.secondary" sx={{ mb: 2 }}>
          Customers
        </Typography>
        <CustomerTable
          customers={customers}
          clickSetCustomer={clickSetCustomer}
        />
      </Container>
    </>
  );
};

export default Customers;
