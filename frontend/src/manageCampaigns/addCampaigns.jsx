import "./addCampaigns.css";
import React, { useState } from "react";
import Headers from "../components/header";
import Footer from "../components/footer";
import { ArrowLeftOutlined, FileImageFilled } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import { createCampaign } from "../api";

function AddCampaign() {
  const [formData, setFormData] = useState({
    name: "",
    organizer: "",
    address: "",
    description: "",
    startDate: "",
    startTime: "",
    endDate: "",
    endTime: "",
    image: null,
  });
  const [notification, setNotification] = useState("");

  const navigate = useNavigate();
  const handleBack = () => {
    navigate(-1);
  };

  const handleChange = (e) => {
    const { name, value, type, files } = e.target;
    if (type === "file") {
      setFormData({
        ...formData,
        [name]: files[0],
      });
    } else {
      setFormData({
        ...formData,
        [name]: value,
      });
    }
  };


  const handleSubmit = async (e) => {
    e.preventDefault();
    
    try {
      const campaignData = new FormData();
      campaignData.append('AddressID', 1); // Use a default AddressID or get it from a form field
      campaignData.append('OrganizerID', 1); // Use a default OrganizerID or get it from a form field
      campaignData.append('CampaignName', formData.name);
      campaignData.append('Description', formData.description);
      
      // Combine date and time for start and end timestamps
      const startTimestamp = new Date(`${formData.startDate}T${formData.startTime}`).toISOString();
      const endTimestamp = new Date(`${formData.endDate}T${formData.endTime}`).toISOString();
      
      campaignData.append('StartTimestamp', startTimestamp);
      campaignData.append('EndTimestamp', endTimestamp);
      
      if (formData.image) {
        campaignData.append('Image', formData.image);
      }
  
      await createCampaign(campaignData);
      
      setNotification("¡Campaña creada!");
      setTimeout(() => setNotification(""), 2000);
      
      // Clear the form
      setFormData({
        name: "",
        organizer: "",
        address: "",
        description: "",
        startDate: "",
        startTime: "",
        endDate: "",
        endTime: "",
        image: null,
      });
    } catch (error) {
      console.error('Error creating campaign:', error);
      setNotification("Error al crear la campaña");
    }
  };

  return (
    <div>
      <Headers />
      <ArrowLeftOutlined className="back" onClick={handleBack} />
      <div className="add-campaign-container">
        <h1>Crear nueva campaña</h1>
        <form className="add-campaign-form" onSubmit={handleSubmit}>
          <div className="form-group-campaigns">
            <label htmlFor="name">Nombre</label>
            <input
              type="text"
              id="name"
              name="name"
              className="form-control"
              value={formData.name}
              onChange={handleChange}
              required
            />
          </div>
          <div className="form-group-campaigns">
            <label htmlFor="organizer">Organizador</label>
            <input
              type="text"
              id="organizer"
              name="organizer"
              className="form-control"
              value={formData.organizer}
              onChange={handleChange}
              required
            />
          </div>
          <div className="form-group-campaigns">
            <label htmlFor="address">Dirección</label>
            <input
              type="text"
              id="address"
              name="address"
              className="form-control"
              value={formData.address}
              onChange={handleChange}
              required
            />
          </div>
          <div className="form-group-campaigns">
            <label htmlFor="description">Descripción</label>
            <textarea
              id="description"
              name="description"
              className="form-control"
              value={formData.description}
              onChange={handleChange}
              required
            />
          </div>
          <div className="form-group-campaigns">
            <label htmlFor="startDate">Fecha/Hora de inicio</label>
            <div className="date-time-inputs">
              <input
                type="date"
                id="startDate"
                name="startDate"
                className="form-control"
                value={formData.startDate}
                onChange={handleChange}
                required
              />
              <input
                type="time"
                id="startTime"
                name="startTime"
                className="form-control"
                value={formData.startTime}
                onChange={handleChange}
                required
              />
            </div>
          </div>
          <div className="form-group-campaigns">
            <label htmlFor="endDate">Fecha/Hora de finalización</label>
            <div className="date-time-inputs">
              <input
                type="date"
                id="endDate"
                name="endDate"
                className="form-control"
                value={formData.endDate}
                onChange={handleChange}
                required
              />
              <input
                type="time"
                id="endTime"
                name="endTime"
                className="form-control"
                value={formData.endTime}
                onChange={handleChange}
                required
              />
            </div>
          </div>
          <div className="form-group-campaigns">
            <label htmlFor="image">Foto</label>
            <div className="file-input">
              <input
                type="file"
                id="image"
                name="image"
                onChange={handleChange}
                accept="image/*"
              />
              <div className="file-input-text">
                {formData.image
                  ? formData.image.name
                  : "Ningún archivo seleccionado"}
              </div>
              <div className="file-input-icon">
                <FileImageFilled className="file-input-icon" />
              </div>
            </div>
          </div>
          <div className="button-container">
            <button type="submit" className="accept-button-blood-inventory">
              Crear
            </button>
          </div>
          {notification && <div className="notification">{notification}</div>}
        </form>
      </div>
      <Footer />
    </div>
  );
}

export default AddCampaign;
