    import React, { useState, useContext, useEffect } from 'react';
    import { useNavigate, useLocation } from 'react-router-dom';
    import './scheduleAppointment.css';
    import { ArrowLeftOutlined } from '@ant-design/icons';
    import Headers from '../components/header';
    import Footer from '../components/footer';
    import { UserContext } from '../UserContext';
    import { getBloodBanks, getBloodTypes } from '../api'; // Import the API function

    function ScheduleAppointment() {
        const { user } = useContext(UserContext);
        const location = useLocation();
        const bankName = location.state?.bankName || '';
        const [formData, setFormData] = useState({
            bloodBank: bankName,
            fullName: '',
            phoneNumber: '+1',
            documentType: 'passport',
            documentNumber: '',
            birthDate: '',
            bloodType: '',
            medicalCondition: '',
            confirmDonor: false,
            awareOfProcess: false,
        });
        const [bloodBanks, setBloodBanks] = useState([]); // State to store blood banks
        const [bloodTypes, setBloodTypes] = useState([]); // State to store blood types

        useEffect(() => {
            if (user) {
                setFormData(prevData => ({
                    ...prevData,
                    bloodBank: bankName,
                    fullName: user.fullName || '',
                    phoneNumber: user.phone || '+1',
                    documentType: user.documentType === 1 ? 'cedula' : 'passport',
                    documentNumber: user.documentNumber || '',
                    birthDate: user.birthDate ? new Date(user.birthDate).toISOString().split('T')[0] : '',
                    bloodType: user.bloodTypeID || '',
                    // Keep other fields as they are
                }));
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
                fullName: '',
                phoneNumber: '+1',
                documentType: 'passport',
                documentNumber: '',
                birthDate: '',
                bloodType: '',
                medicalCondition: '',
                confirmDonor: false,
                awareOfProcess: false,
            })
            // navigate('/somewhere'); // Uncomment and set the correct path if needed
        };

        const handleBack = () => {
            navigate(-1);
        };

        return (
            <div>
                <Headers />
                <ArrowLeftOutlined className='back' onClick={handleBack} />
                <div className="schedule-appointment-container">
                    <h1>Sé donante</h1>
                    <p>Completa el formulario para iniciar tu proceso de donación.</p>
                    <form onSubmit={handleSubmit} className="schedule-appointment-form">
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
                                {bloodTypes.map(type => (
                                    <option key={type.bloodTypeID} value={type.bloodTypeID}>
                                        {type.bloodType}
                                    </option>
                                ))}
                            </select>
                        </div>

                        <div className="form-group">
                            <label htmlFor="medicalCondition">Condición médica</label>
                            <textarea
                                id="medicalCondition"
                                name="medicalCondition"
                                value={formData.medicalCondition}
                                onChange={handleChange}
                                required
                            />
                        </div>

                        <div className="form-group">
                            <label>
                                <input
                                    type="checkbox"
                                    name="confirmDonor"
                                    checked={formData.confirmDonor}
                                    onChange={handleChange}
                                    className='checkboxes'
                                />
                                Confirmo que soy un donante candidato para donar sangre.
                            </label>
                        </div>

                        <div className="form-group">
                            <label>
                                <input
                                    type="checkbox"
                                    name="awareOfProcess"
                                    checked={formData.awareOfProcess}
                                    onChange={handleChange}
                                    className='checkboxes'
                                />
                                Estoy consciente del proceso de donación.
                            </label>
                        </div>

                        <button type="submit" className="submit-button">Solicitar</button>
                    </form>
                    {notification && <div className="notification">{notification}</div>}
                </div>
                <Footer />
            </div>
        );
    }

    export default ScheduleAppointment;