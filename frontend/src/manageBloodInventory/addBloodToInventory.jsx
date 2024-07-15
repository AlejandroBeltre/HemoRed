import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import './addBloodToInventory.css';
import Headers from "../components/header";
import Footer from "../components/footer";
import { ArrowLeftOutlined } from "@ant-design/icons";
import Select from 'react-select';
import { createBloodBag, getBloodBanks, getBloodBags, getBloodTypes } from '../api';

function AddBloodToInventory() {
    const [bloodBanks, setBloodBanks] = useState([]);
    const [selectedBank, setSelectedBank] = useState(null);
    const [bloodTypes, setBloodTypes] = useState({});
    const [bloodType, setBloodType] = useState(null);
    const [quantity, setQuantity] = useState('');
    const [expirationDate, setExpirationDate] = useState('');
    const [currentInventory, setCurrentInventory] = useState({});
    const [notification, setNotification] = useState("");
    const navigate = useNavigate();

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
                const bloodTypeData = response.data.reduce((acc, bloodType) => {
                    acc[bloodType.bloodTypeID] = bloodType.bloodType;
                    return acc;
                }, {});
                setBloodTypes(bloodTypeData);
            } catch (error) {
                console.error('Error fetching blood types:', error);
            }
        };

        fetchBloodBanks();
        fetchBloodTypes();
    }, []);

    useEffect(() => {
        const fetchBloodBags = async () => {
            if (selectedBank) {
                try {
                    const response = await getBloodBags();
                    const bankInventory = response.data
                        .filter(bag => bag.bloodBankID === selectedBank.value)
                        .reduce((acc, bag) => {
                            acc[bag.bloodTypeID] = bag.bags;
                            return acc;
                        }, {});
                    setCurrentInventory(bankInventory);
                } catch (error) {
                    console.error('Error fetching blood bags:', error);
                }
            }
        };

        fetchBloodBags();
    }, [selectedBank]);

    useEffect(() => {
        if (bloodType) {
            setQuantity(currentInventory[bloodType] || 0);
        }
    }, [bloodType, currentInventory]);

    const handleBloodTypeChange = (selectedOption) => {
        setBloodType(selectedOption ? selectedOption.value : null);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const newBloodBag = {
            bloodBankID: selectedBank ? selectedBank.value : 1,
            bloodTypeID: bloodType ? parseInt(bloodType) : 1,
            quantity: parseInt(quantity), // Include quantity in the POST request
            donationID: 1, // Assuming a default value for donationID
            expirationDate,
            isReserved: false // Assuming a default value for isReserved
        };

        if (!newBloodBag.bloodBankID || !newBloodBag.bloodTypeID) {
            setNotification("Please select a blood bank and blood type");
            setTimeout(() => setNotification(""), 2000);
            return;
        }

        try {
            const response = await createBloodBag(newBloodBag, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });
            console.log('Blood bag added:', response);
            setNotification("¡Inventario actualizado!");
            setTimeout(() => setNotification(""), 2000);

            // Clear the fields after form submission
            setSelectedBank(null);
            setBloodType(null);
            setQuantity('');
            setExpirationDate('');
        } catch (error) {
            console.error('Error adding blood bag:', error);
            setNotification("Error al actualizar el inventario");
            setTimeout(() => setNotification(""), 2000);
        }
    };

    const bankOptions = bloodBanks.map(bank => ({
        value: bank.bloodBankID,
        label: bank.bloodBankName
    }));

    const bloodTypeOptions = Object.entries(bloodTypes).map(([id, type]) => ({
        value: id,
        label: type
    }));

    return (
        <div>
            <Headers />
            <ArrowLeftOutlined className="back" onClick={() => navigate(-1)} />
            <div className="add-blood-to-inventory-container">
                <h1>Inventario de sangre</h1>
                <form className="add-blood-to-bank-inventory-form" onSubmit={handleSubmit}>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="bloodbank">Banco de sangre</label>
                        <Select
                            id="bloodbank"
                            className="add-blood-to-inventory-form"
                            options={bankOptions}
                            value={selectedBank}
                            onChange={setSelectedBank}
                            placeholder="Seleccionar banco de sangre..."
                            isClearable
                        />
                    </div>
                    <div className="form-group-add-blood-to-inventory">
                        <div className="input-container">
                            <label htmlFor="bloodType">Tipo de sangre</label>
                            <select
                                id="bloodType"
                                className="form-control"
                                value={bloodType}
                                onChange={(e) => setBloodType(e.target.value)}
                            >
                                <option value="" disabled>Seleccionar tipo de sangre...</option>
                                {bloodTypeOptions.map(option => (
                                    <option key={option.value} value={option.value}>
                                        {option.label}
                                    </option>
                                ))}
                            </select>
                        </div>
                        <div className="input-container">
                            <label htmlFor="quantity">Cantidad</label>
                            <input
                                type="number"
                                id="quantity"
                                className="form-control"
                                value={quantity}
                                onChange={(e) => setQuantity(e.target.value)}
                            />
                        </div>
                    </div>
                    <div className="form-group-add-blood-to-inventory-form">
                        <label htmlFor="expirationDate">Fecha de expiración</label>
                        <input
                            type="date"
                            id="expirationDate"
                            className="form-control"
                            value={expirationDate}
                            onChange={(e) => setExpirationDate(e.target.value)}
                        />
                    </div>
                    <div className="button-container">
                        <button type="submit" className="accept-button-blood-inventory">Aceptar</button>
                    </div>
                    {notification && <div className="notification">{notification}</div>}
                </form>
            </div>
            <Footer />
        </div>
    );
}

export default AddBloodToInventory;