import React, { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import './modifyProfile.css'
import Headers from '../components/header'
import Footer from '../components/footer'
import { ArrowLeftOutlined } from '@ant-design/icons'

function ModifyProfile() {
  const [notification, setNotification] = useState(null);
  const navigate = useNavigate();
  const [passwordVisible, setPasswordVisible] = useState(false);
  const [formData, setFormData] = useState({
    email: 'juan@email.com',
    phoneNumber: '+1',
    birthDate: '1975-05-05',
    gender: 'Masculino',
    medicalInfo: 'Paciente con hipertensión',
    password: ''
  });
  const [initialFormData, setInitialFormData] = useState(formData);
  const [isEditing, setIsEditing] = useState(false);
  const [isModified, setIsModified] = useState(false);

  useEffect(() => {
    setIsModified(JSON.stringify(formData) !== JSON.stringify(initialFormData));
  }, [formData, initialFormData]);

  const handleBack = () => {
    navigate(-1);
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === "phoneNumber") {
      handlePhoneNumberChange(e);
    } else {
      setFormData({ ...formData, [name]: value });
    }
  };

  const handlePhoneNumberChange = (event) => {
    let { value } = event.target;
    value = value.replace(/[^\d]/g, '');
    if (!value.startsWith('1')) {
      value = '1' + value;
    }
    if (value.length > 1) {
      value = value.slice(0, 1) + ' ' + value.slice(1);
    }
    if (value.length > 5) {
      value = value.slice(0, 5) + '-' + value.slice(5);
    }
    if (value.length > 9) {
      value = value.slice(0, 9) + '-' + value.slice(9, 13);
    }
    setFormData({
      ...formData,
      phoneNumber: value.startsWith('+') ? value : '+' + value
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (isModified) {
      console.log('Form submitted:', formData);
      setNotification('Información actualizada correctamente');
      setInitialFormData(formData);
      setIsEditing(false);
      setTimeout(() => {
        setNotification(null);
      }, 2000);
    } else {
      setNotification('No se realizaron cambios');
      setTimeout(() => {
        setNotification(null);
      }, 2000);
    }
  };

  const handleModify = () => {
    setIsEditing(true);
  };

  const handleCancel = () => {
    setFormData(initialFormData); // Revert to initial data
    setIsEditing(false);
  };

  return (
    <div>
      <Headers />
      <ArrowLeftOutlined className="back" onClick={handleBack} />
      <div className="user-modify-form-container">
        <div className="user-info-modify">
          <h1>Perfil</h1>
          <div className="user-image">
            <img src="https://via.placeholder.com/200" alt="User" />
          </div>
          <div className="user-name">Juan Ramon Martinez Scott</div>
          <div className="user-details">Cédula: 001-0000000-0</div>
          <div className="user-details">Tipo de sangre: O+</div>
        </div>
        <div className="user-modify-form">
          <form onSubmit={handleSubmit} className="user-modify-form-submit">
            <div className="form-group-user-modify">
              <label htmlFor="email">Correo electrónico:</label>
              <input
                type="email"
                id="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
                required
                disabled={!isEditing}
              />
            </div>

            <div className="form-group-user-modify">
              <label htmlFor="phoneNumber">Número telefónico:</label>
              <input
                type="tel"
                id="phoneNumber"
                name="phoneNumber"
                value={formData.phoneNumber}
                onChange={handleChange}
                required
                disabled={!isEditing}
              />
            </div>

            <div className="form-group-user-modify">
              <label htmlFor="birthDate">Fecha de nacimiento:</label>
              <input
                type="date"
                id="birthDate"
                name="birthDate"
                value={formData.birthDate}
                onChange={handleChange}
                required
                disabled={!isEditing}
              />
            </div>
            <div className="form-group-user-buttons">
              {isEditing ? (
                <>
                  <button type="button" className="button cancel-button" onClick={handleCancel}>Cancelar</button>
                  <button type="submit" className="button accept-button">Actualizar Información</button>
                </>
              ) : (
                <button type="button" className="register-button" onClick={handleModify}>Modificar</button>
              )}
            </div>
          </form>
        </div>
      </div>
      {notification && (
        <div
          className="notification"
          style={{
            backgroundColor: notification === 'No se realizaron cambios' ? 'red' : 'green'
          }}
        >
          {notification}
        </div>
      )}
      <Footer />
    </div>
  )
}

export default ModifyProfile;