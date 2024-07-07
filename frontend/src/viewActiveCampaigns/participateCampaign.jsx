import Header from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import './participateCampaign.css';
import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';

function ParticipateCampaign() {
    const [formData, setFormData] = useState({
        campaignName: '',
        fullName: '',
        documentType: 'passport',
        documentNumber: '',
        phoneNumber: '+1',
        birthDate: '',
        bloodType: '',
        medicalCondition: '',
        isDonorCandidate: false,
        isAwareOfProcess: false,
    });

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

    const [notification, setNotification] = useState("");
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData({
            ...formData,
            [name]: type === 'checkbox' ? checked : value
        });
    };

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

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Form submitted:', formData);
        setNotification("¡Formulario enviado!");
        setTimeout(() => setNotification(""), 2000);
        // navigate('/somewhere'); // Uncomment and set the correct path if needed
    };

    const handleBack = () => {
        navigate(-1);
    };

    return (
        <div>
            <Header />
            <ArrowLeftOutlined className="back" onClick={handleBack} />
            <div className="blood-request-form-container">
                <h1>Participar en campaña</h1>
                <p>Completa el formulario para iniciar tu proceso de participación en nueva campaña.</p>
                <form onSubmit={handleSubmit} className="blood-request-form">
                    <div className="form-group">
                        <label htmlFor="campaignName">Nombre de campaña</label>
                        <select
                            id="campaignName"
                            name="campaignName"
                            value={formData.campaignName}
                            onChange={handleChange}
                            required
                        >
                            <option value="">Seleccionar</option>
                            <option value="campaign1">Campaña 1</option>
                            <option value="campaign2">Campaña 2</option>
                            {/* Add more options as needed */}
                        </select>
                    </div>

                    <div className="form-group">
                        <label htmlFor="fullName">Nombre completo</label>
                        <input
                            type="text"
                            id="fullName"
                            name="fullName"
                            value={formData.fullName}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Tipo de documento</label>
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
                        <label htmlFor="phoneNumber">Número telefónico</label>
                        <input
                            type="tel"
                            id="phoneNumber"
                            name="phoneNumber"
                            value={formData.phoneNumber}
                            onChange={handlePhoneNumberChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="documentNumber">Documento de identificación</label>
                        <input
                            type="text"
                            id="documentNumber"
                            name="documentNumber"
                            value={formData.documentNumber}
                            onChange={handleDocumentNumberChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="birthDate">Fecha de nacimiento</label>
                        <input
                            type="date"
                            id="birthDate"
                            name="birthDate"
                            value={formData.birthDate}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="bloodType">Tipo de sangre</label>
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

                    <div className="form-group">
                        <label htmlFor="medicalCondition">Condición médica</label>
                        <textarea
                            id="medicalCondition"
                            name="medicalCondition"
                            value={formData.medicalCondition}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="form-group">
                        <label>
                            <input
                                type="checkbox"
                                name="isDonorCandidate"
                                checked={formData.isDonorCandidate}
                                onChange={handleChange}
                            />
                            Confirmo que soy un donante candidato para donar sangre.
                        </label>
                    </div>

                    <div className="form-group">
                        <label>
                            <input
                                type="checkbox"
                                name="isAwareOfProcess"
                                checked={formData.isAwareOfProcess}
                                onChange={handleChange}
                            />
                            Estoy consciente del proceso de donación.
                        </label>
                    </div>

                    <button type="submit" className="submit-button">Agendar</button>
                </form>
                {notification && <div className="notification">{notification}</div>}
            </div>
            <Footer />
        </div>
    );
}

export default ParticipateCampaign;