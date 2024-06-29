import React, { Component, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import Footer from '../components/footer';
import Headers from '../components/header';
import { ArrowLeftOutlined, EyeOutlined, EyeInvisibleOutlined, FileImageFilled } from '@ant-design/icons';
import './registerUser.css'

function RegisterUser() {
    const [notification, setNotification] = useState("");
    const handleDocumentNumberChange = (e) => {
        const { name, value } = e.target;
        
        if (formData.documentType === "cedula") {
            let formattedValue = value.replace(/[^\d]/g, ''); // Remove any non-digit characters
        
            // Format as xxx-xxxxxxx-x
            if (formattedValue.length > 3) {
              formattedValue = formattedValue.slice(0, 3) + '-' + formattedValue.slice(3);
            }
            if (formattedValue.length > 11) {
              formattedValue = formattedValue.slice(0, 11) + '-' + formattedValue.slice(11);
            }
            if (formattedValue.length > 13) {
              formattedValue = formattedValue.slice(0, 13); // Ensure the string does not exceed the maximum length
            }
        
            setFormData({
                ...formData,
                [name]: formattedValue
            });
        } else {
            // For passport, we'll allow alphanumeric characters and some common symbols
            let formattedValue = value.replace(/[^a-zA-Z0-9-]/g, '');
            
            // Limit the length to a reasonable maximum (e.g., 20 characters)
            formattedValue = formattedValue.slice(0, 20);
            
            setFormData({
                ...formData,
                [name]: formattedValue
            });
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
            phoneNumber: '+' + value
          });
    };

    const handleFullNameChange = (e) => {
        const { name, value } = e.target;
        // Only allow letters, spaces, and hyphens (for compound names)
        const formattedValue = value.replace(/[^a-zA-Z\s-]/g, '');
        setFormData({
            ...formData,
            [name]: formattedValue
        });
    };

    const [passwordVisible, setPasswordVisible] = useState(false);
    const [formData, setFormData] = useState({
        documentType: 'passport',
        documentNumber: '',
        fullName: '',
        phoneNumber: '+1',
        email: '',
        direction: '',
        birthDate: '',
        bloodType: '',
        gender: '',
        profilePicture: null,
        password: '',
      });
      
      const [formErrors, setFormErrors] = useState({});
      const handleChange = (e) => {
        const { name, value, type, files } = e.target;

        if (name === "documentNumber") {
            handleDocumentNumberChange(e);
        } else if (name === "phoneNumber") {
            handlePhoneNumberChange(e);
        } else if (name === "fullName") {
            handleFullNameChange(e);
        } else if (type === "file") {
            // Handle file input changes
            setFormData({
                ...formData,
                [name]: files[0] // Assuming you want to handle only the first file
            });
        } else {
            // Handle other input changes normally
            setFormData({
                ...formData,
                [name]: value
            });
        }
      };
    
      const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Form submitted:', formData);
        setNotification("¡Registro exitoso!");
        setTimeout(() => setNotification(""), 2000);
        navigate('/loginUser');
      };

      const handleKeyPress = (event) => {
        // Allow only digits and control keys
        if (!/[0-9]/.test(event.key) && !['Backspace', 'ArrowLeft', 'ArrowRight', 'Tab'].includes(event.key)) {
            event.preventDefault();
        }
    };

    const navigate = useNavigate();
    const handleBack = () => {
        navigate(-1);
    }

    return (
      <div>
        <Headers />
        <div className="register-form-container">
            <ArrowLeftOutlined className="back" onClick={handleBack}/>
            <h1>¡Bienvenido/a!</h1>
            <p>Con tu donación, puedes transformar el futuro de quienes más lo necesitan. ¡Haz la diferencia hoy!</p>
            
            <form onSubmit={handleSubmit} className="register-form">
            <div className="form-group">
            {notification && <div className="notification">{notification}</div>}
                <label>Tipo de documento:</label>
                <div className="radio-group">
                <label>
                    <input
                    type="radio"
                    name="documentType"
                    value="passport"
                    checked={formData.documentType === 'passport'}
                    onChange={handleChange}
                    />
                    Pasaporte
                </label>
                <label>
                    <input
                    type="radio"
                    name="documentType"
                    value="cedula"
                    checked={formData.documentType === 'cedula'}
                    onChange={handleChange}
                    />
                    Cédula
                </label>
                </div>
            </div>

            <div className="form-group">
                <label htmlFor="direction">Dirección:</label>
                <input
                type="text"
                id="direction"
                name="direction"
                value={formData.direction}
                onChange={handleChange}
                />
            </div>

            <div className="form-group">
                <label htmlFor="documentNumber">Documento de identidad:</label>
                <input
                type="text"
                id="documentNumber"
                name="documentNumber"
                value={formData.documentNumber}
                onChange={handleChange}
                required
                placeholder={formData.documentType === 'passport' ? 'Número de pasaporte' : 'Número de cédula'}
                />
            </div>

            <div className="form-group">
                <label htmlFor="birthDate">Fecha de nacimiento:</label>
                <input
                type="date"
                id="birthDate"
                name="birthDate"
                value={formData.birthDate}
                onChange={handleChange}
                required
                placeholder="DD/MM/AAAA"
                />
            </div>

            <div className="form-group">
                <label htmlFor="fullName">Nombre completo:</label>
                <input
                type="text"
                id="fullName"
                name="fullName"
                value={formData.fullName}
                onChange={handleChange}
                required
                />
            </div>
            <div className='side-by-side'>
            <div className="form-group">
                <label htmlFor="gender">Género:</label>
                <select
                id="gender"
                name="gender"
                value={formData.gender}
                onChange={handleChange}
                required
                >
                <option value="">Seleccionar</option>
                <option value="male">Masculino</option>
                <option value="female">Femenino</option>
                </select>
            </div>

            <div className="form-group">
                <label htmlFor="bloodType">Tipo de sangre:</label>
                <select
                id="bloodType"
                name="bloodType"
                value={formData.bloodType}
                onChange={handleChange}
                required
                >
                <option value="">Seleccionar</option>
                <option value="A+">A+</option>
                <option value="A-">A-</option>
                <option value="B+">B+</option>
                <option value="B-">B-</option>
                <option value="AB+">AB+</option>
                <option value="AB-">AB-</option>
                <option value="O+">O+</option>
                <option value="O-">O-</option>
                </select>
            </div>
            </div>

            <div className="form-group">
                <label htmlFor="phoneNumber">Número telefónico:</label>
                <input
                type="tel"
                id="phoneNumber"
                name="phoneNumber"
                value={formData.phoneNumber}
                onChange={handleChange}
                onKeyPress={handleKeyPress} 
                required
                />
            </div>

            <div className="form-group">
                <label htmlFor="profilePicture">Foto de perfil:</label>
                <div className="file-input">
                    <input
                    type="file"
                    id="profilePicture"
                    name="profilePicture"
                    onChange={handleChange}
                    accept="image/*"
                    />
                    <div className="file-input-text">
                        {formData.profilePicture ? formData.profilePicture.name : 'Ningún archivo seleccionado'}
                    </div>
                    <div className="file-input-icon">
                        <FileImageFilled className='file-input-icon'/>
                    </div>
                </div>
            </div>

            <div className="form-group">
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


            <div className="form-group">
                <label htmlFor="password">Contraseña:</label>
                <div className="password-input">
                <input
                type={passwordVisible ? 'text' : 'password'}
                id="password"
                name="password"
                value={formData.password}
                onChange={handleChange}
                required
                />
                <button type="button" onClick={() => setPasswordVisible(!passwordVisible)} className='toggle-password'>
                  {passwordVisible ? <EyeInvisibleOutlined /> : <EyeOutlined />}
                </button>
                </div>
            </div>

            <button type="submit" className="register-button">Registrarse</button>
            </form>
            <p className="login-link">
                <Link to="/login" style={{color: 'inherit'}}>¿Ya tienes una cuenta?</Link>
            </p>
        </div>
        <Footer />
      </div>
    )
  }

export default RegisterUser;