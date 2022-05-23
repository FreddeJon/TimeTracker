import { useAuth } from "react-oidc-context";
import { BrowserRouter, Routes, Route } from "react-router-dom";

import "./App.css";
import Header from "./Components/Header";
import Main from "./Components/Main";
import SignoutCallback from "./Pages/SignoutCallback";
import SigninCallback from "./Pages/SigninCallback";
import Token from "./Pages/Token";

function App() {
  const auth = useAuth();

  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/"
          element={
            <>
              <Header />
              {auth.isAuthenticated && <Main />}
            </>
          }
        ></Route>
        <Route path="/signin-oidc" element={<SigninCallback />}></Route>
        <Route path="/signout-oidc" element={<SignoutCallback />}></Route>
        <Route path="/token" element={<Token />}></Route>
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
