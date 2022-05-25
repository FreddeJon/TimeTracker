import React from "react";
import axios from "axios";
import { useAuth } from "react-oidc-context";

const baseURL = "https://localhost:5003/api/"


const ApiHandler = () => {
    axios.defaults.baseURL = baseURL;
const auth = useAuth();


const getCustomers()

  return {}
};

export default ApiHandler;
