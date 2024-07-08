import './modifyBloodBank.css';
import Headers from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { useNavigate, useParams } from 'react-router-dom';
import React, { useState, useEffect } from 'react';

function ModifyBloodBank() {
    const navigate = useNavigate();
    const { bankId } = useParams();

    const [bloodBank, setBloodBank] = useState(null);
    const [name, setName] = useState('');
    const [address, setAddress] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [schedule, setSchedule] = useState('');
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [notification, setNotification] = useState("");

    useEffect(() => {
        const fetchBloodBank = async () => {
            setIsLoading(true);
            try {
                // Simulate fetching data from an API
                await new Promise(resolve => setTimeout(resolve, 1000));
                const mockBloodBank = {
                    id: bankId,
                    name: "Blood Bank of Alaska",
                    address: "1215 Airport Heights Dr, Anchorage, AK 99508, USA",
                    phoneNumber: "+1 (907) 222-5630",
                    schedule: "9am-5pm"
                };
                setBloodBank(mockBloodBank);
                setName(mockBloodBank.name);
                setAddress(mockBloodBank.address);
                setPhoneNumber(mockBloodBank.phoneNumber);
                setSchedule(mockBloodBank.schedule);
            } catch (error) {
                setError('Failed to fetch blood bank data');
            } finally {
                setIsLoading(false);
            }
        };

        fetchBloodBank();
    }, [bankId]);

    const handleBack = () => {
        navigate(-1);
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        // Handle form submission logic here
        console.log({ name, address, phoneNumber, schedule });
        setNotification("¡Banco de sangre actualizado!");
        setTimeout(() => setNotification(""), 2000);
    };

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <Headers />
            <ArrowLeftOutlined className='back' onClick={handleBack} />
            <div className='modify-blood-bank-container'>
                <h1>Editar banco de sangre</h1>
                <form className="add-blood-to-bank-inventory-form" onSubmit={handleSubmit}>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="name">Nombre:</label>
                        <input
                            type="text"
                            id="name"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            className="form-control"
                        />
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="address">Dirección:</label>
                        <input
                            type="text"
                            id="address"
                            value={address}
                            onChange={(e) => setAddress(e.target.value)}
                            className="form-control"
                        />
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="phoneNumber">Teléfono:</label>
                        <input
                            type="text"
                            id="phoneNumber"
                            value={phoneNumber}
                            onChange={(e) => setPhoneNumber(e.target.value)}
                            className="form-control"
                        />
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="schedule">Horario:</label>
                        <input
                            type="text"
                            id="schedule"
                            value={schedule}
                            onChange={(e) => setSchedule(e.target.value)}
                            className="form-control"
                        />
                    </div>
                    <div className="button-container">
                        <button type="submit" className="accept-button-blood-inventory">Actualizar</button>
                    </div>
                    {notification && <div className="notification">{notification}</div>}
                </form>
            </div>
            <Footer />
        </div>
    );
}

export default ModifyBloodBank;