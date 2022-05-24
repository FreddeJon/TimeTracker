import React, { useState } from "react";

import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import FormControl from "@mui/material/FormControl";
import TextField from "@mui/material/TextField";
import { Button, Divider } from "@mui/material";
import "react-datepicker/dist/react-datepicker.css";
import "bootstrap/dist/css/bootstrap.min.css";

import DatePicker from "react-datepicker";
import { postTimeRegistration } from "../Services/ApiService";
import { useAuth } from "react-oidc-context";

const style = {
  position: "absolute",
  top: "40%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  boxShadow: 24,
  p: 4,
  pt: 3,
};
const defaultValues = {
  timeInMinutes: "",
  description: "",
  date: new Date().toISOString().slice(0, 10),
};

export default function BasicModal({
  open,
  handleClose,
  selectedProject,
  selectedCustomer,
}) {
  const [startDate, setStartDate] = useState(new Date());
  const [formValues, setFormValues] = useState(defaultValues);

  const auth = useAuth();

  const handleDate = (date) => {
    setStartDate(date);
    setFormValues({ ...formValues, date: date.toISOString().slice(0, 10) });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    var token = auth.user?.access_token;
    postTimeRegistration(
      selectedCustomer.id,
      selectedProject.id,
      token,
      formValues
    ).then((response) => {
      if (response.status === 400) {
      } else {
        setFormValues(defaultValues);
        handleClose();
      }
    });
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormValues({
      ...formValues,
      [name]: value,
    });
  };

  return (
    <Modal
      open={open}
      onClose={handleClose}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box
        component="form"
        sx={style}
        noValidate={false}
        autoComplete="off"
        onSubmit={handleSubmit}
      >
        <Typography
          id="modal-modal-title"
          variant="h5"
          sx={{ mb: 1, width: "100%" }}
        >
          New timeregister
        </Typography>
        <Divider />
        <Typography
          id="modal-modal-title"
          variant="subtitle1"
          component="h2"
          sx={{ mb: 2, mt: 1, width: "100%" }}
        >
          {selectedProject.projectName}
        </Typography>
        <FormControl fullWidth>
          <TextField
            id="timeInMinutes"
            name="timeInMinutes"
            required
            label="Minutes"
            placeholder="How many minutes?"
            type="number"
            onChange={handleInputChange}
            sx={{ mb: 3 }}
          />

          <TextField
            id="description"
            name="description"
            label="Description"
            required
            multiline
            rows={4}
            placeholder="What have you done?"
            onChange={handleInputChange}
            sx={{ mb: 3 }}
          />
          <DatePicker
            selected={startDate}
            onChange={(date) => handleDate(date)}
          />
          <Button type="submit">Save</Button>
        </FormControl>
      </Box>
    </Modal>
  );
}
