import axios from "axios";
import React, { createContext } from "react";
import { useAuth } from "react-oidc-context";

const ApiContext = createContext();

const ApiProvider = ({ children }) => {
  const auth = useAuth();

  const token = auth.user?.access_token;

  const config = {
    baseURL: "https://localhost:5003/api/",
  };

  const client = axios.create(config);

  const fetchCustomers = async () => {
    const token = auth.user?.access_token;
    try {
      if (!token) {
        throw "token not found";
      }
      var response = await client.get("customers", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      return { ...response, success: true };
    } catch (error) {
      console.log(error);
      return { ...error, success: false };
    }
  };

  return (
    <ApiContext.Provider value={{ fetchCustomers }}>
      {children}
    </ApiContext.Provider>
  );
};

export { ApiContext, ApiProvider };
