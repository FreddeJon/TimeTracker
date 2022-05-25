import {
  Button,
  Divider,
  FormControl,
  Paper,
  TextField,
  Typography,
} from "@mui/material";
import { Box, Container } from "@mui/system";
import React, { useContext, useState } from "react";
import { useAuth } from "react-oidc-context";
import { DataContext } from "../../Contexts/DataContext";
import {
  fetchTimeRegistrationsForProject,
  postTimeRegistration,
} from "../../Services/ApiService";

const defaultValues = {
  timeInMinutes: "",
  description: "",
  date: new Date(),
};

const NewRegisterForm = () => {
  const { activeCustomer, activeProject, setTimeRegisters } =
    useContext(DataContext);

  const [formValues, setFormValues] = useState(defaultValues);
  const auth = useAuth();

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormValues({
      ...formValues,
      [name]: value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    var token = auth.user?.access_token;
    postTimeRegistration(
      activeCustomer.id,
      activeProject.id,
      token,
      formValues
    ).then((response) => {
      if (response.status === 400) {
      } else {
        setFormValues(defaultValues);

        fetchTimeRegistrationsForProject(
          activeCustomer.id,
          activeProject.id,
          token
        ).then((response) => {
          if (response.status === 400) {
            console.log(response);
          }
          setTimeRegisters(response.data);
        });
      }
    });
  };
  return (
    <Container sx={{ maxWidth: "auto", height: 366, pt: 4 }} component={Paper}>
      <Box
        component="form"
        noValidate={false}
        autoComplete="off"
        onSubmit={handleSubmit}
      >
        <Typography
          id="modal-modal-title"
          variant="h5"
          sx={{ mb: 1, width: "100%" }}
        >
          Register for {activeProject.projectName}
        </Typography>
        <Divider />
        <FormControl fullWidth>
          <TextField
            variant="standard"
            id="timeInMinutes"
            name="timeInMinutes"
            required
            value={formValues.timeInMinutes}
            label="Minutes"
            placeholder="How many minutes?"
            type="number"
            onChange={handleInputChange}
            sx={{ mb: 1 }}
          />
          <TextField
            variant="standard"
            id="description"
            name="description"
            label="Description"
            value={formValues.description}
            required
            multiline
            rows={2}
            placeholder="What have you done?"
            onChange={handleInputChange}
            sx={{ mb: 3 }}
          />
          <TextField
            variant="standard"
            id="date"
            name="date"
            required
            value={formValues.date}
            type="date"
            label="Date"
            onChange={handleInputChange}
            InputLabelProps={{
              shrink: true,
            }}
            sx={{ mb: 3 }}
          />
          <Button type="submit">Add</Button>
        </FormControl>
      </Box>
    </Container>
  );
};

export default NewRegisterForm;
