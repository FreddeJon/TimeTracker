import React, { useContext } from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { DataContext } from "../Contexts/DataContext";

const ProjectsTable = () => {
  const { projects } = useContext(DataContext);

  return (
    <TableContainer sx={{ maxWidth: "auto", height: 366 }} component={Paper}>
      <Table size="small" aria-label="a dense table">
        <TableHead>
          <TableRow>
            <TableCell align="center">Project Name</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {projects.map((row) => (
            <ProjectTableRow key={row.id} row={row} />
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

const ProjectTableRow = ({ row }) => {
  const { activeProject, setActiveProject } = useContext(DataContext);

  return (
    <TableRow
      selected={activeProject === row}
      hover
      key={row.id}
      sx={{
        "&:last-child td, &:last-child th": {
          border: 0,
        },
        cursor: "pointer",
      }}
      onClick={() => setActiveProject(row)}
    >
      <TableCell align="left">{row.projectName}</TableCell>
    </TableRow>
  );
};

export default ProjectsTable;
