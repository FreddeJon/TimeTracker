import axios from "axios";
import { useAuth } from "react-oidc-context";

const baseURL = "https://localhost:5003/api/";

const ApiHandler = () => {
  const client = axios.create({
    baseURL: baseURL,
    timeout: 1000,
  });

  const fetchCustomers = (accessToken) => {
    const config = {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    };
    client
      .get("customers")
      .then(function (response) {
        return response;
      })
      .catch(function (error) {
        console.log(error);
      })
      .then(function () {
        // always executed
      });
  };
  return { fetchCustomers };
};
export default ApiHandler;
