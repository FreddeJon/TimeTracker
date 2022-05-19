const baseUrl = "https://localhost:7189/";

export const fetchCustomers = async () => {
  const response = await fetch(baseUrl + "api/customers");
  const json = await response.json();
  return json;
};

export const fetchProjectsForCustomer = async (customerId) => {
  const response = await fetch(
    baseUrl + `api/customers/${customerId}/projects`
  );
  const json = await response.json();
  return json;
};

export const fetchTimeRegistrationsForProject = async (
  customerId,
  projectId
) => {
  const response = await fetch(
    baseUrl + `api/customers/${customerId}/projects/${projectId}/timeregister`
  );
  const json = await response.json();
  return json;
};
