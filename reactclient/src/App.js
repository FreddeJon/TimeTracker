import { useAuth } from "react-oidc-context";
import { BrowserRouter } from "react-router-dom";

import "./App.css";
import Header from "./Components/Header";
import { Container } from "@mui/material";
import Router from "./Components/Router";

function App() {
  const auth = useAuth();

  return (
    <BrowserRouter>
      <Header />
      <Router />
    </BrowserRouter>
  );
}

export default App;
