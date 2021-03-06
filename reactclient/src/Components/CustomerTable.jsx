import React, { useContext } from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { DataContext } from "../Contexts/DataContext";

const CustomerTable = () => {
  const { customers } = useContext(DataContext);

  return (
    <TableContainer sx={{ maxWidth: "auto", height: 366 }} component={Paper}>
      <Table size="small" aria-label="a dense table">
        <TableHead>
          <TableRow>
            <TableCell>Name</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {customers.map((row) => (
            <CustomerTableRow key={row.id} row={row} />
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

const CustomerTableRow = ({ row }) => {
  const { setActiveCustomer, setActiveProject, activeCustomer } =
    useContext(DataContext);

  const handleSelect = (row) => {
    setActiveProject(null);
    setActiveCustomer(row);
  };

  return (
    <TableRow
      hover
      selected={activeCustomer === row}
      key={row.id}
      sx={{
        "&:last-child td, &:last-child th": { border: 0 },
        cursor: "pointer",
      }}
      onClick={() => handleSelect(row)}
    >
      <TableCell align="left">{row.name}</TableCell>
    </TableRow>
  );
};

export default CustomerTable;
