import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import './addBloodToInventory.css'
import Headers from "../components/header";
import Footer from "../components/footer";
import { ArrowLeftOutlined } from "@ant-design/icons";
import Select from 'react-select'

function AddBloodToInventory() {
    const bloodBanks = [
        {
            id: 1,
            name: "Blood Bank of Alaska",
            isActive: true,
            address: "1215 Airport Heights Dr, Anchorage, AK 99508, USA",
            phoneNumber: "+1 (907) 222-5630",
            bloodTypes: [
                { type: "A+", bags: 23 },
                { type: "A-", bags: 100 },
                { type: "B+", bags: 23 },
                { type: "B-", bags: 100 },
                { type: "AB+", bags: 23 },
                { type: "AB-", bags: 100 },
                { type: "O+", bags: 23 },
                { type: "O-", bags: 0 },
            ]
        },
        {
            id: 2,
            name: "Centro de la sangre y especialidades",
            isActive: false,
            address: "123 Main St, Cityville",
            phoneNumber: "123-456-7890",
            bloodTypes: [
                { type: "A+", bags: 120 },
                { type: "A-", bags: 0 },
                { type: "B+", bags: 95 },
                { type: "B-", bags: 0 },
                { type: "AB+", bags: 0 },
                { type: "AB-", bags: 0 },
                { type: "O+", bags: 0 },
                { type: "O-", bags: 0 },
            ]
        },
        {
            id: 3,
            name: "Blood Bank of Dellaware",
            isActive: true,
            address: "1215 Airport Heights Dr, Anchorage, AK 99508, USA",
            phoneNumber: "+1 (907) 222-5630",
            bloodTypes: [
                { type: "A+", bags: 23 },
                { type: "A-", bags: 100 },
                { type: "B+", bags: 23 },
                { type: "B-", bags: 100 },
                { type: "AB+", bags: 23 },
                { type: "AB-", bags: 100 },
                { type: "O+", bags: 23 },
                { type: "O-", bags: 0 },
            ]
        }
    ];
    const [selectedBank, setSelectedBank] = useState('');
    const [bloodType, setBloodType] = useState('');
    const [quantity, setQuantity] = useState('');
    const [expirationDate, setExpirationDate] = useState('');
    const [notification, setNotification] = useState("");
    const navigate = useNavigate();
    const handleBack = () => {
        navigate(-1);
    }
    const handleSubmit = (e) => {
        e.preventDefault();
        console.log({
            selectedBank: selectedBank ? selectedBank.value : null,
            bloodType,
            quantity,
            expirationDate
        });
        setNotification("¡Inventario actualizado!");
        setTimeout(() => setNotification(""), 2000);
        
        // Clear the fields after form submission
        setSelectedBank('');
        setBloodType('');
        setQuantity('');
        setExpirationDate('');
    }

    const bankOptions = bloodBanks.map(bank => ({
        value: bank.id,
        label: bank.name
    }));
    return (
        <div>
            <Headers />
            <ArrowLeftOutlined className="back" onClick={handleBack} />
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
                            isClerable
                        >
                        </Select>
                        <div className="form-row">
                            <div className="form-group-add-blood-to-inventory">
                                <div className="input-container">
                                    <label htmlFor="bloodType">Tipo de sangre</label>
                                    <select
                                        id="bloodType"
                                        className="form-control"
                                        value={bloodType}
                                        onChange={(e) => setBloodType(e.target.value)}
                                    >
                                        <option value="">Seleccione un tipo de sangre</option>
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
                    </div>
                </form>
            </div>
            <Footer />
        </div>
    )
}

export default AddBloodToInventory;