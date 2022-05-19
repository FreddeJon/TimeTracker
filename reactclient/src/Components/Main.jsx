import React, { useState, useEffect } from "react";
import { fetchCustomers } from "../Services/ApiService";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Container from "@mui/material/Container";
import CreateTimeRegistration from "./CreateTimeRegistration";

const Main = () => {
  const [customers, setCustomers] = useState([]);
  const [selectedCustomer, setselectedCustomer] = useState(null);

  const clickSetCustomer = (customer) => {
    setselectedCustomer(customer);
  };
  useEffect(() => {
    fetchCustomers().then((result) => {
      setCustomers(result.data);
    });
  }, []);

  return <CreateTimeRegistration />;

  if (selectedCustomer) return <main>{selectedCustomer.name}</main>;

  return (
    <main>
      <Container fixed>
        <TableContainer component={Paper}>
          <Table sx={{ minWidth: 550 }} size="small" aria-label="a dense table">
            <TableHead>
              <TableRow>
                <TableCell>Name</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {customers.map((row) => (
                <TableRow
                  hover
                  key={row.id}
                  sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                  onClick={() => clickSetCustomer(row)}
                >
                  <TableCell align="left">{row.name}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Container>
    </main>
  );
};

export default Main;
