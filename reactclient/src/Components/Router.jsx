import React from "react";
import { Routes, Route } from "react-router-dom";

import Main from "./Main";
import SignoutCallback from "../Pages/SignoutCallback";
import SigninCallback from "../Pages/SigninCallback";
import Token from "../Pages/Token";

const Router = () => {
  return (
    <Routes>
      <Route path="/" element={<Main />}></Route>
      <Route path="/signin-oidc" element={<SigninCallback />}></Route>
      <Route path="/signout-oidc" element={<SignoutCallback />}></Route>
      <Route path="/token" element={<Token />}></Route>
      <Route
        path="*"
        element={
          <main style={{ padding: "1rem" }}>
            <p>There's nothing here!</p>
          </main>
        }
      />
    </Routes>
  );
};

export default Router;
