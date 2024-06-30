import React, { Component, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import './modifyProfile.css'
import Headers from '../components/header'
import Footer from '../components/footer'
import { ArrowLeftOutlined, EyeInvisibleOutlined, EyeOutlined } from '@ant-design/icons'

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
        console.log('Form submitted:', formData);
        // Handle form submission logic here
    };
    return (
      <div>
        <Headers />
        <ArrowLeftOutlined className="back" onClick={handleBack} />
        <div className="user-modify-form-container">
        <div className="user-info">
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
          />
        </div>

        <div className="form-group-user-modify">
          <label htmlFor="gender">Género:</label>
          <select
                id="gender"
                name="gender"
                value={formData.gender}
                onChange={handleChange}
                required
                >
                <option value="male">Masculino</option>
                <option value="female">Femenino</option>
                </select>
        </div>
        <div className="form-group-user-buttons">
          <button type="button" className="button cancel-button">Cancelar</button>
          <button type="submit" className="button accept-button">Actualizar Información</button>
        </div>
      </form>
      </div>
    </div>
        <Footer />
      </div>
    )
  }

export default ModifyProfile;
