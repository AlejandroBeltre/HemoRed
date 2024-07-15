import React, { Component, useState, useEffect, useContext } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import Footer from '../components/footer';
import Headers from '../components/header';
import { ArrowLeftOutlined, EyeOutlined, EyeInvisibleOutlined, FileImageFilled } from '@ant-design/icons';
import './registerUser.css'
import { UserContext } from '../UserContext';
import { registerUser } from '../api';

function RegisterUser() {
    const [notification, setNotification] = useState("");
    const [passwordVisible, setPasswordVisible] = useState(false);
    const [formData, setFormData] = useState({
        documentNumber: '',
        documentType: 0, // 0 for passport, 1 for cedula
        bloodTypeID: 1, // Default to first blood type, adjust as needed
        addressID: null, // You'll need to handle address creation separately
        fullName: '',
        email: '',
        password: '',
        birthDate: '',
        gender: 0, // 0 for male, 1 for female
        phone: '',
        userRole: 0, // 0 for regular user, 1 for admin
        lastDonationDate: null,
        image: null
    });

    useEffect(() => {
        const savedFormData = sessionStorage.getItem('formData');
        if (savedFormData) {
            setFormData(JSON.parse(savedFormData));
        }
    }, []);

    const handleDocumentNumberChange = (e) => {
        const { name, value } = e.target;
        let formattedValue;

        if (formData.documentType === "cedula") {
            formattedValue = value.replace(/[^\d]/g, '');
            if (formattedValue.length > 3) {
                formattedValue = formattedValue.slice(0, 3) + '-' + formattedValue.slice(3);
            }
            if (formattedValue.length > 11) {
                formattedValue = formattedValue.slice(0, 11) + '-' + formattedValue.slice(11);
            }
            if (formattedValue.length > 13) {
                formattedValue = formattedValue.slice(0, 13);
            }
        } else {
            formattedValue = value.replace(/[^a-zA-Z0-9-]/g, '');
            formattedValue = formattedValue.slice(0, 20);
        }

        setFormData({
            ...formData,
            [name]: formattedValue
        });

        // Save form data to sessionStorage
        sessionStorage.setItem('formData', JSON.stringify({
            ...formData,
            [name]: formattedValue
        }));
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
            phone: value
        });

        // Save form data to sessionStorage
        sessionStorage.setItem('formData', JSON.stringify({
            ...formData,
            phone: value
        }));
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

    const handleChange = (e) => {
        const { name, value, type, files } = e.target;

        if (name === "documentNumber") {
            handleDocumentNumberChange(e);
        } else if (name === "phone") {
            handlePhoneNumberChange(e);
        } else if (name === "fullName") {
            handleFullNameChange(e);
        } else if (type === "file") {
            setFormData({
                ...formData,
                [name]: files[0]
            });
        } else if (name === "documentType") {
            setFormData({
                ...formData,
                [name]: parseInt(value)
            });
        } else {
            setFormData({
                ...formData,
                [name]: type === "number" ? parseInt(value) : value
            });
        }

        // Update sessionStorage
        sessionStorage.setItem('formData', JSON.stringify({
            ...formData,
            [name]: type === "number" || name === "documentType" ? parseInt(value) : value
        }));
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        
        // Prepare data for API
        const dataToSend = {
            documentNumber: formData.documentNumber,
            documentType: parseInt(formData.documentType),
            bloodTypeID: parseInt(formData.bloodTypeID),
            addressID: parseInt(formData.addressID) || 1, // Defaulting to 1 if not provided
            fullName: formData.fullName,
            email: formData.email,
            password: formData.password,
            birthDate: formData.birthDate,
            gender: parseInt(formData.gender),
            phone: formData.phone.replace(/\D/g, ''), // Remove non-digit characters
            userRole: 0, // Default to regular user
        };
    
        // If there's an image, add it to formData
        if (formData.image) {
            dataToSend.image = formData.image;
        }
    
        try {
            const response = await registerUser(dataToSend);
            setNotification("Registration successful!");
            navigate('/loginUser');
        } catch (error) {
            setNotification(error.message || "Registration failed. Please try again.");
        }
    };
    const handleFileChange = (e) => {
        setFormData({
            ...formData,
            image: e.target.files[0]
        });
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
            <ArrowLeftOutlined className="back" onClick={handleBack} />
            <div className='blood-request-form-container'>
                <div className="register-form-container">
                    <h1>¡Bienvenido/a!</h1>
                    <p>Con tu donación, puedes transformar el futuro de quienes más lo necesitan. ¡Haz la diferencia hoy!</p>
                    <form onSubmit={handleSubmit} className="register-form">
                        <div className="form-group">
                            <label>Tipo de documento:</label>
                            <div className="radio-group">
                                <label>
                                    <input
                                        type="radio"
                                        name="documentType"
                                        value="0"
                                        checked={formData.documentType === 0}
                                        onChange={(e) => setFormData({ ...formData, documentType: parseInt(e.target.value) })}
                                    />
                                    Pasaporte
                                </label>
                                <label>
                                    <input
                                        type="radio"
                                        name="documentType"
                                        value="1"
                                        checked={formData.documentType === 1}
                                        onChange={(e) => setFormData({ ...formData, documentType: parseInt(e.target.value) })}
                                    />
                                    Cédula
                                </label>
                            </div>
                        </div>
                        <div className="form-group">
                            <label htmlFor="addressID">Dirección:</label>
                            <input
                                type="text"
                                id="addressID"
                                name="addressID"
                                value={formData.addressID}
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
                                placeholder={formData.documentType === 0 ? 'Número de pasaporte' : 'Número de cédula'}
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
                                    <option value="0">Masculino</option>
                                    <option value="1">Femenino</option>
                                </select>
                            </div>
                            <div className="form-group">
                                <label htmlFor="bloodTypeID">Tipo de sangre:</label>
                                <select
                                    id="bloodTypeID"
                                    name="bloodTypeID"
                                    value={formData.bloodTypeID}
                                    onChange={handleChange}
                                    required
                                >
                                    <option value="">Seleccionar</option>
                                    <option value="1">A+</option>
                                    <option value="2">A-</option>
                                    <option value="3">B+</option>
                                    <option value="4">B-</option>
                                    <option value="5">AB+</option>
                                    <option value="6">AB-</option>
                                    <option value="7">O+</option>
                                    <option value="8">O-</option>
                                </select>
                            </div>
                        </div>
                        <div className="form-group">
                            <label htmlFor="phone">Número telefónico:</label>
                            <input
                                type="tel"
                                id="phone"
                                name="phone"
                                value={formData.phone}
                                onChange={handleChange}
                                onKeyPress={handleKeyPress}
                                required
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="image">Foto de perfil:</label>
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
                        <input
                            type="hidden"
                            name="userRole"
                            value="1"  // 0 for regular user
                        />
                        <button type="submit" className="register-button">Registrarse</button>
                    </form>
                    <p className="login-link">
                        <Link to="/loginUser" style={{ color: 'inherit' }}>¿Ya tienes una cuenta?</Link>
                    </p>
                </div>
            </div>
            <Footer />
        </div>
    )
}

export default RegisterUser;