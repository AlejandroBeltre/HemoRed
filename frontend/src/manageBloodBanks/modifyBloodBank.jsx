import './modifyBloodBank.css';
import Headers from '../components/header';
import Footer from '../components/footer';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { useNavigate, useParams } from 'react-router-dom';
import React, { useState, useEffect } from 'react';
import { getBloodBankById, getAddressById, updateBloodBank } from '../api'; // Import updateBloodBank

function ModifyBloodBank() {
    const navigate = useNavigate();
    const { bankId } = useParams();

    const [bankDetails, setBankDetails] = useState(null);
    const [formattedAddress, setFormattedAddress] = useState('');
    const [name, setName] = useState('');
    const [address, setAddress] = useState({ street: '', buildingNumber: '' });
    const [phoneNumber, setPhoneNumber] = useState('');
    const [schedule, setSchedule] = useState('');
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [notification, setNotification] = useState("");

    useEffect(() => {
        const fetchBankDetails = async () => {
            try {
                const response = await getBloodBankById(bankId);
                setBankDetails(response.data);
                setName(response.data.bloodBankName);
                setPhoneNumber(response.data.phone);
                setSchedule(response.data.availableHours);

                const addressResponse = await getAddressById(response.data.addressID);
                setAddress(addressResponse.data);
                setFormattedAddress(`${addressResponse.data.street}, ${addressResponse.data.buildingNumber}`);
                setIsLoading(false);
            } catch (error) {
                console.error('Error fetching bank details:', error);
                setError('Error fetching bank details');
                setIsLoading(false);
            }
        };

        fetchBankDetails();
    }, [bankId]);

    const handleBack = () => {
        navigate(-1);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const formData = new FormData();
            formData.append('AddressID', bankDetails.addressID);
            formData.append('BloodBankName', name);
            formData.append('AvailableHours', schedule);
            formData.append('Phone', phoneNumber);
            formData.append('Image', bankDetails.image); // Assuming image is not being updated here
    
            await updateBloodBank(bankId, formData);
            setNotification("¡Banco de sangre actualizado!");
            setTimeout(() => setNotification(""), 2000);
        } catch (error) {
            console.error('Error updating blood bank:', error);
            setNotification("Error al actualizar el banco de sangre");
        }
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
                            value={`${address.street}, ${address.buildingNumber}`}
                            onChange={(e) => setAddress({ ...address, street: e.target.value })}
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