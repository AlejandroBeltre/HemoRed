import React, { useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import './requestBlood.css';
import { ArrowLeftOutlined } from '@ant-design/icons';
import Headers from '../components/header';
import Footer from '../components/footer';

function RequestBlood() {
    const location = useLocation();
    const bankName = location.state?.bankName || '';
    const [formData, setFormData] = useState({
        bloodBank: bankName,
        address: '',
        fullName: '',
        phoneNumber: '+1',
        documentType: 'passport',
        documentNumber: '',
        bloodType: '',
        quantity: '',
        identificationDocument: '',
        reason: '',
        hasDonor: 'no',
    });

    const [notification, setNotification] = useState("");
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
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

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Form submitted:', formData);
        setNotification("¡Solicitud enviada!");
        setTimeout(() => setNotification(""), 2000);

        setFormData({
            bloodBank: '',
            address: '',
            fullName: '',
            phoneNumber: '+1',
            documentType: 'passport',
            documentNumber: '',
            bloodType: '',
            quantity: '',
            identificationDocument: '',
            reason: '',
            hasDonor: 'no',
        });
        // navigate('/somewhere'); // Uncomment and set the correct path if needed
    };

    const handleBack = () => {
        navigate(-1);
    };

    return (
        <div>
            <Headers />
            <ArrowLeftOutlined className='back' onClick={handleBack} />
            <div className="blood-request-form-container">
                <h1>Solicitud de sangre</h1>
                <p>El presente formulario facilita el proceso de solicitud de sangre, por favor responder todo de manera correcta.</p>
                <form onSubmit={handleSubmit} className="blood-request-form">
                    <div className="form-group">
                        <label htmlFor="bloodBank">Banco de sangre</label>
                        <select
                            id="bloodBank"
                            name="bloodBank"
                            value={formData.bloodBank}
                            onChange={handleChange}
                            required
                        >
                            <option value="">Seleccionar</option>
                            <option value="Centro de la sangre y especialidades">Centro de la sangre y especialidades</option>
                            <option value="Banco de sangre Fundación Crisney">Banco de sangre Fundación Crisney</option>
                            <option value="San Diego Blood Bank">San Diego Blood Bank</option>
                            <option value="New York Blood Center">New York Blood Center</option>
                            <option value="Blood Bank of Delmarva">Blood Bank of Delmarva</option>
                            <option value="Carter BloodCare">Carter BloodCare</option>
                        </select> 
                    </div>

                    <div className="form-group">
                        <label htmlFor="address">Dirección</label>
                        <input
                            type="text"
                            id="address"
                            name="address"
                            value={formData.address}
                            onChange={handleChange}
                            required
                        />
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
                        <label htmlFor="quantity">Cantidad de bolsas de sangre (500ml por bolsa)</label>
                        <input
                            type="number"
                            id="quantity"
                            name="quantity"
                            value={formData.quantity}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="reason">Motivo de la solicitud</label>
                        <textarea
                            id="reason"
                            name="reason"
                            value={formData.reason}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>¿Lleva donante?</label>
                        <div className="radio-group">
                            <label>
                                <input
                                    type="radio"
                                    name="hasDonor"
                                    value="yes"
                                    checked={formData.hasDonor === 'yes'}
                                    onChange={handleChange}
                                />
                                Sí
                            </label>
                            <label>
                                <input
                                    type="radio"
                                    name="hasDonor"
                                    value="no"
                                    checked={formData.hasDonor === 'no'}
                                    onChange={handleChange}
                                />
                                No
                            </label>
                        </div>
                    </div>

                    <button type="submit" className="submit-button">Enviar</button>
                </form>
                {notification && <div className="notification">{notification}</div>}
            </div>
            <Footer />
        </div>
    );
}

export default RequestBlood;