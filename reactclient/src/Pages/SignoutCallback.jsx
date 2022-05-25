import React from "react";
import { useAuth } from "react-oidc-context";

const SignoutCallback = () => {
  const auth = useAuth();

  if (auth.isAuthenticated) {
    auth.removeUser();
  }

  return <div>Logged Out</div>;
};

export default SignoutCallback;
