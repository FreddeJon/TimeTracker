import React, { useContext } from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { DataContext } from "../Contexts/DataContext";

const TimeRegistrationTable = ({ registrations }) => {
  const { timeRegisters } = useContext(DataContext);
  return (
    <TableContainer sx={{ maxWidth: "auto" }} component={Paper}>
      <Table size="small" aria-label="a dense table">
        <TableHead>
          <TableRow>
            <TableCell align="left">Date</TableCell>
            <TableCell align="left">Time</TableCell>
            <TableCell align="left">Description</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {timeRegisters.length > 0 &&
            timeRegisters.map((row) => (
              <TimeRegistrationTableRow key={row.id} row={row} />
            ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

const TimeRegistrationTableRow = ({ row }) => {
  return (
    <TableRow
      hover
      key={row.id}
      sx={{
        "&:last-child td, &:last-child th": {
          border: 0,
        },
        cursor: "pointer",
      }}
    >
      <TableCell align="left">{new Date(row.date).toDateString()}</TableCell>
      <TableCell align="left">{formatTime(row.timeInMinutes)}h</TableCell>
      <TableCell align="left">{row.description}</TableCell>
    </TableRow>
  );
};

export default TimeRegistrationTable;

function formatTime(num) {
  var hours = Math.floor(num / 60);
  var minutes = num % 60;
  let minToreturn;
  if (minutes === 0) {
    minToreturn = "00";
  } else if (minutes < 10) {
    minToreturn = "0" + minutes;
  } else {
    minToreturn = minutes;
  }
  return hours + ":" + minToreturn;
}
