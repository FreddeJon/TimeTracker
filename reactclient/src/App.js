import { useAuth } from "react-oidc-context";
import { BrowserRouter, Routes, Route } from "react-router-dom";

import "./App.css";
import Header from "./Components/Header";
import Main from "./Components/Main";
import SignoutCallback from "./Pages/SignoutCallback";
import SigninCallback from "./Pages/SigninCallback";
import Token from "./Pages/Token";
import { Container } from "@mui/material";

function App() {
  const auth = useAuth();

  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/"
          element={
            <>
              <Container
                style={{ padding: "0" }}
                sx={{
                  backgroundColor: "#F4F4F6",
                  height: "100vh",
                }}
              >
                <Header />
                {auth.isAuthenticated && (
                  <>
                    <Main />
                  </>
                )}
              </Container>
            </>
          }
        ></Route>
        <Route path="/signin-oidc" element={<SigninCallback />}></Route>
        <Route path="/signout-oidc" element={<SignoutCallback />}></Route>
        <Route
          path="/token"
          element={
            <>
              <Container
                style={{ padding: "0" }}
                sx={{
                  backgroundColor: "#F4F4F6",
                  height: "100vh",
                }}
              >
                <Header />
                <Token />
              </Container>
            </>
          }
        ></Route>
        <Route
          path="*"
          element={
            <>
              <Header />
              <main style={{ padding: "1rem" }}>
                <p>There's nothing here!</p>
              </main>
            </>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
