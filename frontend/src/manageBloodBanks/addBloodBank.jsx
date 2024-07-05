import './addBloodBank.css';
import React, { useState } from 'react';
import Headers from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined, FileImageFilled } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';

const EnterpriseSchedule = ({ schedule, setSchedule }) => {
  const handleTimeChange = (type, value) => {
    const newSchedule = { ...schedule, [type]: value };
    setSchedule(newSchedule);
  };

  return (
    <div className="schedule-container">
      <div className="flex items-center mb-2">
        <span className="w-24 text-sm">Horario</span>
        <div className="time-inputs">
          <input
            type="time"
            value={schedule.open || ''}
            onChange={(e) => handleTimeChange('open', e.target.value)}
            className="mr-2 p-1 border rounded"
          />
          <span className="mx-2">-</span>
          <input
            type="time"
            value={schedule.close || ''}
            onChange={(e) => handleTimeChange('close', e.target.value)}
            className="mr-2 p-1 border rounded"
          />
        </div>
      </div>
    </div>
  );
};

function AddBloodBank() {
  const [formData, setFormData] = useState({
    name: '',
    address: '',
    phoneNumber: '',
    image: null,
    schedule: {}
  });

  const navigate = useNavigate();
  const handleBack = () => {
    navigate(-1);
  };

  const handleChange = (e) => {
    const { name, value, type, files } = e.target;
    if (type === "file") {
      setFormData({
        ...formData,
        [name]: files[0]
      });
    } else {
      setFormData({
        ...formData,
        [name]: value
      });
    }
  };

  const handlePhoneNumberChange = (event) => {
    let { value } = event.target;
    value = value.replace(/[^\d]/g, '');

    if (value.length > 3) {
      value = value.slice(0, 3) + '-' + value.slice(3);
    }
    if (value.length > 7) {
      value = value.slice(0, 7) + '-' + value.slice(7, 11);
    }

    setFormData({
      ...formData,
      phoneNumber: value
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Form submitted:', formData);
  };

  return (
    <div>
      <Headers />
      <ArrowLeftOutlined className='back' onClick={handleBack} />
      <div className="add-blood-inventory-container">
        <h1>Agregar banco de sangre</h1>
        <form className="add-blood-to-bank-inventory-form" onSubmit={handleSubmit}>
          <div className="form-group-add-blood-to-inventory-form">
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
          <div className="form-group-add-blood-to-inventory-form">
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
          <div className="form-group-add-blood-to-inventory-form">
            <label htmlFor="phoneNumber">Número telefónico</label>
            <input
              type="tel"
              id="phoneNumber"
              name="phoneNumber"
              className="form-control"
              value={formData.phoneNumber}
              onChange={handlePhoneNumberChange}
              required
            />
          </div>
          <div className="form-group-add-blood-to-inventory-form">
            <label htmlFor="image">Imagen</label>
            <div className="file-input">
              <input
                type="file"
                id="image"
                name="image"
                onChange={handleChange}
                accept="image/*"
              />
              <div className="file-input-text">
                {formData.image ? formData.image.name : 'Ningún archivo seleccionado'}
              </div>
              <div className="file-input-icon">
                <FileImageFilled className='file-input-icon' />
              </div>
            </div>
          </div>
          <div className="form-group-add-blood-to-inventory-form">
            <label htmlFor="schedule">Horario</label>
            <EnterpriseSchedule
              schedule={formData.schedule}
              setSchedule={(newSchedule) => setFormData({ ...formData, schedule: newSchedule })}
            />
          </div>
          <div className="button-container">
            <button type="submit" className="accept-button-blood-inventory">Crear</button>
          </div>
        </form>
      </div>
      <Footer />
    </div>
  );
};

export default AddBloodBank;