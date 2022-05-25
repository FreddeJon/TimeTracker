import React, { useState, useEffect, useContext } from "react";
import { useAuth } from "react-oidc-context";

import { Typography } from "@mui/material";
import { Container } from "@mui/system";
import Paper from "@mui/material/Paper";

import CustomerTable from "../Components/CustomerTable";
import { fetchCustomers } from "../Services/ApiService";
import ApiHandler from "../Services/ApiHandler";
import { ApiContext } from "../Contexts/ApiContext";

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
  }, [auth.user?.access_token, fetchCustomers]);

  return (
    <>
      <Container component={Paper} sx={{ p: 3 }} fixed>
        <Typography variant="h4" color="text.secondary" sx={{ mb: 2 }}>
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
