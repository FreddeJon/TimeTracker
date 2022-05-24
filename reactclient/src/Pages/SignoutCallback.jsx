import React from "react";
import { useAuth } from "react-oidc-context";
import Header from "../Components/Header";

const SignoutCallback = () => {
  const auth = useAuth();

  if (auth.isAuthenticated) {
    auth.removeUser();
  }

  return (
    <>
      <Header />
      <div>Logged Out</div>
    </>
  );
};

export default SignoutCallback;
