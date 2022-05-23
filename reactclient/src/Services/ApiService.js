const baseUrl = "https://localhost:5003/";

export const fetchCustomers = async (access_token) => {
  const response = await fetch(baseUrl + "api/customers", {
    headers: {
      Authorization: `Bearer ${access_token}`,
    },
  });
  const json = await response.json();
  return json;
};

export const fetchProjectsForCustomer = async (customerId, access_token) => {
  const response = await fetch(
    baseUrl + `api/customers/${customerId}/projects`,
    {
      headers: {
        Authorization: `Bearer ${access_token}`,
      },
    }
  );
  const json = await response.json();
  return json;
};

export const fetchTimeRegistrationsForProject = async (
  customerId,
  projectId,
  access_token
) => {
  const response = await fetch(
    baseUrl + `api/customers/${customerId}/projects/${projectId}/timeregister`,
    {
      headers: {
        Authorization: `Bearer ${access_token}`,
      },
    }
  );
  const json = await response.json();
  return json;
};
