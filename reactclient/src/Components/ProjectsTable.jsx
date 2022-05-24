import React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";

const ProjectsTable = ({ projects, setSelectedProject, selectedProject }) => {
  return (
    <TableContainer sx={{ maxWidth: "auto", minHeight: 400 }} component={Paper}>
      <Table size="small" aria-label="a dense table">
        <TableHead>
          <TableRow>
            <TableCell align="center">Project Name</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {projects.map((row) => (
            <TableRow
              selected={selectedProject === row}
              hover
              key={row.id}
              sx={{
                "&:last-child td, &:last-child th": {
                  border: 0,
                },
                cursor: "pointer",
              }}
              onClick={() => setSelectedProject(row)}
            >
              <TableCell align="left">{row.projectName}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default ProjectsTable;
