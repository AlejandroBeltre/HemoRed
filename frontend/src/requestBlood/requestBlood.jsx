import React, { useState, useEffect, useContext } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import './requestBlood.css';
import { ArrowLeftOutlined } from '@ant-design/icons';
import Headers from '../components/header';
import Footer from '../components/footer';
import { getBloodBanks, getBloodTypes, getAddressById, createRequest } from '../api'; // Import the API function
import { UserContext } from '../UserContext'; // Import the UserContext

function RequestBlood() {
    const { user } = useContext(UserContext); // Get the user from the context
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

    const [bloodBanks, setBloodBanks] = useState([]); // State to store blood banks
    const [bloodTypes, setBloodTypes] = useState([]); // State to store blood types
    const [notification, setNotification] = useState("");
    const navigate = useNavigate();

    useEffect(() => {
        if (user) {
            setFormData(prevData => ({
                ...prevData,
                bloodBank: bankName,
                fullName: user.fullName || '',
                phoneNumber: user.phone || '+1',
                documentType: user.documentType === 1 ? 'cedula' : 'passport',
                addressID: user.addressID || '',
                documentNumber: user.documentNumber,
                bloodType: user.bloodTypeID || '',
                // Keep other fields as they are
            }));

            if (user.addressID) {
                const fetchAddress = async () => {
                    try {
                        const response = await getAddressById(user.addressID);
                        setFormData(prevData => ({
                            ...prevData,
                            address: response.data.street + ', ' + response.data.buildingNumber || ''
                        }));
                    } catch (error) {
                        console.error('Error fetching address:', error);
                    }
                };

                fetchAddress();
            }
        }
    }, [user, bankName]);

    useEffect(() => {
        const fetchBloodBanks = async () => {
            try {
                const response = await getBloodBanks();
                setBloodBanks(response.data);
            } catch (error) {
                console.error('Error fetching blood banks:', error);
            }
        };

        const fetchBloodTypes = async () => {
            try {
                const response = await getBloodTypes();
                setBloodTypes(response.data);
            } catch (error) {
                console.error('Error fetching blood types:', error);
            }
        };

        fetchBloodBanks();
        fetchBloodTypes();
    }, []);

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

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        const requestData = {
            userDocument: formData.documentNumber,
            bloodTypeId: parseInt(formData.bloodType),
            bloodBankId: bloodBanks.find(bank => bank.bloodBankName === formData.bloodBank)?.bloodBankID || 0,
            requestTimeStamp: new Date().toISOString(),
            requestReason: formData.reason,
            requestedAmount: parseInt(formData.quantity),
            status: 0 // Assuming 0 is the default status
        };
    
        try {
            const response = await createRequest(requestData);
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
        } catch (error) {
            setNotification(error.message || "Request failed. Please try again.");
        }
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
                            {bloodBanks.map(bank => (
                                <option key={bank.bloodBankID} value={bank.bloodBankName}>
                                    {bank.bloodBankName}
                                </option>
                            ))}
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
                            {bloodTypes.map(type => (
                                <option key={type.bloodTypeID} value={type.bloodTypeID}>
                                    {type.bloodType}
                                </option>
                            ))}
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